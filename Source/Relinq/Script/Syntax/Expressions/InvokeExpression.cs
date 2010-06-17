using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Syntax.Expressions
{
    public class InvokeExpression : RelinqScriptExpression
    {
        public RelinqScriptExpression Target { get; private set; }
        public IEnumerable<RelinqScriptExpression> Args { get; private set; }

        public InvokeExpression(RelinqScriptExpression target, IEnumerable<RelinqScriptExpression> args)
            : base(ExpressionType.Invoke, target.AsArray().Concat(args))
        {
            Target = Children.ElementAt(0);
            Args = Children.Skip(1);
        }

        protected override string GetTPathNode() { return "()"; }
        protected override string GetTPathSuffix(int childIndex)
        {
            return childIndex == 0 ? "tar" : "arg" + (childIndex - 1);
        }

        protected override string GetContent()
        {
            return String.Format("{0}({1})", Target.Content, Args.Select(arg => arg.Content).StringJoin());
        }
    }
}