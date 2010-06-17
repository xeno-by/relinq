using System.Linq.Expressions;

namespace Relinq.Linq.Preprocessing
{
    public class CoalesceHandler : LinqVisitor
    {
        public CoalesceHandler(Expression root) 
            : base(root)
        {
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            // a ?? b => a != null ? a : b
            if (b.NodeType == ExpressionType.Coalesce)
            {
                return Visit(Expression.Condition(
                    Expression.NotEqual(b.Left, Expression.Constant(null)),
                    b.Left,
                    b.Right));
            }

            return base.VisitBinary(b);
        }
    }
}