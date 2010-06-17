using System;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp.Parser;
using Relinq.Exceptions.JSToCSharp.TypeInference;

namespace Relinq.Exceptions.JSToCSharp
{
    public class JSToCSharpException : RelinqException
    {
        [IncludeInMessage]
        public JSToCSharpExceptionType Type { get; private set; }

        [IncludeInMessage]
        public String Input { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get 
            { 
                return 
                    Type == JSToCSharpExceptionType.Unexpected ||
                    Type == JSToCSharpExceptionType.UnexpectedJsConstruct ||
                    Type == JSToCSharpExceptionType.UnexpectedNodeType; 
            }
        }

        public JSToCSharpException(String input, Exception innerException)
            : base(innerException)
        {
            Input = input;

            if (innerException is SyntaxErrorException)
            {
                Type = JSToCSharpExceptionType.SyntaxError;
            }
            else if (innerException is SemanticErrorException)
            {
                var see = (SemanticErrorException)innerException;
                Type = see.Type;
            }
            else if (innerException is TypeInferenceException)
            {
                var tie = (TypeInferenceException)innerException;
                Type = tie.Type;
            }
            else
            {
                Type = JSToCSharpExceptionType.Unexpected;
            }
        }
    }
}