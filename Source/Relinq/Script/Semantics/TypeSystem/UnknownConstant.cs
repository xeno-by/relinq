using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Semantics.TypeSystem
{
    public abstract class UnknownConstant : RelinqScriptType
    {
        public ConstantExpression Constant { get; private set; }

        protected UnknownConstant(ConstantExpression constant)
        {
            Constant = constant;
        }

        public override string ToString()
        {
            return "<unknown> (" + Constant + ")";
        }

        public override bool Equals(object obj)
        {
            var other = obj as UnknownConstant;
            return other != null && Constant == other.Constant;
        }

        public override int GetHashCode()
        {
            return Constant == null ? 0 : Constant.GetHashCode();
        }
    }
}