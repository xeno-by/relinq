using System;
using Relinq.Exceptions.Core;
using Relinq.Helpers;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.Grammar;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class ConstantInferenceFailedException : TypeInferenceException
    {
        [IncludeInMessage]
        public String TokenType { get; private set; }

        [IncludeInMessage]
        public String Constant { get; private set; }

        public ConstantInferenceFailedException(RelinqScriptExpression root, ConstantExpression ce)
            : base(JSToCSharpExceptionType.ConstantInferenceFailed, root, ce)
        {
            Constant = ce.Content;
            TokenType = String.Format("{0}.{1} ({2})",  
                typeof(EcmaScriptV3Lexer).Name,
                ce.Token.Type.GetSymbolicName<EcmaScriptV3Parser>(),
                ce.Token.Type);
        }
    }
}