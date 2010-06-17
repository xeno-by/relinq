using System;
using System.Collections.Generic;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.Operators;
using System.Linq;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class NoSuchOperatorException : TypeInferenceException
    {
        [IncludeInMessage]
        public RelinqScriptType[] InferredOperandTypes { get; private set; }

        [IncludeInMessage]
        public String OperatorName { get; private set; }

        public NoSuchOperatorException(RelinqScriptExpression root, OperatorExpression oe, IEnumerable<RelinqScriptType> operandTypes)
            : base(JSToCSharpExceptionType.NoSuchIndexer, root, oe)
        {
            InferredOperandTypes = operandTypes.ToArray();
            OperatorName = oe.Type.GetOpCode();
        }
    }
}