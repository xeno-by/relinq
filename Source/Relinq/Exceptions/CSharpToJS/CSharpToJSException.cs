using System;
using System.Linq.Expressions;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.Integration;
using Relinq.Exceptions.JsonSerializer;
using Relinq.Exceptions.LinqVisitor;

namespace Relinq.Exceptions.CSharpToJS
{
    public class CSharpToJSException : RelinqException
    {
        [IncludeInMessage]
        public CSharpToJSExceptionType Type { get; private set; }

        [IncludeInMessage]
        public Expression Input { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == CSharpToJSExceptionType.Unexpected; }
        }

        public CSharpToJSException(Expression input, Exception innerException)
            : base(innerException)
        {
            Input = input;

            if (innerException is UnexpectedNodeException)
            {
                Type = CSharpToJSExceptionType.Unexpected;
            }
            else if (innerException is ScriptBuilderException)
            {
                var sbe = (ScriptBuilderException)innerException;
                Type = sbe.Type;
            }
            else if (innerException is LinqVisitorException)
            {
                var ave = (LinqVisitorException)innerException;
                if (ave.InnerException is IntegrationException)
                {
                    Type = CSharpToJSExceptionType.Integration;
                }
                else if (ave.InnerException is JsonSerializationException)
                {
                    Type = CSharpToJSExceptionType.JsonSerialization;
                }
                else
                {
                    Type = CSharpToJSExceptionType.Unexpected;
                }
            }
            else
            {
                Type = CSharpToJSExceptionType.Unexpected;
            }
        }
    }
}