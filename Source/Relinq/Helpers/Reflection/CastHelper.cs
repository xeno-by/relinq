using System;
using System.Linq.Expressions;
using Relinq.Script.Semantics.Lookup;
using Relinq.Script.Semantics.UserDefinedCasts;

namespace Relinq.Helpers.Reflection
{
    public static class CastHelper
    {
        public static bool IsCast(this Expression ue)
        {
            return ue.NodeType == ExpressionType.Convert;
        }

        public static bool IsImplicitCast(this Expression ue)
        {
            if (ue.NodeType == ExpressionType.Convert)
            {
                var unary = (UnaryExpression)ue;
                return unary.Operand.Type.HasPredefinedImplicitCastTo(unary.Type) ||
                    (unary.Method != null && UserDefinedImplicitCastsHelper.IsCastImpl(unary.Method.Name));
            }
            else
            {
                throw new NotSupportedException(ue.ToString());
            }
        }

        public static bool IsUserDefinedCast(this Expression ue)
        {
            if (ue.NodeType == ExpressionType.Convert)
            {
                return ((UnaryExpression)ue).Method != null;
            }
            else
            {
                throw new NotSupportedException(ue.ToString());
            }
        }
    }
}