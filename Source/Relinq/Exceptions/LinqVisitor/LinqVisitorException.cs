using System;
using System.Linq.Expressions;
using Relinq.Exceptions.Core;

namespace Relinq.Exceptions.LinqVisitor
{
    public class LinqVisitorException : RelinqException
    {
        [IncludeInMessage]
        public Expression Root { get; private set; }

        [IncludeInMessage]
        public Expression Expression { get; protected set; }

        [IncludeInMessage]
        public ExpressionType? ExpressionType
        {
            get { return Expression == null ? null : (ExpressionType?)Expression.NodeType; }
        }

        [IncludeInMessage]
        public MemberBinding Binding { get; protected set; }

        [IncludeInMessage]
        public MemberBindingType? BindingType
        {
            get { return Binding == null ? null : (MemberBindingType?)Binding.BindingType; }
        }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return false; }
        }

        public LinqVisitorException(Expression expression)
            : this(null, expression, null)
        {
        }

        public LinqVisitorException(Expression root, Expression expression)
            : this(root, expression, null)
        {
        }

        public LinqVisitorException(Expression expression, Exception innerException)
            : this(null, expression, innerException)
        {
        }

        public LinqVisitorException(Expression root, Expression expression, Exception innerException)
            : base(innerException)
        {
            Root = root;
            Expression = expression;
        }

        public LinqVisitorException(MemberBinding binding)
            : this(null, binding, null)
        {
        }

        public LinqVisitorException(Expression root, MemberBinding binding)
            : this(root, binding, null)
        {
        }

        public LinqVisitorException(MemberBinding binding, Exception innerException)
            : this(null, binding, innerException)
        {
        }

        public LinqVisitorException(Expression root, MemberBinding binding, Exception innerException)
            : base(innerException)
        {
            Root = root;
            Binding = binding;
        }
    }
}