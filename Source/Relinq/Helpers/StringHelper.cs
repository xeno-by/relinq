using System;
using Relinq.Helpers.Collections;
using System.Linq;

namespace Relinq.Helpers
{
    public static class StringHelper
    {
        public static String Parenthesize(this String s)
        {
            return String.Format("({0})", s);
        }

        public static String ParenthesizeIf(this String s, bool p)
        {
            return p ? s.Parenthesize() : s;
        }

        public static String Unparenthesize(this String s)
        {
            if (s.StartsWith("(") && s.EndsWith(")"))
            {
//                return s.Skip(1).Reverse().Skip(1).Reverse().StringJoin();
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }

        public static String UnparenthesizeIf(this String s, bool p)
        {
            return p ? s.Unparenthesize() : s;
        }

        public static String ToVerbatimCSharpCopy(this String s)
        {
            return s.Select(c => (c != '\\' && c != '\"') ? new String(c, 1) : "\\" + c)
                .StringJoin(String.Empty);
        }
    }
}
