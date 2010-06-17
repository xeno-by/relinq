using System;
using Relinq.Script.Integration;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Compilation;

namespace Relinq.Script.Semantics.TypeInference
{
    public class TypeInferenceContext
    {
        public RelinqScriptExpression Root { get; private set; }
        public TypeInferenceCache Inferences { get; private set; }
        public IntegrationContext Integration { get; private set; }

        public TypeInferenceContext(RelinqScriptExpression root, 
            TypeInferenceCache inferences, IntegrationContext integration)
        {
            Root = root;

            // ctx.Root might only be one of the following:
            // * Invoke expression (for both cases of compile-time and late-bound invocation)
            // * Indexer or Operator expression
            // * Root of an entire AST
            if (Root.Parent != null && !Root.IsCall())
            {
                throw new NotSupportedException(root.ToString());
            }

            Inferences = inferences;
            Integration = integration;
        }

        public TypeInferenceContext Clone()
        {
            return new TypeInferenceContext(Root, Inferences.Clone(), Integration);
        }
    }
}