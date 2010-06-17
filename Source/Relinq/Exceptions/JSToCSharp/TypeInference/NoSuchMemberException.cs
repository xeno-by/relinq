using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class NoSuchMemberException : TypeInferenceException
    {
        [IncludeInMessage]
        public RelinqScriptType InferredTypeOfTarget { get; private set; }

        [IncludeInMessage]
        public String MemberName { get; private set; }

        public NoSuchMemberException(RelinqScriptExpression root, MemberAccessExpression mae, RelinqScriptType typeOfTarget)
            : base(mae.Parent is InvokeExpression && mae.ChildIndex == 0 ?
                                                                             JSToCSharpExceptionType.NoSuchMethod :
                                                                                                                      JSToCSharpExceptionType.NoSuchFieldOrProp, root, mae)
        {
            InferredTypeOfTarget = typeOfTarget;
            MemberName = mae.Name;
        }
    }
}