using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class CannotBeInvokedException : TypeInferenceException
    {
        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfTarget { get; private set; }

        public CannotBeInvokedException(RelinqScriptExpression root, InvokeExpression ie, RelinqScriptType typeOfTarget)
            : base(JSToCSharpExceptionType.CannotBeInvoked, root, ie)
        {
            InferredTypeOfTarget = typeOfTarget;
        }
    }
}