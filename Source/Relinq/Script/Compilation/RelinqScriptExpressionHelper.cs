using System;
using System.Collections.Generic;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Compilation
{
    public static class RelinqScriptExpressionHelper
    {
        public static bool IsCall(this RelinqScriptExpression e)
        {
            return e.NodeType == ExpressionType.Invoke ||
                   e.NodeType == ExpressionType.Operator ||
                   e.NodeType == ExpressionType.Indexer;
        }

        public static IEnumerable<RelinqScriptExpression> CallArgs(this RelinqScriptExpression e)
        {
            if (!e.IsCall())
            {
                throw new NotSupportedException(e.ToString());
            }
            else
            {
                switch(e.NodeType)
                {
                    case ExpressionType.Invoke:
                        return ((InvokeExpression)e).Args;
                    case ExpressionType.Indexer:
                        return ((IndexerExpression)e).Operands;
                    case ExpressionType.Operator:
                        return ((OperatorExpression)e).Operands;
                    default:
                        throw new NotSupportedException(e.ToString());
                }
            }
        }
    }
}
