using System;
using System.Reflection;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.RelinqScript.Grammar.Keywords;

namespace Relinq.V2.RelinqScript.Compiler2
{
    public static class CompilerKeywords
    {
        public static FuzzyType GetFuzzyType(String wannabe)
        {
            if (RelinqScriptKeywords.IsKeyword(wannabe))
            {
                switch (wannabe)
                {
                    case "ctx":
                        var asm = Assembly.Load("Playground");
                        var serverCtx = asm.GetType("Playground.V2.DataContexts.TestServerDataContext");
                        return FuzzyType.FromRuntimeType(serverCtx);

                    default:
                        throw new NotSupportedException(wannabe);
                }
            }
            else
            {
                throw new NotSupportedException(wannabe);
            }
        }

        public static object CreateInstance(String wannabe)
        {
            if (RelinqScriptKeywords.IsKeyword(wannabe))
            {
                switch (wannabe)
                {
                    case "ctx":
                        return Activator.CreateInstance(GetFuzzyType(wannabe).RuntimeType);

                    default:
                        throw new NotSupportedException(wannabe);
                }
            }
            else
            {
                throw new NotSupportedException(wannabe);
            }
        }
    }
}
