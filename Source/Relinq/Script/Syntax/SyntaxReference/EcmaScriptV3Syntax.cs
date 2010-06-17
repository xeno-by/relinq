using System;
using System.Collections.Generic;
using Antlr.Runtime;
using Relinq.Script.Syntax.Grammar;

namespace Relinq.Script.Syntax.SyntaxReference
{
    public static class EcmaScriptV3Syntax
    {
        private static HashSet<String> _keywords = new HashSet<String>(new String[]{
            "break", "case", "catch", "continue",
            "default", "delete", "do", "else",
            "finally", "for", "function", "if",
            "in", "instanceof", "new", "return",
            "switch", "this", "throw", "try",
            "typeof", "var", "void", "while", "with"});

        private static HashSet<String> _futureReservedKeywords = new HashSet<String>(new String[]{
            "abstract", "boolean", "byte", "char",
            "class", "const", "debugger", "double",
            "enum", "export", "extends", "final",
            "float", "goto", "implements", "import",
            "int", "interface", "long", "native",
            "package", "private", "protected", "public",
            "short", "static", "super", "synchronized",
            "throws", "transient", "volatile"});

        private static HashSet<String> _literalKeywords = new HashSet<String>(new String[]{
            "true", "false", "null"});

        private static HashSet<int> _unclassifiedLiteralTokens = new HashSet<int>(new int[]{
            EcmaScriptV3Lexer.NULL,
            EcmaScriptV3Lexer.TRUE,
            EcmaScriptV3Lexer.FALSE,
            EcmaScriptV3Lexer.RegularExpressionLiteral});

        private static HashSet<int> _integerLiteralTokens = new HashSet<int>(new int[]{
            EcmaScriptV3Lexer.DecimalIntegerLiteral,
            EcmaScriptV3Lexer.OctalIntegerLiteral,
            EcmaScriptV3Lexer.HexIntegerLiteral});

        private static HashSet<int> _floatingPointLiteralTokens = new HashSet<int>(new int[]{
            EcmaScriptV3Lexer.DecimalFloatingPointLiteral});

        private static HashSet<int> _stringLiteralTokens = new HashSet<int>(new int[]{
            EcmaScriptV3Lexer.StringLiteral});

        private static int _objectInitializerToken = EcmaScriptV3Lexer.OBJECT;
        private static int _arrayInitializerToken = EcmaScriptV3Lexer.ARRAY;

        public static bool IsKeyword(IToken token)
        {
            return 
                _keywords.Contains(token.Text) || 
                _futureReservedKeywords.Contains(token.Text) || 
                _literalKeywords.Contains(token.Text);
        }

        public static bool IsLiteral(IToken token)
        {
            return _unclassifiedLiteralTokens.Contains(token.Type) ||
                _integerLiteralTokens.Contains(token.Type) ||
                _floatingPointLiteralTokens.Contains(token.Type) ||
                _stringLiteralTokens.Contains(token.Type);
        }

        public static bool IsInteger(IToken token)
        {
            return _integerLiteralTokens.Contains(token.Type);
        }

        public static bool IsFloat(IToken token)
        {
            return _floatingPointLiteralTokens.Contains(token.Type);
        }

        public static bool IsString(IToken token)
        {
            return _stringLiteralTokens.Contains(token.Type);
        }

        public static bool IsObject(IToken token)
        {
            return token.Type == _objectInitializerToken;
        }

        public static bool IsArray(IToken token)
        {
            return token.Type == _arrayInitializerToken;
        }

        public static bool IsIdentifier(IToken token)
        {
            return token.Type == EcmaScriptV3Lexer.Identifier;
        }
    }
}