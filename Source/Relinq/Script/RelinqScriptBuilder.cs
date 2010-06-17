using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;
using Relinq.Exceptions.CSharpToJS;
using Relinq.Exceptions.LinqVisitor;
using Relinq.Helpers.Assurance;
using Relinq.Linq;
using Relinq.Script.Integration;
using Relinq.Script.Semantics.Literals;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Script
{
    public class RelinqScriptBuilder : LinqVisitor
    {
        public StringBuilder Script { get; set; }
        private IntegrationContext Integration { get; set; }

        public RelinqScriptBuilder(Expression root, IntegrationContext integration)
            : base(root)
        {
            Script = new StringBuilder();
            Integration = integration;

            TransparentIdsCounter = 0;
            TransparentToHumanFriendly = new Dictionary<string, string>();
        }

        private T Fail<T>(T node, CSharpToJSExceptionType type)
        {
            // can't use "node is Expression" since the node can be null
            if (typeof(Expression).IsAssignableFrom(typeof(T)))
            {
                var expression = node as Expression;
                throw new ScriptBuilderException(type, Root, expression);
            }
            if (typeof(MemberBinding).IsAssignableFrom(typeof(T)))
            {
                var binding = node as MemberBinding;
                throw new ScriptBuilderException(type, Root, binding);
            }
            else
            {
                throw new UnexpectedNodeException(node, typeof(T));
            }
        }

        protected override Expression VisitExpression(Expression exp)
        {
            if (exp == null)
            {
                Script.Append("null");

                return null;
            }
            else
            {
                var needsBrackets = ExpressionNeedsBrackets(Current, Parent);
                if (needsBrackets) Script.Append("(");
                var returnExp = base.VisitExpression(exp);
                if (needsBrackets) Script.Append(")");

                return returnExp;
            }
        }

        private bool ExpressionNeedsBrackets(Expression exp, Expression context)
        {
            if (exp == null || context == null)
            {
                return false;
            }
            else
            {
                var expCaresAboutPrio = exp.HasPriority();
                var contextCaresAboutPrio = context.HasPriority() &&
                    !(context.NodeType == ExpressionType.ArrayIndex && ((BinaryExpression)context).Right == exp) &&
                    !(context.NodeType == ExpressionType.Call && ((MethodCallExpression)context).Arguments.Contains(exp)) &&
                    !(context.NodeType == ExpressionType.Invoke && ((InvocationExpression)context).Arguments.Contains(exp));

                var needsBracketsDueToPrio = expCaresAboutPrio && (
                    context.IsCast() ? ExpressionNeedsBrackets(exp, FirstNonConvertParent(exp)) :
                    (
                        contextCaresAboutPrio &&
                        (
                            (
                                exp.GetPriority() < ClrOperators.UnaryOpsPriority && 
                                context.GetPriority() >= exp.GetPriority()
                            ) 
                            ||
                            (   
                                exp.GetPriority() == ClrOperators.PrimaryOpsPriority && 
                                context.GetPriority() > exp.GetPriority()
                            )
                        ))
                    );

                var needsBracketsToDisambiguateSyntax =
                    (exp.NodeType == ExpressionType.Negate && context.NodeType == ExpressionType.Negate) ||
                    (exp.NodeType == ExpressionType.UnaryPlus && context.NodeType == ExpressionType.UnaryPlus);

                // stuff with NewExpression is a workaround for the bug in ES3 parser
                return needsBracketsDueToPrio || needsBracketsToDisambiguateSyntax || (contextCaresAboutPrio && exp is NewExpression);
            }
        }

        private Expression FirstNonConvertParent(Expression exp)
        {
            (exp == Current).SurelyTrue();
            for (var i = 0; NthAncestor(i) != null; ++i)
                if (!NthAncestor(i).IsCast()) return NthAncestor(i);
            return null;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Divide:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                    Visit(b.Left);
                    Script.AppendFormat(" {0} ", b.GetOpCode());
                    Visit(b.Right);
                    return b;

                case ExpressionType.ArrayIndex:
                    Visit(b.Left);
                    Script.Append("[");
                    Visit(b.Right);
                    Script.Append("]");
                    return b;

                case ExpressionType.AddChecked:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.SubtractChecked:
                    return Fail(b, CSharpToJSExceptionType.CheckedArithmetic);

//                case ExpressionType.Coalesce:
                default:
                    return Fail(b, CSharpToJSExceptionType.Unexpected);
            }
        }

        protected override MemberBinding VisitBinding(MemberBinding binding)
        {
            return Fail(binding, CSharpToJSExceptionType.Binding);
        }

        protected override Expression VisitConditional(ConditionalExpression c)
        {
            Visit(c.Test);
            Script.Append(" ? ");
            Visit(c.IfTrue);
            Script.Append(" : ");
            Visit(c.IfFalse);
            return c;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (Integration.IsRegisteredCSharp(c.Value))
            {
                Script.Append(Integration.ProduceJS(c.Value));
            }
            else
            {
                var parentIsRelationOp = Parent.NodeType.IsRelational();
                var binaryParent = Parent as BinaryExpression;
                var anotherOperand = binaryParent == null ? null : (binaryParent.Left == c ? binaryParent.Right : binaryParent.Left);
                var anotherOperandIsCastCharOrLifted = anotherOperand == null ? false : (anotherOperand.IsCast() &&
                    ((UnaryExpression)anotherOperand).Operand.Type.IsTOrNullableT<char>());

                // this hack is used to serialize character relational operators in the way they were most likely written
                // point is that a C# code "s[0] == 'c'" will be transformed to a fucked up version: "(int)s[0] == 99"
                // everything complies to the spec, just it looks not good. the stuff below works arounds this peculiarity
                if (parentIsRelationOp && anotherOperandIsCastCharOrLifted)
                {
                    // if one writes simply "(char)c.Value" the entire thing will crash
                    Script.Append(JsonSerializer.Serialize((char)(int)c.Value, typeof(char)));
                }
                else
                {
                    Type expectedType;
                    if (Parent.NodeType == ExpressionType.Call && ((MethodCallExpression)Parent).Arguments.Contains(c))
                    {
                        var mce = (MethodCallExpression)Parent;
                        var paramIndex = mce.Arguments.IndexOf(c);

                        var isvarargs = mce.Method.IsVarargs() && mce.Method.GetParameters().Length < mce.Arguments.Count;
                        var isvararg = isvarargs && paramIndex >= mce.Method.GetParameters().Length;
                        if (isvararg) paramIndex = mce.Method.GetParameters().Length - 1;

                        expectedType = mce.Method.GetParameters()[paramIndex].ParameterType;
                        if (isvararg) expectedType = expectedType.GetElementType();
                    }
                    else if (Parent is UnaryExpression && ((UnaryExpression)Parent).Method != null)
                    {
                        var ue = (UnaryExpression)Parent;
                        expectedType = ue.Method.GetParameters().Single().ParameterType;
                    }
                    else if (Parent is BinaryExpression && ((BinaryExpression)Parent).Method != null)
                    {
                        var be = (BinaryExpression)Parent;
                        var paramIndex = be.Left == c ? 0 : 1;
                        expectedType = be.Method.GetParameters().ElementAt(paramIndex).ParameterType;
                    }
                    else
                    {
                        expectedType = c.Type;
                    }

                    Script.Append(JsonSerializer.Serialize(c.Value, expectedType));
                }
            }

            return c;
        }

        protected override Expression VisitInvocation(InvocationExpression iv)
        {
            Visit(iv.Expression);
            Script.Append("(");

            foreach (var arg in iv.Arguments)
            {
                var prevSymbol = Script[Script.Length - 1];
                if (prevSymbol != '(' && prevSymbol != '[') Script.Append(", ");
                Visit(arg);
            }

            Script.Append(")");
            return iv;
        }

        protected override Expression VisitLambda(LambdaExpression lambda)
        {
            Script.Append("function(");

            for (var i = 0; i < lambda.Parameters.Count; ++i)
            {
                var parameter = lambda.Parameters[i];
                Visit(parameter);
                if (i != lambda.Parameters.Count - 1) Script.Append(", ");
            }

            Script.Append("){return ");
            Visit(lambda.Body);
            Script.Append("}");

            return lambda;
        }

        protected override Expression VisitListInit(ListInitExpression init)
        {
            return Fail(init, CSharpToJSExceptionType.NotAnonymousCtor);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Member.IsStatic())
            {
                return Fail(m, CSharpToJSExceptionType.StaticMember);
            }
            else
            {
                Visit(m.Expression);
                Script.AppendFormat(".{0}", GetHumanFriendlyName(m.Member.Name));
                return m;
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.IsExtension())
            {
                Visit(m.Arguments[0]);
                Script.AppendFormat(".{0}(", m.Method.Name);
                VisitMethodArgs(m);
                Script.Append(")");
            }
            else
            {
                if (m.Method.IsStatic)
                {
                    return Fail(m, CSharpToJSExceptionType.StaticMember);
                }
                else
                {
                    Visit(m.Object);

                    if (m.Method.IsIndexer()) Script.Append("[");
                    else Script.AppendFormat(".{0}(", m.Method.Name);

                    VisitMethodArgs(m);

                    if (m.Method.IsIndexer()) Script.Append("]");
                    else Script.Append(")");
                }
            }

            return m;
        }

        private void VisitMethodArgs(MethodCallExpression m)
        {
            var startIndex = m.Method.IsExtension() ? 1 : 0;
            for (var i = startIndex; i < m.Arguments.Count; ++i)
            {
                var arg = m.Arguments[i];
                if (m.Method.IsVarargs() && i == m.Arguments.Count - 1)
                {
                    if (arg.NodeType == ExpressionType.NewArrayInit)
                    {
                        ((NewArrayExpression)arg).Expressions.ForEach(vararg => {
                            Script.Append(", ");
                            Visit(vararg);
                        });
                    }
                    else
                    {
                        Fail(m, CSharpToJSExceptionType.Unexpected);
                    }
                }
                else
                {
                    if (i != startIndex) Script.Append(", ");
                    Visit(arg);
                }
            }
        }

        protected override NewExpression VisitNew(NewExpression nex)
        {
            if (nex.Constructor.DeclaringType.IsAnonymous())
            {
                Script.Append("{");

                for (var i = 0; i < nex.Members.Count; i++)
                {
                    var member = nex.Members[i];
                    var arg = nex.Arguments[i];

                    if (Script[Script.Length - 1] != '{') Script.Append(", ");
                    var memberName = GetHumanFriendlyName(member.Name);
                    Script.AppendFormat("{0} : ", memberName.StartsWith("get_") ? memberName.Substring(4) : memberName);
                    Visit(arg);
                }

                Script.Append("}");
                return nex;
            }
            else
            {
                return Fail(nex, CSharpToJSExceptionType.NotAnonymousCtor);
            }
        }

        protected override Expression VisitNewArray(NewArrayExpression na)
        {
            return Fail(na, CSharpToJSExceptionType.NotAnonymousCtor);
        }

        private int TransparentIdsCounter { get; set; }
        private Dictionary<String, String> TransparentToHumanFriendly { get; set; }

        private String GetHumanFriendlyName(String name)
        {
            if (name.IsTransparentIdentifier())
            {
                if (!TransparentToHumanFriendly.ContainsKey(name))
                {
                    TransparentToHumanFriendly.Add(name, "$" + TransparentIdsCounter++);
                }

                return TransparentToHumanFriendly[name];
            }
            else
            {
                return name;
            }
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            Script.Append(GetHumanFriendlyName(p.Name));
            return p;
        }

        protected override Expression VisitTypeIs(TypeBinaryExpression b)
        {
            return Fail(b, CSharpToJSExceptionType.ExplicitTypeRelatedOperation);
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.Not:
                case ExpressionType.UnaryPlus:
                    Script.Append(u.GetOpCode());
                    Visit(u.Operand);
                    return u;

                case ExpressionType.Quote:
                    if (u.Operand is LambdaExpression)
                    {
                        return Visit(u.Operand);
                    }
                    else
                    {
                        return Fail(u, CSharpToJSExceptionType.Unexpected);
                    }

                case ExpressionType.ArrayLength:
                    Visit(u.Operand);
                    Script.Append(".Length");
                    return u;

                case ExpressionType.Convert:
                    if (u.Operand is MethodCallExpression && 
                        ((MethodCallExpression)u.Operand).Method == typeof(Delegate).GetMethod(
                            "CreateDelegate", 
                            new Type[]{typeof(Type), typeof(object), typeof(MethodInfo)}))
                    {
                        var mce = (MethodCallExpression)u.Operand;
                        if (mce.Arguments[2] is ConstantExpression)
                        {
                            var target = mce.Arguments[1];
                            var methodGroup = ((MethodInfo)((ConstantExpression)mce.Arguments[2]).Value).Name;
                            var parenthesize = target.HasPriority() && target.GetPriority() < mce.GetPriority();

                            if (parenthesize) Script.Append("(");
                            _expressionStack.Insert(0, mce);
                            Visit(target);
                            _expressionStack.RemoveAt(0); 
                            if (parenthesize) Script.Append(")");
                            Script.Append(".").Append(methodGroup);

                            return u;
                        }
                        else
                        {
                            return Fail(u, CSharpToJSExceptionType.Unexpected);
                        }
                    }
                    else
                    {
                        if (u.IsImplicitCast())
                        {
                            int nonCastIndex;
                            if (!Parent.IsCast())
                            {
                                nonCastIndex = 0;
                            }
                            else
                            {
                                if (u.IsUserDefinedCast() && Parent.IsImplicitCast() && !NthAncestor(1).IsCast())
                                {
                                    nonCastIndex = 1;
                                }
                                else if (u.IsImplicitCast() && Parent.IsUserDefinedCast() && !NthAncestor(1).IsCast())
                                {
                                    nonCastIndex = 1;
                                }
                                else if (u.IsImplicitCast() && Parent.IsUserDefinedCast() && NthAncestor(1).IsImplicitCast() &&
                                    !NthAncestor(2).IsCast())
                                {
                                    nonCastIndex = 2;
                                }
                                else
                                {
                                    return Fail(u, CSharpToJSExceptionType.Unexpected);
                                }
                            }

                            var nonCastParent = NthAncestor(nonCastIndex);
                            if (nonCastParent.NodeType == ExpressionType.NewArrayInit &&
                                NthAncestor(nonCastIndex + 1) is MethodCallExpression)
                            {
                                var call = (MethodCallExpression)NthAncestor(nonCastIndex + 1);
                                if (call.Method.IsVarargs() && 
                                    call.Arguments.IndexOf(nonCastParent) == call.Arguments.Count - 1)
                                {
                                    // ignore varargs invocation pseudo-parameter
                                    nonCastParent = NthAncestor(nonCastIndex + 1);
                                }
                            }

                            if (nonCastParent is MethodCallExpression && u != ((MethodCallExpression)nonCastParent).Object)
                            {
                                return Visit(u.Operand);
                            }
                            else if (nonCastParent is UnaryExpression || nonCastParent is BinaryExpression)
                            {
                                return Visit(u.Operand);
                            }
                            else if (nonCastParent is ConditionalExpression)
                            {
                                var iif = ((ConditionalExpression)Parent);
                                if (u == iif.Test)
                                {
                                    return Visit(u.Operand);
                                }
                                else
                                {
                                    var otherBranch = u == iif.IfFalse ? iif.IfTrue : iif.IfFalse;
                                    return otherBranch.IsCast() ?
                                        Fail(u, CSharpToJSExceptionType.ExplicitTypeRelatedOperation) : 
                                        Visit(u.Operand);
                                }
                            }
                            else
                            {
                                return Fail(u, CSharpToJSExceptionType.Unexpected);
                            }
                        }
                        else
                        {
                            return Fail(u, CSharpToJSExceptionType.ExplicitTypeRelatedOperation);
                        }
                    }

                case ExpressionType.TypeAs:
                    return Fail(u, CSharpToJSExceptionType.ExplicitTypeRelatedOperation);

                case ExpressionType.ConvertChecked:
                case ExpressionType.NegateChecked:
                    return Fail(u, CSharpToJSExceptionType.CheckedArithmetic);

                default:
                    return Fail(u, CSharpToJSExceptionType.Unexpected);
            }
        }
    }
}