using System;
using Relinq.Script.Syntax.Expressions;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeSystem
{
    public class Lambda : RelinqScriptType
    {
        public LambdaExpression Expression { get; private set; }
        public Type Type { get; private set; }

        public Lambda(LambdaExpression expression, Type type)
        {
            Expression = expression;
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("[λ: <{0}>]", Type.ToComprehensiveString());
        }

        public override bool Equals(object obj)
        {
            var other = obj as Lambda;
            return other != null && Type == other.Type && Expression == other.Expression;
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            num = (-1521134295 * num) + (Type == null ? 0 : Type.GetHashCode());
            num = (-1521134295 * num) + (Expression == null ? 0 : Expression.GetHashCode());
            return num;
        }
    }
}