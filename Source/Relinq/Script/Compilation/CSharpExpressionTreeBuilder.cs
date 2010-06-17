using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.CSharpBuilder;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Integration;
using Relinq.Script.Semantics.Casts;
using Relinq.Script.Semantics.Literals;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.Operators;
using LinqExpression = System.Linq.Expressions.Expression;

namespace Relinq.Script.Compilation
{
    // todo. implement sanity checker instead of endless conditions!
    // todo. and do the same for TypeInferenceEngine:
    // todo.   * RelinqScriptAstValidator
    // todo.   * TypeInferenceCacheValidator (that is inherited from RSAV!!)
    // todo. when implementing those, think of *additional* checks

    // http://code.google.com/p/relinq/wiki/BuildingCSharpAst
    public class CSharpExpressionTreeBuilder
    {
        private RelinqScriptExpression Ast { get; set; }
        private IntegrationContext Integration { get; set; }

        public CSharpExpressionTreeBuilder(RelinqScriptExpression ast, IntegrationContext integration)
        {
            Ast = ast;
            Integration = integration;
        }

        public LinqExpression Compile(TypeInferenceCache inferences)
        {
            return Compile(Ast, new CompilationContext(inferences));
        }

        private LinqExpression Compile(RelinqScriptExpression e, CompilationContext ctx)
        {
            try
            {
                if (!ctx.Types.ContainsKey(e))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, e, ctx);
                }

                if (!(ctx.Types[e] is ClrType || ctx.Types[e] is Null ||
                      ctx.Types[e] is UnknownConstant || ctx.Types[e] is Lambda))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, e, ctx);
                }

                if (e.IsCall() && !ctx.Invocations.ContainsKey(e))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, e, ctx);
                }

                if (e is LambdaExpression && !(ctx.Types[e] is Lambda))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, e, ctx);
                }

                switch (e.NodeType)
                {
                    case ExpressionType.Keyword:
                        return CompileKeyword((KeywordExpression)e, ctx);

                    case ExpressionType.Variable:
                        return CompileVariable((VariableExpression)e, ctx);

                    case ExpressionType.Constant:
                        return CompileConstant((ConstantExpression)e, ctx);

                    case ExpressionType.New:
                        return CompileNew((NewExpression)e, ctx);

                    case ExpressionType.Lambda:
                        return CompileLambda((LambdaExpression)e, ctx);

                    case ExpressionType.MemberAccess:
                        return CompileMemberAccess((MemberAccessExpression)e, ctx);

                    case ExpressionType.Invoke:
                        return CompileInvoke((InvokeExpression)e, ctx);

                    case ExpressionType.Indexer:
                        return CompileIndexer((IndexerExpression)e, ctx);

                    case ExpressionType.Operator:
                        return CompileOperator((OperatorExpression)e, ctx);

                    case ExpressionType.Conditional:
                        return CompileConditional((ConditionalExpression)e, ctx);
                }
            }
            catch (CSharpBuilderException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.Unexpected, Ast, e, ctx, ex);
            }

            throw new NotSupportedException(e.ToString());
        }

        private LinqExpression CompileKeyword(KeywordExpression ke, CompilationContext ctx)
        {
            return LinqExpression.Constant(Integration.ProduceCSharp(ke.Name));
        }

        private LinqExpression CompileVariable(VariableExpression ve, CompilationContext ctx)
        {
            if (!ctx.Closure.ContainsKey(ve.Name))
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ve, ctx);
            }

            return ctx.Closure[ve.Name];
        }

        private LinqExpression CompileConstant(ConstantExpression ce, CompilationContext ctx)
        {
            Func<Type, LinqExpression> constFactory = clrType =>
                LinqExpression.Constant(JsonDeserializer.Deserialize(ce.Data, clrType), clrType);

            if (ctx.Types[ce] is ClrType)
            {
                return constFactory(((ClrType)ctx.Types[ce]).Type);
            }
            else if (ctx.Types[ce] is Null || ctx.Types[ce] is UnknownConstant)
            {
                return constFactory(AcquireExpectedTypeFromContext(ce, ctx));
            }
            else
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ce, ctx);
            }
        }

        private LinqExpression CompileNew(NewExpression ne, CompilationContext ctx)
        {
            var clrType = ctx.Types[ne] is ClrType ? ((ClrType)ctx.Types[ne]).Type : null;
            if (clrType == null || !clrType.IsAnonymous())
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ne, ctx);
            }

            return LinqExpression.New(
                clrType.GetConstructors()[0],
                ne.Props.Values.Select(propExpr => Compile(propExpr, ctx)),
                ne.Props.Keys.Select(prop => clrType.GetProperty(prop)).ToArray());
        }

        private LinqExpression CompileLambda(LambdaExpression le, CompilationContext ctx)
        {
            var lambda = ctx.Types[le] as Lambda;
            if (lambda == null)
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, le, ctx);
            }

            var @params = le.Args.Select((arg, i) => 
                LinqExpression.Parameter(lambda.Type.GetFunctionDesc().Args.ElementAt(i), arg)).
                ToDictionary(param => param.Name);

            foreach (var name in @params.Keys)
            {
                if (Integration.IsRegisteredJS(name))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, le, ctx);
                }
                else if (ctx.Closure.ContainsKey(name))
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, le, ctx);
                }
                else
                {
                    ctx.Closure.Add(name, @params[name]);
                }
            }

            try
            {
                // todo. also yield cast to convert return types?
                return LinqExpression.Lambda(Compile(le.Body, ctx), @params.Values.ToArray());
            }
            finally
            {
                ctx.Closure.RemoveRange(@params.Keys);
            }
        }

        private LinqExpression CompileMemberAccess(MemberAccessExpression mae, CompilationContext ctx)
        {
            if (ctx.Types[mae] is ClrType)
            {
                var clrType = ((ClrType)ctx.Types[mae.Target]).Type;
                if (clrType.IsArray && mae.Name == "Length")
                {
                    return LinqExpression.ArrayLength(Compile(mae.Target, ctx));
                }
                else
                {
                    var fop = clrType.GetFieldOrProperty(mae.Name);
                    //no validation for fop != null and being only of 2 possible types - I'm sick & tired!
                    return fop is FieldInfo ?
                        LinqExpression.Field(Compile(mae.Target, ctx), (FieldInfo)fop) :
                        LinqExpression.Property(Compile(mae.Target, ctx), (PropertyInfo)fop);
                }
            }
            else if (ctx.Types[mae] is MethodGroup)
            {
                // we have three variants here:
                // 1) mae is a target of an Invoke -> note. processed, but not here
                // 2) mae is an argument of a call -> we need to emit a CreateDelegate stuff
                // 3) mae is a branch of a conditional -> same as 2

                var expected = AcquireExpectedTypeFromContext(mae, ctx);
                if (!expected.IsDelegate())
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, mae, ctx);
                }

                var createDelegate = typeof(Delegate).GetMethod("CreateDelegate", new []{
                    typeof(Type), typeof(Object), typeof(MethodInfo)});
                return LinqExpression.Call(createDelegate, new []{
                    LinqExpression.Constant(expected, typeof(Type)), 
                    Compile(mae.Target, ctx),
                    LinqExpression.Constant(ctx.Invocations[mae], typeof(MethodInfo))});
            }
            else
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, mae, ctx);
            }
        }

        private LinqExpression CompileInvoke(InvokeExpression ie, CompilationContext ctx)
        {
            var typeofTarget = ctx.Types[ie.Target];
            if (typeofTarget is ClrType)
            {
                var clrType = ((ClrType)typeofTarget).Type;
                if (clrType.IsDelegate())
                {
                    // not checking for a void-returning signature
                    return LinqExpression.Invoke(
                        Compile(ie.Target, ctx),
                        CompileCallArguments(ctx.Invocations[ie], ie.Args, ctx));
                }
                else
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ie, ctx);
                }
            }
            else if (typeofTarget is MethodGroup)
            {
                var target = ie.Target as MemberAccessExpression;
                if (target == null)
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ie, ctx);
                }

                var invocation = ctx.Invocations[ie];
                if (!invocation.Signature.IsExtension())
                {
                    // not checking for a void-returning signature
                    return LinqExpression.Call(
                        Compile(target.Target, ctx),
                        invocation.Signature,
                        CompileCallArguments(invocation, ie.Args, ctx));
                }
                else
                {
                    // not checking for a void-returning signature
                    var callArguments = target.Target.AsArray().Concat(ie.Args);
                    return LinqExpression.Call(
                        invocation.Signature,
                        CompileCallArguments(invocation, callArguments, ctx).ToArray());
                }
            }
            else
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ie, ctx);
            }
        }

        private LinqExpression CompileIndexer(IndexerExpression ie, CompilationContext ctx)
        {
            var typeofTarget = ctx.Types[ie.Target];
            if (typeofTarget is ClrType)
            {
                var clrType = ((ClrType)typeofTarget).Type;
                if (clrType.IsArray && clrType.GetArrayRank() == 1)
                {
                    // not checking whether the indexer expression has one and only operand
                    return LinqExpression.ArrayIndex(
                        Compile(ie.Target, ctx),
                        CompileCallArguments(ctx.Invocations[ie], ie.Operands, ctx));
                }
                else
                {
                    // not checking for a void-returning signature
                    // neither we check whether the sig is actually an indexer
                    return LinqExpression.Call(
                        Compile(ie.Target, ctx),
                        ctx.Invocations[ie].Signature,
                        CompileCallArguments(ctx.Invocations[ie], ie.Operands, ctx));
                }
            }
            else
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ie, ctx);
            }
        }

        private LinqExpression CompileOperator(OperatorExpression oe, CompilationContext ctx)
        {
            // not checking anything here. see comment in CompileConditional method's body

            // do not rely on oe.Type to detect the LINQ expression type, since
            // lookup/resolution might yield an operator with a different type
            // todo. validate this (LogicalNot -> OnesComplement, but not others)

            var sig = ctx.Invocations[oe].Signature;
            var sigTypes = sig.GetOperatorTypes();
            var actualOpType = sigTypes.Count() > 1 ? oe.Type : sigTypes.Single();

            if (!sig.IsRedirected())
            {
                var compiledOperands = CompileCallArguments(ctx.Invocations[oe], oe.Operands, ctx);

                var ctorArgs = compiledOperands.Cast<Object>();
                if (sig.IsUserDefinedOperator()) ctorArgs = ctorArgs.Concat(sig.AsArray());
                return actualOpType.LinqExpressionFactory()(ctorArgs.ToArray());
            }
            else
            {
                if (sig.IsLifted())
                {
                    // todo. implement this
                    throw new NotImplementedException(String.Format(
                        "Don't know how to compile the lifted operator: '{0}'", sig));
                }
                else
                {
                    var rebuiltSig = sig.RedirectionRoot();
                    var rebuiltCasts = ctx.Invocations[oe].Casts.Zip(
                        sig.RedirectionRoot().ToXArgs(oe.XArgsCount()), 
                        (cast, param) => Cast.Lookup(cast.Source, param));
                    // todo. the stuff above sucks since there's no cast from string to object

                    var compiledOperands = CompileCallArguments(
                        rebuiltSig, rebuiltCasts, oe.Operands, ctx);

                    var ctorArgs = compiledOperands.Cast<Object>();
                    ctorArgs = ctorArgs.Concat(rebuiltSig.AsArray());
                    return actualOpType.LinqExpressionFactory()(ctorArgs.ToArray());
                }
            }
        }

        private LinqExpression CompileConditional(ConditionalExpression ce, CompilationContext ctx)
        {
            // todo. conditional has to have a CLR type inferred.

            // cba to check this stuff
            var typeofTest = ctx.Types[ce.Test];
            var typeofFalse = ctx.Types[ce.IfFalse];
            var typeofTrue = ctx.Types[ce.IfTrue];

            var castTest = Cast.Lookup(typeofTest, typeof(bool));
            if (typeofTest is ClrType)
            {
                if (castTest == null)
                {
                    throw new CSharpBuilderException(
                        JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ce, ctx);
                }
            }
            else
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ce, ctx);
            }

            var f2tCast = Cast.Lookup(typeofFalse, typeofTrue);
            var t2fCast = Cast.Lookup(typeofTrue, typeofFalse);

            if (f2tCast == null && t2fCast == null)
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ce, ctx);
            }
            else if (f2tCast != null && t2fCast != null && typeofFalse != typeofTrue)
            {
                throw new CSharpBuilderException(
                    JSToCSharpExceptionType.UnexpectedInferredAst, Ast, ce, ctx);
            }

            return LinqExpression.Condition(
                CompileCast(castTest, ce.Test, ctx),
                f2tCast == null ? Compile(ce.IfFalse, ctx) : CompileCast(f2tCast, ce.IfFalse, ctx),
                t2fCast == null ? Compile(ce.IfTrue, ctx) : CompileCast(t2fCast, ce.IfTrue, ctx));
        }

        private Type AcquireExpectedTypeFromContext(RelinqScriptExpression e, CompilationContext ctx)
        {
            if (!e.Parent.IsCall())
            {
                if (e.Parent is ConditionalExpression)
                {
                    // conditional expression must have a CLR type inferred
                    // it is checked during its compilation which happens
                    // before its test or branches are compiled
                    // so here we can safely assume that fact
                    return e.ChildIndex == 0 ? typeof(bool) : ((ClrType)ctx.Types[e.Parent]).Type;
                }
                else
                {
                    throw new CannotResolveQuasiTypeFromContextException(Ast, e, ctx);
                }
            }
            else
            {
                if (e.ChildIndex == 0 &&
                    // even if we analyze a target of an extension method invocation,
                    // it still needs to be treated in a special way *elsewhere*
                    (e.Parent is IndexerExpression || e.Parent is InvokeExpression))
                {
                    throw new CannotResolveQuasiTypeFromContextException(Ast, e, ctx);
                }

                var sig = ctx.Invocations[e.Parent].Signature;
                var sigp = sig.GetParameters();

                var argIndex = sig.IsExtension() || sig.IsOperator() ? e.ChildIndex : e.ChildIndex - 1;
                argIndex = Math.Min(argIndex, sigp.Length - 1); // varargs

                var argType = sigp[argIndex].ParameterType;
                if (argIndex == sigp.Length - 1 && sig.IsVarargs()) // varargs
                    argType = argType.GetElementType();

                return argType;
            }
        }

        private IEnumerable<LinqExpression> CompileCallArguments(
            ResolvedInvocation invocation, IEnumerable<RelinqScriptExpression> args, CompilationContext ctx)
        {
            return CompileCallArguments(invocation.Signature, invocation.Casts, args, ctx);
        }

        private IEnumerable<LinqExpression> CompileCallArguments(
            MethodInfo sig, IEnumerable<Cast> casts, 
            IEnumerable<RelinqScriptExpression> args, CompilationContext ctx)
        {
            var sigp = sig.GetParameters();

            var instaCasts = sig.IsExtension() ? casts : casts.Skip(1);
            for (var i = 0; i < sigp.Length; ++i)
            {
                var cast = instaCasts.ElementAt(i);
                if (i != sigp.Length - 1 || !sig.IsVarargs())
                {
                    yield return CompileCast(cast, args.ElementAt(i), ctx);
                }
                else
                {
                    // cba to check whether the cast actually features a nice CLR type
                    var argType = ((ClrType)cast.Destination).Type;
                    yield return LinqExpression.NewArrayInit(
                        argType, args.Skip(sigp.Length - 1).Select(arg => CompileCast(cast, arg, ctx)));
                }
            }
        }

        private LinqExpression CompileCast(Cast cast, RelinqScriptExpression e, CompilationContext ctx)
        {
            // todo. int -> enum and string -> char casts need special treatment

            // todo. remove this hack that's necessary only for redirectto+concat to work
            if (cast == null && e.Parent is OperatorExpression &&
                ((OperatorExpression)e.Parent).Type == OperatorType.Add)
            {
                return Compile(e, ctx);
            }

            if (cast.Destination is ClrType)
            {
                if (cast.UserDefined == null)
                {
                    if (cast.Source is ClrType)
                    {
                        var ta = ((ClrType)cast.Source).Type;
                        var tp = ((ClrType)cast.Destination).Type;

                        if (tp.LookupInheritanceChain().Contains(typeof(LinqExpression)))
                        {
                            return LinqExpression.Quote(Compile(e, ctx));
                        }
                        else
                        {
                            if (ta.LookupInheritanceChain().Contains(tp))
                            {
                                if (ta.IsValueType && tp.IsReferenceType())
                                {
                                    return LinqExpression.Convert(Compile(e, ctx), tp);
                                }
                                else
                                {
                                    return Compile(e, ctx);
                                }
                            }
                            else
                            {
                                return LinqExpression.Convert(Compile(e, ctx), tp);
                            }
                        }
                    }
                    else if (cast.Source is Lambda)
                    {
                        var tp = ((ClrType)cast.Destination).Type;
                        if (tp.IsDelegate())
                        {
                            return Compile(e, ctx);
                        }
                        if (tp.LookupInheritanceChain().Contains(typeof(LinqExpression)))
                        {
                            return LinqExpression.Quote(Compile(e, ctx));
                        }
                    }
                    else if (cast.Source is MethodGroup)
                    {
                        throw new NotImplementedException(String.Format(
                            "Don't know how to compile the cast: '{0}'", cast));
                    }
                    else
                    {
                        if (cast.Source is Null || cast.Source is UnknownConstant)
                        {
                            return Compile(e, ctx);
                        }
                    }
                }
                else
                {
                    var ta = ((ClrType)cast.Source).Type;
                    var tp = ((ClrType)cast.Destination).Type;

                    // As a sidenote: C# expression trees fail to support spec-compliant u/d conversions 
                    // that consist of three step: implicit -> u/d -> implicit, 
                    // so all involved implicit conversions need to be created manually.

                    throw new NotImplementedException(String.Format(
                        "Don't know how to compile the cast: '{0}'", cast));
                }
            }

            throw new CSharpBuilderException(
                JSToCSharpExceptionType.UnexpectedInferredAst, Ast, e, ctx);
        }
    }
}