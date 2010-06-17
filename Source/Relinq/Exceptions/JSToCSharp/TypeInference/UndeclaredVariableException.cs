using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.Closures;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class UndeclaredVariableException : TypeInferenceException
    {
        [IncludeInMessage]
        public Closure Closure { get; private set; }

        [IncludeInMessage]
        public String Name { get; private set; }

        public UndeclaredVariableException(RelinqScriptExpression root, VariableExpression ve, Closure closure) 
            : base(JSToCSharpExceptionType.UndeclaredVariable, root, ve)
        {
            Closure = closure;
            Name = ve.Name;
        }
    }
}