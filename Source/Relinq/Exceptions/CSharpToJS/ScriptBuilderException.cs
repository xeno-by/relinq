using System;
using System.Linq.Expressions;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.LinqVisitor;

namespace Relinq.Exceptions.CSharpToJS
{
    public class ScriptBuilderException : UnsupportedNodeException
    {
        [IncludeInMessage]
        public CSharpToJSExceptionType Type { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == CSharpToJSExceptionType.Unexpected; }
        }

        public ScriptBuilderException(CSharpToJSExceptionType type, Expression root, Expression expression)
            : base(root, expression)
        {
            Type = type;
        }

        public ScriptBuilderException(CSharpToJSExceptionType type, Expression root, Expression expression, Exception innerException)
            : base(root, expression, innerException)
        {
            Type = type;
        }

        public ScriptBuilderException(CSharpToJSExceptionType type, Expression root, MemberBinding binding)
            : base(root, binding)
        {
            Type = type;
        }

        public ScriptBuilderException(CSharpToJSExceptionType type, Expression root, MemberBinding binding, Exception innerException)
            : base(root, binding, innerException)
        {
            Type = type;
        }
    }
}