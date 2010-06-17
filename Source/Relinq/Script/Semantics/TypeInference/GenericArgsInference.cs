using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution;
using Relinq.Script.Compilation;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.StructuralTrees;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Semantics.Lookup;

namespace Relinq.Script.Semantics.TypeInference
{
    // http://code.google.com/p/relinq/wiki/GenericArgsInference
    public static class GenericArgsInference
    {
        public static MethodInfo InferGenericTypeParams(this MethodInfo mi, TypeInferenceContext ctx)
        {
            var originalSignature = mi;
            TypeInferenceEngine preview = null;

            try
            {
                preview = new TypeInferenceEngine(ctx);
                ctx.Root.CallArgs().ForEach(child => preview.InferTypes(child));

                var fail = Array.FindIndex(ctx.Root.CallArgs().ToArray(), c => preview.Inferences[c] is Variant);
                if (fail != -1)
                {
                    throw new GenericArgumentsInferenceException(
                        GenericArgumentsInferenceExceptionType.VariantArg,
                        originalSignature, mi, preview, fail);
                }
                else
                {
                    Func<IEnumerable<RelinqScriptType>> args = () => ctx.Root.ToXArgs(preview.Inferences);
                    Func<IEnumerable<Type>> @params = () => mi.ToXArgs(args().Count());

                    var argc = args().Count();
                    if (argc != @params().Count())
                    {
                        throw new GenericArgumentsInferenceException(
                            GenericArgumentsInferenceExceptionType.ArgcMismatch,
                            originalSignature, mi, preview);
                    }
                    else
                    {
                        var processed = new TrackableList<bool>(Enumerable.Repeat(false, argc));
                        var fresh = new List<bool>(Enumerable.Repeat(true, argc));
                        processed.ListChanged += (o, e) => fresh[e.Index] = true;

                        while (fresh.Any(b => b) && !processed.All(b => b))
                        {
                            Enumerable.Range(0, argc).ForEach(i => fresh[i] = false);
                            foreach (var i in processed.ToArray()
                                .Select((b, j) => new { b, j }).Where(_ => !_.b).Select(_ => _.j))
                            {
                                var ai_r = args().ElementAt(i);
                                var pi = @params().ElementAt(i);

                                var argIndex = mi.IsExtension() ? i : i - 1;

                                if (!pi.IsOpenGeneric())
                                {
                                    if (pi.IsFunctionType() && ai_r is Lambda)
                                    {
                                        var ai_s = ((Lambda)ai_r).Type.GetFunctionSignature();
                                        var pi_s = pi.GetFunctionSignature();

                                        // todo. support varargs here as well
                                        if (ai_s.GetParameters().Length == pi_s.GetParameters().Length)
                                        {
                                            var preview2 = new TypeInferenceEngine(ctx);

                                            // todo. the line below is only possible because we don't process lambda extension calls
                                            var callArg = ctx.Root.CallArgs().ElementAt(i - 1);
                                            preview2.Ctx.Inferences.Add(callArg, new Lambda(
                                                (LambdaExpression)callArg,
                                                (pi_s.GetParameters()
                                                    .Select(pi2 => pi2.ParameterType.IsGenericParameter ?
                                                        typeof(Variant) : pi2.ParameterType)
                                                    .Concat(typeof(Variant).AsArray()))
                                                    .ForgeFuncType()));

                                            try
                                            {
                                                preview2.InferTypes(callArg);
                                            }
                                            catch (RelinqException rex)
                                            {
                                                if (rex.IsUnexpected)
                                                {
                                                    throw;
                                                }
                                                else
                                                {
                                                    throw new GenericArgumentsInferenceException(
                                                        GenericArgumentsInferenceExceptionType.LambdaInconsistentBody,
                                                        originalSignature, mi, preview, i, rex);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new GenericArgumentsInferenceException(
                                                    GenericArgumentsInferenceExceptionType.Unexpected,
                                                    originalSignature, mi, preview, i, ex);
                                            }

                                            var lambdaBody = ((LambdaExpression)callArg).Body;
                                            var retval = preview2.Inferences[lambdaBody];

                                            if (retval is ClrType)
                                            {
                                                var ai_ret = ((ClrType)retval).Type;
                                                var pi_ret = pi_s.ReturnType;

                                                if (!ai_ret.HasImplicitCastTo(pi_ret))
                                                {
                                                    throw new GenericArgumentsInferenceException(
                                                        GenericArgumentsInferenceExceptionType.LambdaIncompatibleRetval,
                                                        originalSignature, mi, preview, i);
                                                }
                                            }
                                            else
                                            {
                                                // lambdas that return lambdas or method groups
                                                // are not implemented, since they're not supported by C# spec.
                                                throw new GenericArgumentsInferenceException(
                                                    GenericArgumentsInferenceExceptionType.LambdaInvalidRetval,
                                                    originalSignature, mi, preview, i);
                                            }
                                        }
                                        else
                                        {
                                            throw new GenericArgumentsInferenceException(
                                                GenericArgumentsInferenceExceptionType.LambdaArgcMismatch,
                                                originalSignature, mi, preview, i);
                                        }
                                    }

                                    processed[i] = true;
                                }
                                else
                                {
                                    if (ai_r is ClrType)
                                    {
                                        var ai = ((ClrType)ai_r).Type;

                                        // todo. also check implicit casts here?
                                        var inf = ai.LookupInheritanceChain()
                                            .Select(aij => pi.InferFrom(aij))
                                            .FirstOrDefault(inf2 => inf2 != null);

                                        if (inf != null)
                                        {
                                            mi = mi.InferStructuralUnits(inf.ToDictionary(
                                                kvp => "a" + argIndex + kvp.Key,
                                                kvp => kvp.Value));

                                            if (mi != null)
                                            {
                                                processed[i] = true;
                                            }
                                            else
                                            {
                                                throw new GenericArgumentsInferenceException(
                                                    GenericArgumentsInferenceExceptionType.InferencesContradictGenericConstraints,
                                                    originalSignature, mi, preview, i);
                                            }
                                        }
                                        else
                                        {
                                            throw new GenericArgumentsInferenceException(
                                                GenericArgumentsInferenceExceptionType.FormalArgCannotBeInferredFromActualArg,
                                                originalSignature, mi, preview, i);
                                        }
                                    }
                                    else if (ai_r is Lambda)
                                    {
                                        if (pi.IsFunctionType())
                                        {
                                            var ai_s = ((Lambda)ai_r).Type.GetFunctionSignature();
                                            var pi_s = pi.GetFunctionSignature();

                                            // todo. support varargs here as well
                                            if (ai_s.GetParameters().Length == pi_s.GetParameters().Length)
                                            {
                                                var preview2 = new TypeInferenceEngine(ctx);

                                                // todo. the line below is only possible because we don't process lambda extension calls
                                                var callArg = ctx.Root.CallArgs().ElementAt(i - 1);
                                                preview2.Ctx.Inferences.Add(callArg, new Lambda(
                                                    (LambdaExpression)callArg,
                                                    (pi_s.GetParameters()
                                                        .Select(pi2 => pi2.ParameterType.IsGenericParameter ?
                                                            typeof(Variant) : pi2.ParameterType)
                                                        .Concat(typeof(Variant).AsArray()))
                                                        .ForgeFuncType()));

                                                try
                                                {
                                                    preview2.InferTypes(callArg);
                                                }
                                                catch (RelinqException rex)
                                                {
                                                    if (rex.IsUnexpected)
                                                    {
                                                        throw;
                                                    }
                                                    else
                                                    {
                                                        throw new GenericArgumentsInferenceException(
                                                            GenericArgumentsInferenceExceptionType.LambdaInconsistentBody,
                                                            originalSignature, mi, preview, i, rex);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw new GenericArgumentsInferenceException(
                                                        GenericArgumentsInferenceExceptionType.Unexpected,
                                                        originalSignature, mi, preview, i, ex);
                                                }

                                                var lambdaBody = ((LambdaExpression)callArg).Body;
                                                var retval = preview2.Inferences[lambdaBody];

                                                if (retval is ClrType)
                                                {
                                                    var ai_ret = ((ClrType)retval).Type;
                                                    var pi_ret = pi_s.ReturnType;

                                                    if (pi_ret.IsOpenGeneric())
                                                    {
                                                        var inf = ai_ret.LookupInheritanceChain()
                                                            .Select(ai_retj => pi_ret.InferFrom(ai_retj))
                                                            .FirstOrDefault(inf2 => inf2 != null);

                                                        if (inf != null)
                                                        {
                                                            var map = pi.GetStructuralTree();
                                                            mi = mi.InferStructuralUnits(inf.ToDictionary(
                                                                kvp => "a" + argIndex + map.Single(kvp2 =>
                                                                    kvp2.Value == pi_ret.SelectStructuralUnit(kvp.Key)).Key,
                                                                kvp => kvp.Value));

                                                            if (mi != null)
                                                            {
                                                                processed[i] = true;
                                                            }
                                                            else
                                                            {
                                                                throw new GenericArgumentsInferenceException(
                                                                    GenericArgumentsInferenceExceptionType.InferencesContradictGenericConstraints,
                                                                    originalSignature, mi, preview, i);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            throw new GenericArgumentsInferenceException(
                                                                GenericArgumentsInferenceExceptionType.FormalArgCannotBeInferredFromActualArg,
                                                                originalSignature, mi, preview, i);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        processed[i] = true;
                                                    }
                                                }
                                                else if (retval is Variant)
                                                {
                                                    continue;
                                                }
                                                // lambdas that return lambdas or method groups
                                                // are not implemented, since they're not supported by C# spec.
                                                else
                                                {
                                                    throw new GenericArgumentsInferenceException(
                                                        GenericArgumentsInferenceExceptionType.LambdaInvalidRetval,
                                                        originalSignature, mi, preview, i);
                                                }
                                            }
                                            else
                                            {
                                                throw new GenericArgumentsInferenceException(
                                                    GenericArgumentsInferenceExceptionType.LambdaArgcMismatch,
                                                    originalSignature, mi, preview, i);
                                            }
                                        }
                                        else
                                        {
                                            throw new GenericArgumentsInferenceException(
                                                GenericArgumentsInferenceExceptionType.FormalArgCannotBeInferredFromActualArg,
                                                originalSignature, mi, preview, i);
                                        }
                                    }
                                    // unlike lambdas, method groups don't participate in type args inference 
                                    // neither they can help to infer type args, 
                                    // nor can they be inferred themselves.
                                    else
                                    {
                                        processed[i] = true;
                                    }
                                }
                            }
                        }

                        if (!mi.IsOpenGeneric())
                        {
                            return mi;
                        }
                        else
                        {
                            throw new GenericArgumentsInferenceException(
                                GenericArgumentsInferenceExceptionType.IncompleteInferenceSet,
                                originalSignature, mi, preview);
                        }
                    }
                }
            }
            catch (GenericArgumentsInferenceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GenericArgumentsInferenceException(
                    GenericArgumentsInferenceExceptionType.Unexpected,
                    originalSignature, mi, preview ?? new TypeInferenceEngine(ctx), ex);
            }
        }
    }
}
