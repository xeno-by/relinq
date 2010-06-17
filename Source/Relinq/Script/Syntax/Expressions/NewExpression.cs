using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Syntax.Expressions
{
    public class NewExpression : RelinqScriptExpression
    {
        public IDictionary<String, RelinqScriptExpression> Props { get; private set; }

        public NewExpression(IDictionary<String, RelinqScriptExpression> props)
            : base(ExpressionType.New, props.Values)
        {
            Props = props;
        }

        protected override string GetTPathNode() { return "new"; }
        protected override string GetTPathSuffix(int childIndex) { return null; }

        protected override string GetContent()
        {
            return String.Format("new {{{0}}}",
                Props.Select(kvp => String.Format("{0} = {1}", kvp.Key, kvp.Value.Content)).StringJoin());
        }
    }
}