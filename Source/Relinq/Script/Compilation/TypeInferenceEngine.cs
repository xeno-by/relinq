using System;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.Integration;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.TypeInference;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation.EngineAspects;
using Relinq.Script.Integration;
using Relinq.Script.Semantics.Closures;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.TypeSystem.Anonymous;
using Relinq.Script.Semantics.Lookup;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Syntax.Operators;

namespace Relinq.Script.Compilation
{
    public class TypeInferenceEngine
    {
        public TypeInferenceContext Ctx { get; private set; }
        public RelinqScriptExpression Root { get { return Ctx.Root; } }
        public TypeInferenceCache Inferences { get { return Ctx.Inferences; } }
        public IntegrationContext Integration { get { return Ctx.Integration; } }

        public TypeInferenceEngine(RelinqScriptExpression ast, IntegrationContext integration)
        {
            Ctx = new TypeInferenceContext(ast, new TypeInferenceCache(), integration);
        }

        public TypeInferenceEngine(TypeInferenceContext external)
        {
            Ctx = external.Clone();
        }

        public TypeInferenceCache InferTypes()
        {
            InferTypes(Root, Inferences);
            return Inferences;
        }

        public TypeInferenceCache InferTypes(RelinqScriptExpression e)
        {
            InferTypes(e, Inferences);
            return Inferences;
        }

        public static TypeInferenceProxy _sap = 
            new TypeInferenceProxy(new TypeInferenceEngineCache());

        private void InferTypes(RelinqScriptExpression e, TypeInferenceCache cache)
        {
            _sap.InferTypes(e, cache, InferTypesImpl);
        }

        private void InferTypesImpl(RelinqScriptExpression e, TypeInferenceCache cache)
        {
            try
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Keyword:
                        InferKeyword((KeywordExpression)e, cache);
                        break;

                    case ExpressionType.Variable:
                        InferVariable((VariableExpression)e, cache);
                        break;

                    case ExpressionType.Constant:
                        InferConstant((ConstantExpression)e, cache);
                        break;

                    case ExpressionType.New:
                        InferNew((NewExpression)e, cache);
                        break;

                    case ExpressionType.Lambda:
                        InferLambda((LambdaExpression)e, cache);
                        break;

                    case ExpressionType.MemberAccess:
                        InferMemberAccess((MemberAccessExpression)e, cache);
                        break;

                    case ExpressionType.Invoke:
                        InferInvoke((InvokeExpression)e, cache);
                        break;

                    case ExpressionType.Indexer:
                        InferIndexer((IndexerExpression)e, cache);
                        break;

                    case ExpressionType.Operator:
                        InferOperator((OperatorExpression)e, cache);
                        break;

                    case ExpressionType.Conditional:
                        InferConditional((ConditionalExpression)e, cache);
                        break;

                    default:
                        throw new TypeInferenceException(
                            JSToCSharpExceptionType.UnexpectedNodeType, Root, e);
                }
            }
            catch (TypeInferenceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TypeInferenceException(
                    JSToCSharpExceptionType.Unexpected, Root, e, ex);
            }
        }

        private void InferKeyword(KeywordExpression ke, TypeInferenceCache cache)
        {
            try
            {
                cache.Add(ke, Integration.ProduceCSharp(ke.Name).GetType());
            }
            catch (JSToCSharpIntegrationException jsex)
            {
                throw new TypeInferenceException(JSToCSharpExceptionType.Integration, Root, ke, jsex);
            }
        }

        private void InferVariable(VariableExpression ve, TypeInferenceCache cache)
        {
            if (ve.GetClosure().ContainsKey(ve.Name))
            {
                var lambda = ve.GetClosure()[ve.Name];
                var funcType = ((Lambda)cache[lambda]).Type;
                var argIndex = Array.IndexOf(lambda.Args.ToArray(), ve.Name);
                cache.Add(ve, funcType.GetFunctionDesc().Args.ElementAt(argIndex));
            }
            else
            {
                throw new UndeclaredVariableException(Root, ve, ve.GetClosure());
            }
        }

        private void InferConstant(ConstantExpression ce, TypeInferenceCache cache)
        {
            var inferred = TypeInferenceConstants.InferType(ce);
            if (inferred != null)
            {
                cache.Add(ce, inferred);
            }
            else
            {
                throw new ConstantInferenceFailedException(Root, ce);
            }
        }

        private void InferNew(NewExpression ne, TypeInferenceCache cache)
        {
            ne.Props.Values.ForEach(e => InferTypes(e, cache));

            var propTypes = ne.Props.Values.Select(e => cache[e]);
            if (propTypes.All(t => t is ClrType))
            {
                var clrPropTypes = propTypes.Cast<ClrType>().Select(pt => pt.Type);
                cache.Add(ne, AnonymousTypesHelper.ForgeTupleType(ne.Props.Keys, clrPropTypes));
            }
            else
            {
                var fail = Array.FindIndex(propTypes.ToArray(), 
                    t => !(t is ClrType || t is Variant));

                if (fail == -1)
                {
                    cache.Add(ne, new Variant());
                }
                else
                {
                    throw new CannotForgeAnonymousTypeException(
                        Root, ne, ne.Props.Keys.ElementAt(fail), propTypes.ElementAt(fail));
                }
            }
        }

        private void InferLambda(LambdaExpression le, TypeInferenceCache cache)
        {
            // lambda is the only one expression that can have its type set from outside
            if (!cache.ContainsKey(le))
            {
                var lambdaType = Enumerable.Repeat(typeof(Variant), 1 + le.Args.Count()).ForgeFuncType();
                cache.Add(le, new Lambda(le, lambdaType));
            }

            // detect overlapping variables
            var closure = le.Parent == null ? null : le.Parent.GetClosure();
            if (closure != null)
            {
                var overlapping = closure.Keys.Intersect(le.Args);
                if (overlapping.IsNotEmpty())
                {
                    throw new RedeclaredVariableException(Root, le, closure);
                }
            }

            // detect overriding keywords
            foreach (var arg in le.Args)
            {
                if (Integration.IsRegisteredJS(arg))
                {
                    throw new VariableOverridesKeywordException(Root, le, arg);
                }
            }

            InferTypes(le.Body, cache);
        }

        private void InferMemberAccess(MemberAccessExpression mae, TypeInferenceCache cache)
        {
            InferTypes(mae.Target, cache);
            var typeofTarget = cache[mae.Target];

            if (typeofTarget is ClrType)
            {
                // nb: the member access also covers method group lookup
                // i.e. object.Method(arg1, arg2) is in fact resolved in two steps:
                // 1) object.Method -> resolves to all instance or extension methods
                //    (on this step fields and properties are omitted because the expression
                //    is being used in the context of an invocation)
                // 2) MG(arg1, arg2) -> picks up the best method from the MG that matches arglist

                var usedInContextOfInvocation = mae.Parent is InvokeExpression && mae.ChildIndex == 0;
                cache.Add(mae, usedInContextOfInvocation ? 
                    typeofTarget.LookupMethodGroup(mae.Name) : 
                    typeofTarget.LookupMemberAccess(mae.Name));

                if (cache[mae] == null)
                {
                    throw new NoSuchMemberException(Root, mae, typeofTarget);
                }
            }
            else if (typeofTarget is Variant)
            {
                cache.Add(mae, new Variant());
            }
            else
            {
                throw new NoSuchMemberException(Root, mae, typeofTarget);
            }
        }

        private void InferInvoke(InvokeExpression ie, TypeInferenceCache cache)
        {
            InferTypes(ie.Target, cache);
            var typeofTarget = cache[ie.Target];

            if (typeofTarget is ClrType)
            {
                var preview = cache.Clone();
                ie.Args.ForEach(arg => InferTypes(arg, preview));
                var types = ie.Args.Select(arg => preview[arg]);

                if (types.Any(type => type is Variant))
                {
                    cache.Add(ie, new Variant());
                    cache.Upgrade(preview);
                }
                else
                {
                    var clrType = ((ClrType)typeofTarget).Type;
                    if (clrType.IsDelegate())
                    {
                        var sig = clrType.GetFunctionSignature();
                        InferMethodGroup(new MethodGroup(sig.AsArray(), clrType.Name), ie, cache);
                    }
                    else
                    {
                        throw new CannotBeInvokedException(Root, ie, typeofTarget);
                    }
                }
            }
            else if (typeofTarget is MethodGroup)
            {
                InferMethodGroup((MethodGroup)typeofTarget, ie, cache);
            }
            else if (typeofTarget is Variant)
            {
                cache.Add(ie, new Variant());
            }
            else
            {
                throw new CannotBeInvokedException(Root, ie, typeofTarget);
            }
        }

        private void InferIndexer(IndexerExpression ie, TypeInferenceCache cache)
        {
            InferTypes(ie.Target, cache);
            var typeofTarget = cache[ie.Target];

            if (typeofTarget is ClrType)
            {
                var preview = cache.Clone();
                ie.Operands.ForEach(operand => InferTypes(operand, preview));
                var types = ie.Operands.Select(operand => preview[operand]);

                if (types.Any(type => type is Variant))
                {
                    cache.Add(ie, new Variant());
                    cache.Upgrade(preview);
                }
                else
                {
                    var alts = typeofTarget.LookupIndexers();
                    if (alts == null)
                    {
                        throw new NoSuchIndexerException(Root, ie, typeofTarget);
                    }

                    InferMethodGroup(alts, ie, cache);
                }
            }
            else if (typeofTarget is Variant)
            {
                cache.Add(ie, new Variant());
            }
            else
            {
                throw new NoSuchIndexerException(Root, ie, typeofTarget);
            }
        }

        private void InferOperator(OperatorExpression oe, TypeInferenceCache cache)
        {
            var preview = cache.Clone();
            oe.Operands.ForEach(operand => InferTypes(operand, preview));
            var types = oe.Operands.Select(operand => preview[operand]);

            if (types.Any(type => type is Variant))
            {
                cache.Add(oe, new Variant());
                cache.Upgrade(preview);
            }
            else
            {
                var alts = oe.Type.LookupOperators(types.ToArray());

                // logical not can also be used to express ones complement
                // since they both correspond to a single LINQ expression type
                if (oe.Type == OperatorType.LogicalNot)
                {
                    var addendum = OperatorType.OnesComplement.LookupOperators(types.ToArray());
                    if (addendum != null)
                    {
                        var original = alts == null ? new MethodInfo[0] : alts.Alts;
                        alts = new MethodGroup(original.Concat(addendum.Alts), oe.Type.GetOpCode());
                    }
                }

                if (alts == null)
                {
                    throw new NoSuchOperatorException(Root, oe, types);
                }

                InferMethodGroup(alts, oe, cache);
            }
        }

        private void InferMethodGroup(MethodGroup mg, RelinqScriptExpression root, TypeInferenceCache cache)
        {
            // q: should existing but not accessible (security!) methods be included into the resolution?
            // a: yes and here's why:
            // scenario 1. Included; when overload resolution binds to an unauthorized method = crash.
            // scenario 2. Not included, so overload resolution binds to an unexpected method = fail.

            // lets us the fail fast if the resolution is unnecessary.
            // inferences made here won't be wasted anyways since they get cached
            var ctx = new TypeInferenceContext(root, cache, Integration);
            var preview = new TypeInferenceEngine(ctx);
            root.CallArgs().ForEach(child => preview.InferTypes(child));

            // we cannot pass the preview.Ctx inside since it might have
            // potentially half-inferred lambdas that won't be able to be reinferred
            // by MG resolution since ctx is init only.
            var resolved = mg.Resolve(ctx);

            cache.Add(root, resolved.Signature.ReturnType);
            cache.Invocations.Add(root, resolved);
            cache.Upgrade(resolved.Inferences);
        }

        private void InferConditional(ConditionalExpression ce, TypeInferenceCache cache)
        {
            ce.Children.ForEach(child => InferTypes(child, cache));
            var typeofTest = cache[ce.Test];
            var typeofFalse = cache[ce.IfFalse];
            var typeofTrue = cache[ce.IfTrue];

            if (typeofTest is Variant || typeofFalse is Variant || typeofTrue is Variant)
            {
                cache.Add(ce, new Variant());
            }
            else
            {
                if (!(typeofTest is ClrType &&
                    typeofTest.HasImplicitCastTo(typeof(bool))))
                {
                    throw new InconsistentConditionalExpression(
                        JSToCSharpExceptionType.ConditionalTestInvalidType,
                        Root, ce, typeofTest, typeofTrue, typeofFalse);
                }

                var f2t = typeofFalse.HasImplicitCastTo(typeofTrue);
                var t2f = typeofTrue.HasImplicitCastTo(typeofFalse);

                if (!f2t && !t2f)
                {
                    if (typeofFalse is Lambda || typeofTrue is Lambda)
                    {
                        throw new NotImplementedException(String.Format(
                            "Don't know how to infer the '{0} ? {1} : {2}' conditional expression.",
                            typeofTest, typeofTrue, typeofFalse));
                    }
                    else
                    {
                        throw new InconsistentConditionalExpression(
                            JSToCSharpExceptionType.ConditionalClausesNoCommonTypeWithOnlyOneClauseBeingCast,
                            Root, ce, typeofTest, typeofTrue, typeofFalse);
                    }
                }
                else if (f2t && t2f && typeofFalse != typeofTrue)
                {
                    throw new InconsistentConditionalExpression(
                        JSToCSharpExceptionType.ConditionalClausesAmbiguousCommonType,
                        Root, ce, typeofTest, typeofTrue, typeofFalse);
                }
                else
                {
                    cache.Add(ce, f2t ? typeofTrue : typeofFalse);
                }
            }
        }
    }
}