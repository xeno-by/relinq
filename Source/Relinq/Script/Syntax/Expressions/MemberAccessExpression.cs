using System;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Syntax.Expressions
{
    public class MemberAccessExpression : RelinqScriptExpression
    {
        public String Name { get; private set; }
        public RelinqScriptExpression Target { get; private set; }

        public MemberAccessExpression(String name, RelinqScriptExpression target)
            : base(ExpressionType.MemberAccess, target.AsArray())
        {
            Name = name;
            Target = Children.ElementAt(0);
        }

        protected override string GetTPathNode() { return "f:" + Name; }
        protected override string GetTPathSuffix(int childIndex) { return null; }

        protected override string GetContent()
        {
            return String.Format("{0}.{1}", Target.Content, Name);
        }
    }
}