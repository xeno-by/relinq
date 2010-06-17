using System;
using Antlr.Runtime;
using Relinq.Helpers.Reflection;

namespace Relinq.Exceptions.JSToCSharp.Parser
{
    public static class RecognitionExceptionHelper
    {
        public static SyntaxErrorException Report(BaseRecognizer source, RecognitionException e)
        {
            var input = source.Input.ToString();
            if (source.Input is ANTLRStringStream)
            {
                var dataField = typeof(ANTLRStringStream).GetField("data", BF.PrivateInstance);
                input = new String((Char[])dataField.GetValue(source.Input));
            }

            var antlrMessage = source.GetErrorHeader(e) + " " + source.GetErrorMessage(e, source.TokenNames);
            throw new SyntaxErrorException(input, antlrMessage, e);
        }
    }
}