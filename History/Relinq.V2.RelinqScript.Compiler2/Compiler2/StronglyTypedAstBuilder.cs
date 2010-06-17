using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.V2.Helpers;
using Relinq.V2.Json;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.RelinqScript.Reflection.ForgedMethods.MethodForgers;
using Relinq.V2.RelinqScript.Reflection;
using Expression = System.Linq.Expressions.Expression;

namespace Relinq.V2.RelinqScript.Compiler2
{
    public class StronglyTypedAstBuilder
    {
        public Expression Visit(TypeInferenceUnit tiu)
        {
            if (tiu == null)
            {
                return null;
            } 
            else
            {
                if (!tiu.IsFullyInferred)
                {
                    throw new ArgumentException(String.Format(
                        "Type inference unit '{0}': underlying type not fully inferred", tiu));
                }
                else
                {
                    switch (tiu.NodeType)
                    {
                        case ExpressionType.Keyword:
                            return VisitKeyword((KeywordExpression)tiu);

                        case ExpressionType.Variable:
                            return VisitVariable((VariableExpression)tiu);

                        case ExpressionType.Literal:
                            return VisitLiteral((LiteralExpression)tiu);

                        case ExpressionType.Json:
                            return VisitJson((JsonExpression)tiu);

                        case ExpressionType.Lambda:
                            return VisitLambda((LambdaExpression)tiu);

                        case ExpressionType.Field:
                            return VisitField((FieldExpression)tiu);

                        case ExpressionType.Call:
                            return VisitCall((CallExpression)tiu);

                        case ExpressionType.Ternary:
                            return VisitTernary((ConditionalExpression)tiu);
                    }

                    throw new NotSupportedException(tiu.ToString());
                }
            }
        }

        public Expression VisitKeyword(KeywordExpression ke)
        {
            return Expression.Constant(CompilerKeywords.CreateInstance(ke.Name), ke.Type.RuntimeType);
        }

        private Dictionary<String, System.Linq.Expressions.ParameterExpression> _closure =
            new Dictionary<String, System.Linq.Expressions.ParameterExpression>();

        public Expression VisitVariable(VariableExpression ve)
        {
            if (!_closure.ContainsKey(ve.Name))
            {
                throw new ArgumentException(String.Format(
                    "Cannot find variable '{0}' in current context", ve));
            }
            else
            {
                return _closure[ve.Name];
            }
        }

        public Expression VisitLiteral(LiteralExpression le)
        {
            var value = JsonSerializer.Deserialize(le.Data, le.Type.RuntimeType);
            return Expression.Constant(value, le.Type.RuntimeType);
        }

        public Expression VisitJson(JsonExpression je)
        {
            var memberInits = new Dictionary<String, Expression>();
            foreach (var namedValue in je.Props)
                memberInits.Add(namedValue.Key, Visit(namedValue.Value));

            return Expression.New(
                je.Type.RuntimeType.GetConstructors()[0],
                memberInits.Values.ToArray(),
                memberInits.Keys.Select(name => je.Type.RuntimeType.GetFieldOrProperty(name)));
        }

        public Expression VisitLambda(LambdaExpression le)
        {
            var @params = new List<System.Linq.Expressions.ParameterExpression>();
            foreach (var arg in le.Args)
            {
                if (_closure.ContainsKey(arg))
                {
                    throw new ArgumentException(String.Format(
                        "Context already contains the '{0}' variable", arg));
                }
                else
                {
                    var parameter = Expression.Parameter(le.Lambda.Args[arg].RuntimeType, arg);
                    _closure.Add(arg, parameter);
                    @params.Add(parameter);
                }
            }

            try
            {
                return Expression.Lambda(Visit(le.Body), @params.ToArray());
            }
            finally
            {
                le.Args.ForEach(arg => _closure.Remove(arg));
            }
        }

        public Expression VisitField(FieldExpression fe)
        {
            var mi = fe.Target.Type.RuntimeType.GetFieldOrProperty(fe.Name);
            if (mi is FieldInfo)
            {
                return Expression.Field(Visit(fe.Target), (FieldInfo)mi);
            }
            else
            {
                return Expression.Property(Visit(fe.Target), (PropertyInfo)mi);
            }
        }

        // todo. it would be awesome to emit operators as MakeUnary/MakeBinary/Condition
        public Expression VisitCall(CallExpression ce)
        {
            var fuzzyMethod = ce.Method.Alternatives.Single();
            var runtimeMethod = fuzzyMethod.RuntimeMethod;

            var target = runtimeMethod.IsExtension() ? null : ce.Target;
            var args = runtimeMethod.IsExtension() ? new[] { ce.Target }.Union(ce.Args) : ce.Args;

            var linqTarget = Visit(target);
            var linqArgs = args.Select(arg => Visit(arg)).ToArray();

            // bug: wtf alert
            if (runtimeMethod.IsExtension())
            {
                // http://mitpress.mit.edu/sicp/full-text/sicp/book/node12.html
                //
                // Alyssa P. Hacker doesn't see why if needs to be provided as a special form. 
                // "Why can't I just define it as an ordinary procedure in terms of cond?" she asks. 
                // Alyssa's friend Eva Lu Ator claims this can indeed be done, and she defines a new version of if: 
                // (define (new-if predicate then-clause else-clause)
                //   (cond (predicate then-clause)
                //   (else else-clause)))
                //
                // What happens when Alyssa attempts to use the "new-if" stuff?

                if (runtimeMethod.SameMetadataToken(ForgedMethodsHost.PrimitiveAndImpl))
                {
                    return Expression.AndAlso(linqArgs.First(), linqArgs.Last());
                }
                else if (runtimeMethod.SameMetadataToken(ForgedMethodsHost.PrimitiveOrImpl))
                {
                    return Expression.OrElse(linqArgs.First(), linqArgs.Last());
                }
                else
                {
                    return Expression.Call(runtimeMethod, linqArgs);
                }
            }
            else if (runtimeMethod.IsStatic)
            {
                return Expression.Call(runtimeMethod, new[] {linqTarget}.Union(linqArgs).ToArray());
            }
            else
            {
                return Expression.Call(Visit(ce.Target), runtimeMethod, linqArgs);
            }
        }

        public Expression VisitTernary(ConditionalExpression te)
        {
            return Expression.Condition(Visit(te.Condition), Visit(te.IfTrue), Visit(te.IfFalse));
        }
    }
}
