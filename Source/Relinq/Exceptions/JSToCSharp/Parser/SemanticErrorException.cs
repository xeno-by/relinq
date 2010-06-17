using System;
using Antlr.Runtime.Tree;
using Relinq.Exceptions.Core;

namespace Relinq.Exceptions.JSToCSharp.Parser
{
    public class SemanticErrorException : ParserException
    {
        [IncludeInMessage]
        public CommonTree Node { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get 
            {
                return Type == JSToCSharpExceptionType.Unexpected ||
                   Type == JSToCSharpExceptionType.UnexpectedNodeType;
            }
        }

        public SemanticErrorException(JSToCSharpExceptionType type, CommonTree node)
            : this(type, node, null)
        {
        }

        public SemanticErrorException(JSToCSharpExceptionType type, CommonTree node, Exception innerException)
            : base(type, innerException)
        {
            Node = node;
        }
    }
}