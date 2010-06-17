using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class NoSuchIndexerException : TypeInferenceException
    {
        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfTarget { get; private set; }

        public NoSuchIndexerException(RelinqScriptExpression root, IndexerExpression ie, RelinqScriptType typeOfTarget)
            : base(JSToCSharpExceptionType.NoSuchIndexer, root, ie)
        {
            InferredTypeOfTarget = typeOfTarget;
        }
    }
}