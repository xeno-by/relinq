using System;
using System.Linq.Expressions;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Relinq.V2.RelinqScript.Compiler2;
using Relinq.V2.RelinqScript.Grammar;

namespace Relinq.V2.RelinqScript
{
    public class RelinqScriptCompiler2 : IRelinqScriptCompiler
    {
        public Expression Compile(String relinqScript)
        {
            var input = new ANTLRStringStream(relinqScript);
            var lex = new EcmaScriptV3Lexer(input);
            var tokens = new CommonTokenStream(lex);
            var parser = new EcmaScriptV3Parser(tokens);

            var es3Ast = parser.expression();
            var rsAst = new RelinqScriptParser().Visit((CommonTree)es3Ast.Tree);

            var compilerAst = new TypeInferenceAstBuilder().Visit(rsAst);
            using (var engine = new TypeInferenceEngine(compilerAst))
            {
#if DEBUG
                try
                {
                    var wtf = 0;
                    while(!engine.Run()) if (wtf++ == 10) break;
                    return new StronglyTypedAstBuilder().Visit(compilerAst);
                }
                catch (Exception)
                {
                    engine.Dump();
                    throw;
                }
#else
                engine.Run();
                return new StronglyTypedAstBuilder().Visit(tiAst);
#endif
            }
        }
    }
}
