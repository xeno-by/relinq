using System;
using System.Linq.Expressions;

namespace Relinq.Exceptions.LinqVisitor
{
    public class UnsupportedNodeException : LinqVisitorException
    {
        public UnsupportedNodeException(Expression expression)
            : base(expression)
        {
        }

        public UnsupportedNodeException(Expression root, Expression expression)
            : base(root, expression)
        {
        }

        public UnsupportedNodeException(Expression expression, Exception innerException)
            : base(expression, innerException)
        {
        }

        public UnsupportedNodeException(Expression root, Expression expression, Exception innerException)
            : base(root, expression, innerException)
        {
            
        }

        public UnsupportedNodeException(MemberBinding binding)
            : base(binding)
        {
        }

        public UnsupportedNodeException(Expression root, MemberBinding binding)
            : base(root, binding)
        {
        }

        public UnsupportedNodeException(MemberBinding binding, Exception innerException)
            : base(binding, innerException)
        {
        }

        public UnsupportedNodeException(Expression root, MemberBinding binding, Exception innerException)
            : base(root, binding, innerException)
        {
        }
    }
}