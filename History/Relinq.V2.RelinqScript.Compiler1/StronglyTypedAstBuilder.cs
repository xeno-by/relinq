using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Relinq.V2.Helpers;
using Relinq.V2.RelinqScript.Grammar.Literals;
using Relinq.V2.RelinqScript.Compiler1.TypeInference_Old;
using Relinq.V2.RelinqScript.Grammar.Expressions;
using Relinq.V2.RelinqScript.Reflection;
using LinqExpression=System.Linq.Expressions.Expression;
using LinqParameterExpression = System.Linq.Expressions.ParameterExpression;

namespace Relinq.V2.RelinqScript.Compiler1
{
    public class StronglyTypedAstBuilder
    {
        #region Execution Context

        private Closure _closure = new Closure();

        public StronglyTypedAstBuilder()
        {
            _closure.Add("null", LinqExpression.Constant(null));
            _closure.Add("true", LinqExpression.Constant(true));
            _closure.Add("false", LinqExpression.Constant(false));
            _closure.Add("ctx", LinqExpression.Constant(CompilerKeywords.GetInstance("ctx")));
        }

        public StronglyTypedAstBuilder(Closure externalClosure)
        {
            foreach (var pair in externalClosure)
            {
                _closure.Add(pair.Key, pair.Value);
            }
        }

        #endregion

        public virtual LinqExpression Visit(RelinqScriptExpression astNode)
        {
            switch (astNode.NodeType)
            {
                case ExpressionType.Keyword:
                case ExpressionType.Variable:
                    return VisitObjectReference(astNode);

                case ExpressionType.Constant:
                    return VisitConstant((ConstantExpression)astNode, null);

                case ExpressionType.New:
                    return VisitNew((NewExpression)astNode);

                case ExpressionType.Lambda:
                    return VisitLambda((LambdaExpression)astNode, null);

                case ExpressionType.Field:
                    return VisitField((FieldExpression)astNode);

                case ExpressionType.Invoke:
                    return VisitInvoke((InvokeExpression)astNode);

                case ExpressionType.Indexer:
                    return VisitIndexer((IndexerExpression)astNode);

                case ExpressionType.Operator:
                    return VisitOperator((OperatorExpression)astNode);

                case ExpressionType.Conditional:
                    return VisitConditional((ConditionalExpression)astNode);
            }

            throw new NotSupportedException(astNode.ToString());
        }

        public virtual LinqExpression VisitInvoke(InvokeExpression call)
        {
            var resolved = ResolveMethod(null, call);
            if (resolved == null)
            {
                // Screw extension methods other than from Queryable and Enumerable
                resolved = ResolveMethod(typeof(Queryable), call);
                resolved = resolved ?? ResolveMethod(typeof(Enumerable), call);
            }

            if (resolved != null)
            {
                return resolved;
            }
            else
            {
                throw new NotSupportedException(call.ToString());
            }
        }

        public virtual LinqExpression VisitIndexer(IndexerExpression ind)
        {
            throw new NotImplementedException(ind.ToString());
        }

        public virtual LinqExpression VisitOperator(OperatorExpression op)
        {
            throw new NotImplementedException(op.ToString());
        }

        public virtual LinqExpression VisitConditional(ConditionalExpression cond)
        {
            // todo: add conversions here
            throw new NotImplementedException(cond.ToString());
        }

        private LinqExpression ResolveMethod(Type hostClass, InvokeExpression call)
        {
            throw new NotImplementedException(call.ToString());
//            var isExtension = hostClass != null;
//            if (isExtension)
//            {
//                var args = new List<RelinqScriptExpression>(call.Args);
//                args.Insert(0, call.Target);
//                call = new InvokeExpression(call.Name, new ConstantExpression("null"), args);
//            }
//
//            hostClass = hostClass ?? Visit(call.Target).Type;
//            var mis = isExtension ?
//                hostClass.GetMethods(BindingFlags.Static | BindingFlags.Public) :
//                // Only instance methods for non-extension invocations
//                hostClass.GetMethods(BindingFlags.Instance | BindingFlags.Public);
//
//            var mrs = mis
//                .Where(mi => mi.Name == call.Name)
//                .Where(mi => !isExtension || mi.IsExtension())
//                .Select(mi => new MethodResolution(mi, _closure, call));
//
//            // First pass matches formal and actual (a.k.a. "real") arguments using available types.
//            // While matching it also tries to infer generic arguments of the method.
//            //
//            // Since typeinfo in JS is quite often incomplete the following arguments will not be used
//            // for generic arguments inference:
//            //  * Function-type parameters (tho matches arg count of an underlying delegate)
//            //  * Object definitions (when used as func arguments they need type info in order to be inferred)
//            //
//            // Second pass iteratively applies  
//
//            var success = mrs.SingleOrDefault(mr => mr.Resolve(ResolutionPass.First) != false);
//            if (success != null)
//            {
//                success.Resolve(ResolutionPass.Second);
//                return success.ResolvedCall;
//            }
//            else
//            {
//                return null;
//            }
        }

        public virtual LinqExpression VisitField(FieldExpression field)
        {
            var target = Visit(field.Target);
            var mi = target.Type.GetFieldOrProperty(field.Name);

            if (mi is FieldInfo)
            {
                return LinqExpression.Field(target, (FieldInfo)mi);
            }

            if (mi is PropertyInfo)
            {
                return LinqExpression.Property(target, (PropertyInfo)mi);
            }

            throw new NotSupportedException(field.ToString());
        }

        public virtual LinqExpression VisitLambda(LambdaExpression astNode, Type[] argTypes)
        {
            return VisitLambda(astNode, argTypes, false);
        }

        public virtual LinqExpression VisitLambda(LambdaExpression lambda, Type[] argTypes, bool firstPassOfMethodResolution)
        {
            if (argTypes == null)
            {
                if (!firstPassOfMethodResolution)
                {
                    throw new NotSupportedException("Cannot build lambda expression with no type info provided");
                }
                else
                {
                    var args = new List<LinqParameterExpression>();
                    lambda.Args.ForEach(arg => args.Add(LinqExpression.Parameter(typeof(object), arg)));
                    return LinqExpression.Lambda(LinqExpression.Constant(null), args.ToArray());
                }
            }
            else
            {
                // fuck varargs for now
                if (lambda.Args.Count() != argTypes.Length)
                    throw new ArgumentException("Number of inferred types doesn't match args count");

                var @params = new List<LinqParameterExpression>();
                for (var i = 0; i < lambda.Args.ToArray().Length; i++)
                {
                    var arg = lambda.Args.ToArray()[i];
                    if (!_closure.ContainsKey(arg))
                    {
                        _closure.Add(arg, LinqExpression.Parameter(argTypes[i], arg));
                        @params.Add((LinqParameterExpression) _closure[arg]);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format(
                            "Lambda parameter '{0}' is already defined within this closure.", arg));
                    }
                }

                try
                {
                    return LinqExpression.Lambda(Visit(lambda.Body), @params.ToArray());
                }
                finally
                {
                    lambda.Args.ForEach(arg => _closure.Remove(arg));
                }
            }
        }

        public virtual LinqExpression VisitObjectReference(RelinqScriptExpression astNode)
        {
            String refName;
            switch (astNode.NodeType)
            {
                case ExpressionType.Keyword:
                    refName = ((KeywordExpression)astNode).Name;
                    break;
                case ExpressionType.Variable:
                    refName = ((VariableExpression)astNode).Name;
                    break;
                default:
                    throw new NotSupportedException(astNode.ToString());
            }

            return _closure[refName];
        }

        public virtual LinqExpression VisitNew(NewExpression astNode)
        {
//            var tupleArgs = astNode.Props.Values.Select(prop => Visit(prop));
//            var tupleType = AnonymousTypesHelper.ForgeTupleType(tupleArgs.Select(arg => arg.Type).ToArray());
//
//            return LinqExpression.New(tupleType.GetConstructors()[0], tupleArgs,
//                AnonymousTypesHelper.ForgeTupleMemberInfos(tupleType, astNode.Props.Keys.ToArray()));
            throw new NotSupportedException();
        }

        public virtual LinqExpression VisitConstant(ConstantExpression astNode, Type expectedType)
        {
            return VisitConstant(astNode, expectedType, false);
        }

        public virtual LinqExpression VisitConstant(ConstantExpression astNode, Type expectedType, bool firstPassOfMethodResolution)
        {
            if (expectedType == null)
                expectedType = LiteralTypeResolution.InferType(astNode.Data);

            var @object = JsonSerializer.Deserialize(astNode.Data, expectedType);
            return LinqExpression.Constant(@object, expectedType);
        }
    }
}