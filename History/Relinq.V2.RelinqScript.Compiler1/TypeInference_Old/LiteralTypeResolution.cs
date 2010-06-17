using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Relinq.V2.RelinqScript.Grammar.Literals;

namespace Relinq.V2.RelinqScript.Compiler1.TypeInference_Old
{
    public static class LiteralTypeResolution
    {
        private sealed class Null { }

        public static Type InferType(String s)
        {
            if (s == "null")
            {
                return typeof(Null);
            }
            else if (Regex.IsMatch(s, "^'.*'$"))
            {
                return typeof(String);
            }
            else
            {
                bool boolValue;
                if (bool.TryParse(s, out boolValue))
                {
                    return typeof(bool);
                } 
                
                int intValue;
                if (int.TryParse(s, out intValue))
                {
                    return typeof(int);
                }

                double doubleValue;
                if (double.TryParse(s, out doubleValue))
                {
                    return typeof(double);
                }

                var listMatch = Regex.Match(s, @"^\[(?<contents>.*)\]$");
                if (listMatch.Success)
                {
                    var items = listMatch.Result("${contents}").DisassembleJsonObject();

                    Type commonType = null;
                    foreach (var item in items)
                    {
                        var itemType = InferType(item);
                        if (itemType == null)
                        {
                            return null;
                        }
                        else
                        {
                            if (commonType == null)
                            {
                                commonType = itemType;
                            }
                            else
                            {
                                if (itemType.IsAssignableFrom(commonType))
                                {
                                    commonType = itemType;
                                }
                                else if (itemType == typeof(Null))
                                {
                                    if (!commonType.IsClass)
                                    {
                                        commonType = typeof(Nullable<>).MakeGenericType(commonType);
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }

                    if (commonType == null || commonType == typeof(Null))
                    {
                        return null;
                    }
                    else
                    {
                        return typeof(IEnumerable<>).MakeGenericType(commonType);
                    }
                }
            }

            return null;
        }
    }
}