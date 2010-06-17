using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Integration;

namespace Relinq.Exceptions.Integration
{
    public abstract class IntegrationException : RelinqException
    {
        [IncludeInMessage]
        public IntegrationExceptionType Type { get; private set; }

        [IncludeInMessage]
        public IntegrationContext Context { get; private set; }

        protected IntegrationException(IntegrationExceptionType type, IntegrationContext ctx, Exception innerException)
            : base(innerException)
        {
            Type = type;
            Context = ctx;
        }
    }
}