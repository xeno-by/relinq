using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class InconsistentConditionalExpression : TypeInferenceException
    {
        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfTest { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfIfTrue { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfIfFalse { get; private set; }

        public InconsistentConditionalExpression(JSToCSharpExceptionType type, RelinqScriptExpression root, ConditionalExpression expression, RelinqScriptType inferredTypeOfTest, RelinqScriptType inferredTypeOfIfTrue, RelinqScriptType inferredTypeOfIfFalse) 
            : base(type, root, expression)
        {
            InferredTypeOfTest = inferredTypeOfTest;
            InferredTypeOfIfTrue = inferredTypeOfIfTrue;
            InferredTypeOfIfFalse = inferredTypeOfIfFalse;
        }
    }
}