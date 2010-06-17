using System;
using System.Linq.Expressions;
using Relinq.Exceptions.Core;

namespace Relinq.Exceptions.LinqVisitor
{
    public class UnexpectedNodeException : LinqVisitorException
    {
        [IncludeInMessage]
        public Object Node { get; private set; }

        [IncludeInMessage]
        public Type NodeType { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return true; }
        }

        public UnexpectedNodeException(Object node, Type nodeType)
            : this(null, node, nodeType)
        {
        }

        public UnexpectedNodeException(Expression root, Object node, Type nodeType) 
            : base(root, (Expression)null)
        {
            NodeType = nodeType;

            Node = node;
            Expression = node as Expression;
            Binding = node as MemberBinding;
        }
    }
}