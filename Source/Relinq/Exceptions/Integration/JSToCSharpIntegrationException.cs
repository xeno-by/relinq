using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Integration;

namespace Relinq.Exceptions.Integration
{
    public class JSToCSharpIntegrationException : IntegrationException
    {
        [IncludeInMessage]
        public String JSVariable { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == IntegrationExceptionType.Unexpected; }
        }

        public JSToCSharpIntegrationException(IntegrationExceptionType type, IntegrationContext ctx, String jsVariable)
            : this(type, ctx, jsVariable, null)
        {
        }

        public JSToCSharpIntegrationException(IntegrationExceptionType type, IntegrationContext ctx, String jsVariable, Exception innerException)
            : base(type, ctx, innerException)
        {
            JSVariable = jsVariable;
        }
    }
}