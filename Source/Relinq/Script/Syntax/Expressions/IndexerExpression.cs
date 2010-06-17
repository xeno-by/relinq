using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Syntax.Expressions
{
    public class IndexerExpression : RelinqScriptExpression
    {
        public RelinqScriptExpression Target { get; private set; }
        public IEnumerable<RelinqScriptExpression> Operands { get; private set; }

        public IndexerExpression(RelinqScriptExpression target, IEnumerable<RelinqScriptExpression> operands)
            : base(ExpressionType.Indexer, target.AsArray().Concat(operands))
        {
            Target = target;
            Operands = operands;
        }

        protected override string GetTPathNode() { return "[]"; }
        protected override string GetTPathSuffix(int childIndex)
        {
            return childIndex == 0 ? "tar" : "arg" + (childIndex - 1);
        }

        protected override string GetContent()
        {
            return String.Format("{0}[{1}]", Target.Content, Operands.Select(op => op.Content).StringJoin());
        }
    }
}