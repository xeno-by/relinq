using System;
using System.Linq;
using Relinq.Helpers.Strings;

namespace Relinq.Script.Syntax.Expressions
{
    public class ConditionalExpression : RelinqScriptExpression
    {
        public RelinqScriptExpression Test { get { return Children.ElementAt(0); } }
        public RelinqScriptExpression IfTrue { get { return Children.ElementAt(1); } }
        public RelinqScriptExpression IfFalse { get { return Children.ElementAt(2); } }

        public ConditionalExpression(RelinqScriptExpression cond, RelinqScriptExpression ifTrue, RelinqScriptExpression ifFalse) 
            : base(ExpressionType.Conditional, new []{cond, ifTrue, ifFalse})
        {
        }

        protected override string GetTPathNode() { return String.Format("test"); }
        protected override string GetTPathSuffix(int childIndex)
        {
            if (childIndex == 0) return "?";
            if (childIndex == 1) return "0";
            if (childIndex == 2) return "1";
            throw new NotSupportedException(childIndex.ToString());
        }

        protected override string GetContent()
        {
            var content = String.Format("({0} ? {1} : {2})", 
                Test.Content, 
                IfTrue.Content.ParenthesizeIf(IfTrue is ConditionalExpression),
                IfFalse.Content.ParenthesizeIf(IfTrue is ConditionalExpression));

            return content.ParenthesizeIf(Parent != null);
        }
    }
}