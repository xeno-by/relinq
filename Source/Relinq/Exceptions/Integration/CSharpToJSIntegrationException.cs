using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Integration;

namespace Relinq.Exceptions.Integration
{
    public class CSharpToJSIntegrationException : IntegrationException
    {
        [IncludeInMessage]
        public Object CSharpObject { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == IntegrationExceptionType.Unexpected; }
        }

        public CSharpToJSIntegrationException(IntegrationExceptionType type, IntegrationContext ctx, Object csharpObject)
            : this(type, ctx, csharpObject, null)
        {
        }

        public CSharpToJSIntegrationException(IntegrationExceptionType type, IntegrationContext ctx, Object csharpObject, Exception innerException) 
            : base(type, ctx, innerException)
        {
            CSharpObject = csharpObject;
        }
    }
}