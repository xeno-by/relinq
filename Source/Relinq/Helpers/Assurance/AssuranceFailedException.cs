using System;

namespace Relinq.Helpers.Assurance
{
    public class AssuranceFailedException : Exception
    {
        public AssuranceFailedException(String message, params Object[] @params)
            : base(String.Format(message, @params))
        {
        }

        public AssuranceFailedException(String message, Exception innerException, params Object[] @params)
            : base(String.Format(message, @params), innerException)
        {
        }
    }
}