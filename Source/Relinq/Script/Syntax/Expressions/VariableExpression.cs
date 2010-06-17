using System;
using System.Linq;

namespace Relinq.Script.Syntax.Expressions
{
    public class VariableExpression : RelinqScriptExpression
    {
        public String Name { get; private set; }

        public VariableExpression(String name)
            : base(ExpressionType.Variable, Enumerable.Empty<RelinqScriptExpression>())
        {
            Name = name;
        }

        protected override string GetTPathNode() { return "v:" + Name; }
        protected override string GetTPathSuffix(int childIndex) { return null; }
        protected override string GetContent() { return Name; }
    }
}