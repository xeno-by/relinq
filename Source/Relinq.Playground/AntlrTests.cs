using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NUnit.Framework;
using Relinq.Exceptions.JSToCSharp.Parser;
using Relinq.Script.Compilation;
using Relinq.Script.Syntax.Grammar;
using Relinq.Script.Syntax.SyntaxReference;

namespace Relinq.Playground
{
    [TestFixture]
    public class AntlrTests
    {
        [Test]
        [ExpectedException(typeof(SyntaxErrorException))]
        public void TestCrashOnRecognitionException()
        {
            new RelinqScriptParser("2+*+2", TestTransformationFramework.Integration).Parse();
        }

        private CommonTree ParseViaAntlrOnly(String relinqScriptCode)
        {
            var input = new ANTLRStringStream(relinqScriptCode);
            var lex = new EcmaScriptV3Lexer(input);
            var tokens = new CommonTokenStream(lex);
            var parser = new EcmaScriptV3Parser(tokens);
            return (CommonTree)parser.expression().Tree;
        }

        [Test]
        public void TestLiteralTokens()
        {
            CommonTree es3Ast;
            IToken token;

            es3Ast = ParseViaAntlrOnly("null");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.NULL);

            es3Ast = ParseViaAntlrOnly("true");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.TRUE);

            es3Ast = ParseViaAntlrOnly("false");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.FALSE);

            es3Ast = ParseViaAntlrOnly("2");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.DecimalIntegerLiteral);

            es3Ast = ParseViaAntlrOnly("2.2");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.DecimalFloatingPointLiteral);

            es3Ast = ParseViaAntlrOnly("0x00");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.HexIntegerLiteral);

            es3Ast = ParseViaAntlrOnly("'xai'");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.StringLiteral);

            es3Ast = ParseViaAntlrOnly("\"xai\"");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.StringLiteral);

            es3Ast = ParseViaAntlrOnly(@"/^\d{5}$/");
            token = es3Ast.Token;
            Assert.IsTrue(EcmaScriptV3Syntax.IsLiteral(token));
            Assert.IsTrue(token.Type == EcmaScriptV3Lexer.RegularExpressionLiteral);
        }
    }
}
