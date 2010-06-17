using System;
using System.Collections.Generic;
using System.Text;

namespace Relinq.Script.Semantics.Literals
{
    // todo: rewrite using RelinqScriptParser
    public static class DeserializationHelper
    {
        // Splits JSON lists and object definitions into structural parts.
        // String.Split method won't work for lists of object.
        public static IEnumerable<String> DisassembleJsonObject(this String s)
        {
            var brackBalance = 0;
            var braceBalance = 0;
            var par = new StringBuilder();

            foreach (var c in s)
            {
                switch (c)
                {
                    case '[':
                        ++brackBalance;
                        break;
                    case ']':
                        --brackBalance;
                        break;
                    case '{':
                        ++braceBalance;
                        break;
                    case '}':
                        --braceBalance;
                        break;
                    default:
                        if (c == ',' && brackBalance == 0 && braceBalance == 0)
                        {
                            yield return par.ToString().Trim();
                            par = new StringBuilder();
                            continue;
                        }
                        break;
                }

                par.Append(c);
            }

            var lastElement = par.ToString().Trim();
            if (!String.IsNullOrEmpty(lastElement))
            {
                yield return lastElement;
            }
        }
    }
}