using System;
using Antlr.Runtime;
using Relinq.Exceptions.Core;
using Relinq.Helpers.Strings;

namespace Relinq.Exceptions.JSToCSharp.Parser
{
    public class SyntaxErrorException : ParserException
    {
        private String Input { get; set; }

        [IncludeInMessage]
        public String SourceCode { get; private set; }

        [IncludeInMessage]
        public int LineNumber { get { return AntlrException.Line; } }

        [IncludeInMessage]
        public int CharPositionInLine { get { return AntlrException.CharPositionInLine; } }

        [IncludeInMessage]
        public Span ErrorSpan
        {
            get
            {
                var abs = Input.GetAbsoluteIndex(LineNumber, CharPositionInLine);
                if (abs == -1)
                {
                    return Span.Invalid;
                }
                else
                {
                    var span = AntlrException.Token == null ? 0 : AntlrException.Token.Text.Length;
                    return Span.FromLength(abs, span);
                }
            }
        }

        [IncludeInMessage]
        public String AntlrMessage { get; private set; }

        public RecognitionException AntlrException { get { return (RecognitionException)InnerException; } }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return false; }
        }

        public SyntaxErrorException(string input, String antlrMessage, RecognitionException exception)
            : base(JSToCSharpExceptionType.SyntaxError, exception)
        {
            AntlrMessage = antlrMessage;
            Input = input;

            var span = AntlrException.Token == null ? -1 : AntlrException.Token.Text.Length;
            var prettyInput = input.InjectErrorMarker(LineNumber, CharPositionInLine, span).InjectLineNumbers1();
            SourceCode = prettyInput;
        }
    }
}