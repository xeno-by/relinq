using System;
using System.Linq;

namespace Relinq.Script.Syntax.Expressions
{
    public class KeywordExpression : RelinqScriptExpression
    {
        public String Name { get; private set; }

        public KeywordExpression(String name)
            : base(ExpressionType.Keyword, Enumerable.Empty<RelinqScriptExpression>())
        {
            Name = name;
        }

        protected override string GetTPathNode() { return "k:" + Name; }
        protected override string GetTPathSuffix(int childIndex) { return null; }
        protected override string GetContent() { return Name; }
    }
}