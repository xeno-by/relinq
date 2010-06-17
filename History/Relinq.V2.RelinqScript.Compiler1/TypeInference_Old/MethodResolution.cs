using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V2.Helpers;
using Relinq.V2.RelinqScript.Grammar.Expressions;
using Relinq.V2.RelinqScript.Reflection;
using LambdaExpression=Relinq.V2.RelinqScript.Grammar.Expressions.LambdaExpression;

namespace Relinq.V2.RelinqScript.Compiler1.TypeInference_Old
{
    public class MethodResolution
    {
        // in
        private MethodInfo MethodInfo { get; set; }
        private Closure Closure { get; set; }
        private InvokeExpression CallAst { get; set; }

        // out
        private bool? Result { get; set; }

        // aux
        private ResolutionPass? PassesComplete { get; set; }
        private Expression Target { get; set; }
        private List<Type> FormalArgs { get; set; }
        private List<Expression> RealArgs { get; set; }
        private Dictionary<Type, Type> InferenceCache { get; set; }

        public MethodResolution(MethodInfo methodInfo, Closure closure, InvokeExpression callAst)
        {
            MethodInfo = methodInfo;
            Closure = closure;
            CallAst = callAst;
            Result = null;

            PassesComplete = null;
            Target = new StronglyTypedAstBuilder(closure).Visit(callAst.Target);
            FormalArgs = new List<Type>(MethodInfo.GetParameters().Select(p => p.ParameterType));
            RealArgs = new List<Expression>(callAst.Args.Select(ast => ast is LambdaExpression ?
                new StronglyTypedAstBuilder(closure).VisitLambda((LambdaExpression)ast, null, true) :
                new StronglyTypedAstBuilder(closure).Visit(ast)));
            InferenceCache = new Dictionary<Type, Type>();
            Array.ForEach(MethodInfo.GetGenericArguments(), t => InferenceCache.Add(t, null));
        }

        public bool? Success
        {
            get
            {
                return Result;
            }
        }

        public MethodCallExpression ResolvedCall
        {
            get
            {
                if (Result.HasValue && Result.Value)
                {
                    return MethodInfo.IsExtension() ? 
                       Expression.Call(MethodInfo, RealArgs.ToArray()) : 
                       Expression.Call(Target, MethodInfo, RealArgs.ToArray());
                }
                else
                {
                    return null;
                }
            }
        }

        private Type ResolveGenericArgs(Type t)
        {
            if (InferenceCache.ContainsKey(t))
            {
                return InferenceCache[t];
            }
            else
            {
                if (!t.IsGenericType)
                {
                    return t;
                }
                else
                {
                    var resolvedArgs = t.GetGenericArguments().Select(genericArg => ResolveGenericArgs(genericArg));
                    if (resolvedArgs.Any(resolvedArg => resolvedArg == null))
                    {
                        return null;
                    }
                    else
                    {
                        return t.GetGenericTypeDefinition().MakeGenericType(resolvedArgs.ToArray());
                    }
                }
            }
        }

        public bool? Resolve(ResolutionPass pass)
        {
            // Guard ourselves from invalid/unnecessary calls
            if (Result == false) return false;
            if (PassesComplete == null && pass != ResolutionPass.First) throw new ArgumentException(String.Format(
                "Cannot execute resolution pass '{0}'. Expecting '{1}'", pass, ResolutionPass.First));
            if (PassesComplete == ResolutionPass.First && pass != ResolutionPass.Second) throw new ArgumentException(String.Format(
                "Cannot execute resolution pass '{0}'. Expecting '{1}'", pass, ResolutionPass.Second));
            if (PassesComplete == ResolutionPass.Second) return Result;

            if (pass == ResolutionPass.Second)
            {
                if (MethodInfo.IsGenericMethodDefinition)
                {
                    // Lez infer lambda arguments using knowledge about method generic argument types
                    // upd. Along with that we'll infer stuff that wasn't touched during the first pass

                    for (var i = 0; i < FormalArgs.Count; ++i)
                    {
                        if (FormalArgs[i].IsFunctionType())
                        {
                            var fFormal = FormalArgs[i].GetFunctionDesc();
                            var resolvedLambdaArgs = fFormal.Args.Select(lambdaArg => ResolveGenericArgs(lambdaArg));

                            if (resolvedLambdaArgs.Any(resolvedLambdaArg => resolvedLambdaArg == null))
                            {
                                throw new ArgumentException("First pass of resolution failed: " +
                                    "not all types necessary to fully resolve lambda parameters were inferred.");
                            }
                            else
                            {
                                RealArgs[i] = new StronglyTypedAstBuilder(Closure).VisitLambda(
                                    (LambdaExpression)CallAst.Args.ElementAt(i),
                                    resolvedLambdaArgs.ToArray());

                                var realArgType = RealArgs[i].GetType().IsLambda() && FormalArgs[i].IsLambda() ?
                                   RealArgs[i].GetType() : RealArgs[i].Type;
                                MatchTypesExact(FormalArgs[i], realArgType, ResolutionPass.Second, true);
                            }
                        }
                    }

                    // Everything should be inferred at this moment, or ALL ABOARD THE FAILBOAT
                    if (InferenceCache.Any(kvp => kvp.Value == null))
                    {
                        throw new ArgumentException("First pass of resolution failed: not all generic arguments had been inferred");
                    }

                    MethodInfo = MethodInfo.MakeGenericMethod(InferenceCache.Values.ToArray());
                    FormalArgs = new List<Type>(MethodInfo.GetParameters().Select(p => p.ParameterType));
                    InferenceCache = new Dictionary<Type, Type>();
                }
            }

            // Screw varargs methods for now
            var formalArgs = MethodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            var realArgs = RealArgs.Select((arg, i) => arg.GetType().IsLambda() && FormalArgs[i].IsLambda() ? arg.GetType() : arg.Type).ToArray();

            if (formalArgs.Length == realArgs.Length)
            {
                Result = true;

                for (var i = 0; i < formalArgs.Length; i++)
                {
                    var formal = formalArgs[i];
                    var real = realArgs[i];

                    var matchResult = MatchTypesAllowInheritance(formal, real, pass);
                    Result = Result.Accumulate(matchResult);
                }

                // first version of method inference assumed that at this point all generic args are inferred
                // however this approach is invalid since return result might need some help from lambda parameters
                // example: IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource,TResult>> selector)
                // damn that was so elegant...
            }
            else
            {
                Result = false;
            }

            // Update information about state
            if (PassesComplete == ResolutionPass.First) PassesComplete = ResolutionPass.Second;
            if (PassesComplete == null) PassesComplete = ResolutionPass.First;

            return Result;
        }

        private bool? MatchTypesAllowInheritance(Type formal, Type real, ResolutionPass pass)
        {
            // We don't compare metadata tokens here yet because even if they are equal we need to fill inferenceCache.
//            if (formal.MetadataToken == real.MetadataToken) return true;

            // Next try natural type conversion
            if (formal.IsAssignableFrom(real))
            {
                return true;
            }

            // If that fails, maybe the types are delegates/lambdas and need special treatment
            if (formal.IsFunctionType() && real.IsFunctionType())
            {
                return MatchFunctionTypes(formal, real, pass, false);
            }

            // If that fails, maybe formal parameter type is an generic type parameterized by method generic argument
            // and that fucks up all the conversion jazz (see Playground.V2.Tests.TestParameterInfo)
            if (formal.IsGenericType && formal.FullName == null)
            {
                Type baseOfRealThatMatchesFormal;
                if (formal.IsInterface)
                {
                    // All interfaces will be included here, not only the ones directly implemented => no recursion
                    // What's more - this stuff works fine for open generics (see Playground.V2.Tests.TestGetInterfaces)
                    // And what's more - generic arguments of base interfaces will match those of the real type
                    var baseInterfaces = new List<Type>(real.GetInterfaces());
                    if (real.IsInterface) baseInterfaces.Add(real);

                    baseOfRealThatMatchesFormal =
                        baseInterfaces.SingleOrDefault(iface => iface.SameMetadataToken(formal));
                }
                else
                {
                    var baseTypes = new List<Type>();
                    for (var current = real; current != null; current = current.BaseType) baseTypes.Add(current);

                    baseOfRealThatMatchesFormal =
                        baseTypes.SingleOrDefault(type => type.SameMetadataToken(formal));
                }

                // If this is not null, then stuff isn't that bad - now we only need to match generic type arguments
                // with previously inferred types and update the inference cache with matching results.
                if (baseOfRealThatMatchesFormal != null)
                {
                    bool? ok = true;
                    for (var i = 0; i < formal.GetGenericArguments().Length; ++i)
                    {
                        var formalArg = formal.GetGenericArguments()[i];
                        var realArg = real.GetGenericArguments()[i];

                        var matchResult = MatchTypesExact(formalArg, realArg, pass, true);
                        ok = ok.Accumulate(matchResult);
                    }

                    if (!ok.HasValue && pass == ResolutionPass.Second)
                    {
                        throw new ArgumentException("Second-pass resolution can't result in a null match");
                    }

                    return ok;
                }
            }

            // Types are incompatible
            return false;
        }

        private bool? MatchTypesExact(Type formal, Type real, ResolutionPass pass, bool allowTypeInference)
        {
            // Replace formal type with its inferred type if it's a generic argument of the method
            if (InferenceCache.ContainsKey(formal))
            {
                // If a generic argument of the method ain't inferred yet, it's high time for that
                if (allowTypeInference && InferenceCache[formal] == null)
                    InferenceCache[formal] = real;

                if (InferenceCache[formal] != null)
                    formal = InferenceCache[formal];
            }

            // If the types are delegates/lambdas they need special treatment
            if (formal.IsFunctionType() && real.IsFunctionType())
            {
                return MatchFunctionTypes(formal, real, pass, allowTypeInference && pass == ResolutionPass.Second);
            }
            else
            {
                bool? ok = formal.SameMetadataToken(real);

                if (ok != false && real.IsGenericType)
                {
                    var formalArgs = formal.GetGenericArguments();
                    var realArgs = real.GetGenericArguments();

                    for (var i = 0; i < realArgs.Length; ++i)
                    {
                        var matchResult = MatchTypesExact(formalArgs[i], realArgs[i], pass, allowTypeInference);
                        ok = ok.Accumulate(matchResult);
                    }
                }

                return ok;
            }
        }

        private bool? MatchFunctionTypes(Type formal, Type real, ResolutionPass pass, bool allowTypeInference)
        {
            if (formal.IsLambda() && real.IsDelegate())
            {
                return false;
            }
            else
            {
                var formalDesc = formal.GetFunctionDesc();
                var realDesc = real.GetFunctionDesc();
                var argsOfFormal = formalDesc.Args.ToArray();
                var argsOfReal = realDesc.Args.ToArray();

                if (argsOfFormal.Length != argsOfReal.Length)
                {
                    return false;
                }
                else
                {
                    bool? ok = true;
                    for (var i = 0; i < argsOfFormal.Length; ++i)
                    {
                        var matchResult = MatchTypesExact(argsOfFormal[i], argsOfReal[i], pass, allowTypeInference);
                        ok = ok.Accumulate(matchResult);
                    }

                    var returnValueMatched = MatchTypesExact(formalDesc.ReturnValue, realDesc.ReturnValue, pass, allowTypeInference);
                    ok = ok.Accumulate(returnValueMatched);

                    if (ok == false && pass == ResolutionPass.First)
                    {
                        ok = null;
                    }

                    return ok;
                }
            }
        }
    }
}