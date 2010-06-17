using System.Collections.Generic;
using System.Linq;

namespace Relinq.Script.Syntax.Expressions
{
    public static class ExpressionsHelper
    {
        public static bool IsIndirectChildOf(this RelinqScriptExpression e1, RelinqScriptExpression e2)
        {
            if (e1 == null || e2 == null) return false;
            return e1 == e2 || IsIndirectChildOf(e1.Parent, e2);
        }

        public static IEnumerable<RelinqScriptExpression> Parents(this RelinqScriptExpression e)
        {
            for (var curr = e.Parent; curr != null; curr = curr.Parent)
                yield return curr;
        }

        public static int Depth(this RelinqScriptExpression e)
        {
            return e.Parents().Count();
        }
    }
}