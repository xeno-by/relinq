using System;
using System.Linq.Expressions;
using Relinq.Script.Compilation;
using Relinq.Script.Integration;

namespace Relinq.Script
{
    public class RelinqScriptCompiler
    {
        private IntegrationContext Integration { get; set; }

        public RelinqScriptCompiler(IntegrationContext integration)
        {
            Integration = integration;
        }
        
        public Expression Compile(String relinqScriptCode)
        {
            var ast = new RelinqScriptParser(relinqScriptCode, Integration).Parse();
            var inferences = new TypeInferenceEngine(ast, Integration).InferTypes();
            return new CSharpExpressionTreeBuilder(ast, Integration).Compile(inferences);
        }
    }
}