using System;
using System.Collections.Generic;
using Relinq.Helpers.Strings;
using Relinq.Script.Syntax.Operators;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Syntax.Expressions
{
    public class OperatorExpression : RelinqScriptExpression
    {
        public OperatorType Type { get; private set; }
        public IEnumerable<RelinqScriptExpression> Operands { get; private set; }

        public OperatorExpression(OperatorType type, IEnumerable<RelinqScriptExpression> operands)
            : base(ExpressionType.Operator, operands)
        {
            Type = type;
            Operands = operands;
        }

        protected override string GetTPathNode() { return this.GetOperatorCode(); }
        protected override string GetTPathSuffix(int childIndex)
        {
            return Operands.Count() == 1 ? "u" : (childIndex == 0 ? "l" : "r");
        }

        protected override string GetContent() 
        {
            String content;
            if (Operands.Count() == 1)
            {
                content = String.Format("{0}{1}",
                    this.GetOperatorCode(), Operands.ElementAt(0).Content);
            }
            else if (Operands.Count() == 2)
            {
                content = String.Format("{1} {0} {2}",
                    this.GetOperatorCode(), 
                    Operands.ElementAt(0).Content,
                    Operands.ElementAt(1).Content);
            }
            else
            {
                throw new NotSupportedException(String.Format(
                    "Operator with type '{0}' and operands [{1}] is not supported.",
                    Type, Operands.StringJoin()));
            }

            var parentIsHighOperator = Parent is OperatorExpression &&
                ((OperatorExpression)Parent).Type.GetPriority() >= Type.GetPriority();
            var iAmTargetOfAnInstanceCall = (Parent is MemberAccessExpression ||
                Parent is IndexerExpression) && ChildIndex == 0;

            return content.ParenthesizeIf(parentIsHighOperator || iAmTargetOfAnInstanceCall);
        }
    }
}