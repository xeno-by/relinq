using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class CannotForgeAnonymousTypeException : TypeInferenceException
    {
        [IncludeInMessage]
        public String OffendingProperty { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType OffendingType { get; private set; }

        public CannotForgeAnonymousTypeException(RelinqScriptExpression root, NewExpression ne, string offendingProperty, RelinqScriptType offendingType)
            : base(JSToCSharpExceptionType.CannotForgeAnonymousType, root, ne)
        {
            OffendingProperty = offendingProperty;
            OffendingType = offendingType;
        }
    }
}