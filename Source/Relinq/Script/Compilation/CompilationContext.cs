using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using System.Linq;

namespace Relinq.Script.Compilation
{
    public class CompilationContext
    {
        public IDictionary<String, ParameterExpression> Closure { get; private set; }
        public IDictionary<RelinqScriptExpression, RelinqScriptType> Types { get; private set; }
        public IDictionary<RelinqScriptExpression, ResolvedInvocation> Invocations { get; private set; }

        public CompilationContext(TypeInferenceCache inferences)
        {
            Closure = new Dictionary<String, ParameterExpression>();
            Types = inferences.AsReadOnly();
            Invocations = inferences.Invocations.AsReadOnly();
        }

        public override string ToString()
        {
            var closure = Environment.NewLine + ">>Closure: " + Closure.Keys.StringJoin();
            var inferredTypes = Environment.NewLine +
                Types.Select(kvp => String.Format(">>{0}: {1}", kvp.Key, kvp.Value)).StringJoin(Environment.NewLine);
            var inferredInvocations = Environment.NewLine +
                Invocations.Select(kvp => String.Format(">>{0}: {1}", kvp.Key, kvp.Value)).StringJoin(Environment.NewLine);
            return closure + inferredTypes + inferredInvocations;
        }
    }
}