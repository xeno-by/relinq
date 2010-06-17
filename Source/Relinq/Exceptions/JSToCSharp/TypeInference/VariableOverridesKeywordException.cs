using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class VariableOverridesKeywordException : TypeInferenceException
    {
        [IncludeInMessage]
        public String Name { get; private set; }

        public VariableOverridesKeywordException(RelinqScriptExpression root, LambdaExpression le, String name)
            : base(JSToCSharpExceptionType.VariableOverridesKeyword, root, le)
        {
            Name = name;
        }
    }
}