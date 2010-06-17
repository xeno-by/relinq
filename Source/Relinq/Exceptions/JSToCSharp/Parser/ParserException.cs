using System;
using Relinq.Exceptions.Core;

namespace Relinq.Exceptions.JSToCSharp.Parser
{
    public abstract class ParserException : RelinqException
    {
        [IncludeInMessage]
        public JSToCSharpExceptionType Type { get; private set; }

        protected ParserException(JSToCSharpExceptionType type)
            : this(type, null)
        {
        }

        protected ParserException(JSToCSharpExceptionType type, Exception innerException)
            : base(innerException)
        {
            Type = type;
        }
    }
}