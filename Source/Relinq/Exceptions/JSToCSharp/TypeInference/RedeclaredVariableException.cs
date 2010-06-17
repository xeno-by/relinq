using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.Closures;
using Relinq.Script.Syntax.Expressions;
using System.Linq;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class RedeclaredVariableException : TypeInferenceException
    {
        [IncludeInMessage]
        public Closure Closure { get; private set; }

        [IncludeInMessage]
        public String Name { get; private set; }

        public RedeclaredVariableException(RelinqScriptExpression root, LambdaExpression le, Closure closure)
            : base(JSToCSharpExceptionType.RedeclaredVariable, root, le)
        {
            Closure = closure;
            Name = le.Args.Intersect(closure.Keys).First();
        }
    }
}