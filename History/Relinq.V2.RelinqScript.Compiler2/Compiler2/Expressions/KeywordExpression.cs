using System;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class KeywordExpression : TypeInferenceUnit
    {
        public String Name { get; private set; }

        public KeywordExpression(String name)
            : base(ExpressionType.Keyword, Enumerable.Empty<TypeInferenceUnit>())
        {
            Name = name;
        }

        protected override string ContentToString()
        {
            return "k:" + Name;
        }

        protected override FuzzyType OnInitializeMainTypePoint()
        {
            return CompilerKeywords.GetFuzzyType(Name);
        }
    }
}