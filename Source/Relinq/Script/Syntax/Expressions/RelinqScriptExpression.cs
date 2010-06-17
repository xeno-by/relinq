using System;
using System.Collections.Generic;
using System.Text;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Strings;

namespace Relinq.Script.Syntax.Expressions
{
    public abstract class RelinqScriptExpression /*: DebuggableObject*/
    {
        public ExpressionType NodeType { get; private set; }

        public int ChildIndex { get; private set; }
        public RelinqScriptExpression Parent { get; private set; }
        public IEnumerable<RelinqScriptExpression> Children { get; private set; }

        protected RelinqScriptExpression(ExpressionType nodeType, IEnumerable<RelinqScriptExpression> children)
        {
            NodeType = nodeType;
            Children = new List<RelinqScriptExpression>(children).AsReadOnly();

            var childIndex = 0;
            foreach (var child in Children)
            {
                child.Parent = this;
//                child.RegDebuggableParent(this);
                child.ChildIndex = childIndex++;
            }
        }

        protected abstract string GetTPathNode();
        protected abstract string GetTPathSuffix(int childIndex);
        protected abstract string GetContent(); // todo. strip the result from extra parentheses

        public string ShortTPath { get { return GenerateTPath(false); } }
        public string FullTPath { get { return GenerateTPath(true); } }
        public string Content { get { return GetContent(); } }

        private string GenerateTPath(bool full)
        {
            var stack = new Stack<String>();
            for (var current = this; current != null; current = current.Parent)
            {
                var suffix = current.Parent == null ? null :
                    current.Parent.GetTPathSuffix(current.ChildIndex);
                suffix = full && !suffix.IsNullOrEmpty() ? suffix + ":" : null;

                stack.Push("/" + suffix + current.GetTPathNode());
            }

            var sb = new StringBuilder();
            while (stack.Count != 0) sb.Append(stack.Pop());
            return sb.ToString();
        }

        public override string ToString()
        {
            var content = this is OperatorExpression ? Content.Unparenthesize() : Content;
            return String.Format("[{0} -> {1}]", FullTPath, content);
        }
    }
}