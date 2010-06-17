using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Relinq.Exceptions.Core;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Strings;

namespace Relinq.Playground
{
    public static class RelinqExceptionSupport
    {
        internal static void GenerateExceptionTest(this Exception ex)
        {
            var sb = new StringBuilder();
            GenerateExceptionTest(ex, 0, sb);

            var trace = new StackTrace().GetFrame(1);
            File.WriteAllText(@"d:\" + trace.GetMethod().Name, sb.ToString());
        }

        internal static void GenerateExceptionTest(this Exception ex, int index, StringBuilder sb)
        {
            var varName = "ex" + index;
            sb.AppendLine(String.Format("var {0} = {1};",
                varName,
                index == 0 ? "ex" : "ex" + (index - 1) + ".InnerException"));
            sb.AppendLine(String.Format(
                "Assert.AreEqual(typeof({0}), {1}.GetType());",
                ex.GetType().Name, varName));

            if (ex.InnerException == null)
            {
                sb.AppendLine(String.Format("Assert.IsNull({0}.InnerException);", varName));
            }
            else
            {
                sb.AppendLine(String.Format("Assert.IsNotNull({0}.InnerException);", varName));
            }

            sb.AppendLine();

            if (ex is RelinqException)
            {
                var valStrings = ((RelinqException)ex).PrettyProperties;
                if (valStrings.IsNotEmpty())
                {
                    sb.AppendLine(String.Format("var {0}_kvps = ((RelinqException){0}).PrettyProperties;", varName));
                    foreach (var kvp in valStrings)
                    {
                        if (kvp.Key != "Id")
                        {
                            var value = kvp.Value.ToVerbatimCSharpCopy();
                            var guidRegex = @"\{?[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}\}?";

                            String assertion;
                            if (new Regex(guidRegex).IsMatch(value))
                            {
                                assertion = "RelinqExceptionSupport.AssertAreEqualExceptGuids";
                            }
                            else
                            {
                                assertion = "Assert.AreEqual";
                            }

                            sb.AppendLine(String.Format(
                                "{4}({0}\"{1}\", {2}_kvps[\"{3}\"]);",
                                kvp.Value.Contains(Environment.NewLine) ? "@" : "",
                                value, varName, kvp.Key.ToVerbatimCSharpCopy(),
                                assertion));
                        }
                    }

                    sb.AppendLine();
                }
            }

            if (ex.InnerException != null)
            {
                GenerateExceptionTest(ex.InnerException, index + 1, sb);
            }
        }

        internal static void AssertAreEqualExceptGuids(String s1, String s2)
        {
            // http://www.regexlib.com/REDetails.aspx?regexp_id=212
            var guidRegex = @"\{?[a-fA-F\d]{8}-([a-fA-F\d]{4}-){3}[a-fA-F\d]{12}\}?";
            var replaceRegex = new Regex("Id: " + guidRegex);
            s1 = replaceRegex.Replace(s1, m => "Id: <GUID>");
            s2 = replaceRegex.Replace(s1, m => "Id: <GUID>");

            Assert.AreEqual(s1, s2);
        }
    }
}
