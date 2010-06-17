using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.Parser;
using Relinq.Helpers.Collections;
using Relinq.Script.Integration;
using Relinq.Script.Syntax.Grammar;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.Operators;
using Relinq.Script.Syntax.SyntaxReference;
using Relinq.Helpers;
using Relinq.Helpers.Assurance;

namespace Relinq.Script.Compilation
{
    public class RelinqScriptParser
    {
        public String RelinqScriptCode { get; private set; }
        public IntegrationContext Integration { get; private set; }

        private Dictionary<Func<CommonTree, bool>, Func<CommonTree, RelinqScriptExpression>> _rules = 
            new Dictionary<Func<CommonTree, bool>, Func<CommonTree, RelinqScriptExpression>>();

        public RelinqScriptParser(String relinqScriptCode, IntegrationContext integration)
        {
            RelinqScriptCode = relinqScriptCode;
            Integration = integration;

            _rules.Add(t => t.Text == "PAREXPR", ParseParentheses);
            _rules.Add(t => t.Text == "CALL", ParseCall);
            _rules.Add(t => t.Text == "BYINDEX", ParseByIndex);
            _rules.Add(t => t.Text == "BYFIELD", ParseByField);
            _rules.Add(t => t.Text == "function", ParseFunctionDefinition);
            _rules.Add(t => EcmaScriptV3Syntax.IsObject(t.Token), ParseObjectInitializer);
            _rules.Add(t => EcmaScriptV3Syntax.IsArray(t.Token), ParseArrayInitializer);
            _rules.Add(t => OperatorsReference.IsOperator(t.Text), ParseOperator);

            _rules.Add(t => t.Children.Count == 0 && 
                !EcmaScriptV3Syntax.IsKeyword(t.Token) && integration.IsRegisteredJS(t.Text),
                t => new KeywordExpression(t.Text));

            _rules.Add(t => t.Children.Count == 0 && 
                !integration.IsRegisteredJS(t.Text) && EcmaScriptV3Syntax.IsIdentifier(t.Token),
                t => new VariableExpression(t.Text));

            _rules.Add(t => t.Children.Count == 0 && 
                !integration.IsRegisteredJS(t.Text) && EcmaScriptV3Syntax.IsLiteral(t.Token),
                t => new ConstantExpression(t.Token));
        }

        public RelinqScriptExpression Parse()
        {
            var input = new ANTLRStringStream(RelinqScriptCode);
            var lex = new EcmaScriptV3Lexer(input);
            var tokens = new CommonTokenStream(lex);
            var parser = new EcmaScriptV3Parser(tokens);
            var esV3Ast = (CommonTree)parser.program().Tree;
            esV3Ast.RecursivelyReplaceNullListsWithEmptyOnes();

            return Parse(esV3Ast);
        }

        private RelinqScriptExpression Parse(CommonTree astNode)
        {
            try
            {
                // If the text is null, this means that for this very grammar
                // it's a root node of multiple statements => unsupported case
                if (astNode.Text == null)
                {
                    throw new SemanticErrorException(
                        JSToCSharpExceptionType.JsShouldBeSingleExpression, astNode);
                }
                else
                {
                    var applicableRule = _rules.Where(rule => rule.Key(astNode))
                        .Select(rule => rule.Value).SingleOrDefault();
                    if (applicableRule != null)
                    {
                        return applicableRule(astNode);
                    }
                    else
                    {
                        throw new SemanticErrorException(
                            JSToCSharpExceptionType.UnsupportedJsConstruct, astNode);
                    }
                }
            }
            catch (SemanticErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SemanticErrorException(
                    JSToCSharpExceptionType.Unexpected, astNode, ex);
            }
        }

        private RelinqScriptExpression Parse(object astNode)
        {
            return Parse((CommonTree)astNode);
        }

        private RelinqScriptExpression[] Parse(IList astNodes)
        {
            return ParseImpl(astNodes).ToArray();
        }

        private IEnumerable<RelinqScriptExpression> ParseImpl(IList astNodes)
        {
            foreach (CommonTree astNode in astNodes)
            {
                yield return Parse(astNode);
            }
        }

        private RelinqScriptExpression ParseParentheses(CommonTree astNode)
        {
            if (astNode.Children.Count == 1)
            {
                return Parse(astNode.Children[0]);
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseCall(CommonTree astNode)
        {
            if (astNode.Children.Count == 2)
            {
                var argsList = (CommonTree)astNode.Children[1];
                if (argsList.Text == "ARGS")
                {
                    var target = (CommonTree)astNode.Children[0];
                    var args = new List<CommonTree>();
                    foreach (CommonTree arg in argsList.Children) args.Add(arg);

                    return new InvokeExpression(Parse(target), Parse(args));
                }
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseByIndex(CommonTree astNode)
        {
            if (astNode.Children.Count == 2)
            {
                var target = (CommonTree)astNode.Children[0];
                var argsNode = (CommonTree)astNode.Children[1];

                IEnumerable<RelinqScriptExpression> args;

                // a single index is passed without any wrappers
                if (argsNode.Text != "CEXPR")
                {
                    args = Parse(argsNode).AsArray();
                }
                // multiple indices are parsed as CEXPR with multiple child expressions
                else
                {
                    args = Parse(argsNode.Children);
                }

                return new IndexerExpression(Parse(target), args);
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseByField(CommonTree astNode)
        {
            if (astNode.Children.Count == 2)
            {
                return new MemberAccessExpression(
                    ((CommonTree)astNode.Children[1]).Text,
                    Parse(astNode.Children[0]));
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseFunctionDefinition(CommonTree astNode)
        {
            if (astNode.Children.Count == 2)
            {
                var argsList = (CommonTree)astNode.Children[0];
                var bodyBlock = (CommonTree)astNode.Children[1];

                if (argsList.Text == "ARGS" &&
                    bodyBlock.Children.Count == 1 && bodyBlock.Text == "BLOCK")
                {
                    var args = new List<String>();

                    var allArgsAreValid = true;
                    foreach (CommonTree arg in argsList.Children)
                    {
                        args.Add(arg.Text);
                        allArgsAreValid &= arg.Children.Count == 0;
                    }

                    if (allArgsAreValid)
                    {
                        var bodyContent = (CommonTree)bodyBlock.Children[0];
                        if (bodyContent.Text == "return")
                        {
                            bodyContent = (CommonTree)bodyContent.Children[0];
                            return new LambdaExpression(args, Parse(bodyContent));
                        }
                    }
                }
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private bool IsCompileTimeConstant(CommonTree astNode)
        {
            if (astNode.Children.Count == 0)
            {
                return EcmaScriptV3Syntax.IsLiteral(astNode.Token);
            }
            else
            {
                if (astNode.Text == "ARRAY")
                {
                    var isCTConstant = true;
                    foreach (CommonTree item in astNode.Children)
                    {
                        if (item.Text == "ITEM" && item.Children.Count == 1)
                        {
                            isCTConstant &= IsCompileTimeConstant((CommonTree)item.Children[0]);
                        }
                        else
                        {
                            throw new SemanticErrorException(
                                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
                        }
                    }

                    return isCTConstant;
                }
                else if (astNode.Text == "OBJECT")
                {
                    var isCTConstant = true;
                    foreach (CommonTree prop in astNode.Children)
                    {
                        if (prop.Text == "NAMEDVALUE" && prop.Children.Count == 2 &&
                            ((CommonTree)prop.Children[0]).Children.Count == 0)
                        {
                            isCTConstant &= IsCompileTimeConstant((CommonTree)prop.Children[1]);
                        }
                        else
                        {
                            throw new SemanticErrorException(
                                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
                        }
                    }

                    return isCTConstant;
                }
                else
                {
                    return false;
                }
            }
        }

        private String BackToJS(CommonTree astNode)
        {
            IsCompileTimeConstant(astNode).SurelyTrue();

            var js = new StringBuilder();
            if (EcmaScriptV3Syntax.IsLiteral(astNode.Token))
            {
                js.Append(astNode.Text);
            }
            else if (astNode.Text == "ARRAY")
            {
                js.Append("[");
                foreach (CommonTree item in astNode.Children)
                    js.AppendFormat("{0}, ", BackToJS((CommonTree)item.Children[0]));

                if (js[js.Length - 1] != '[') js.Remove(js.Length - 2, 2);
                js.Append("]");
            }
            else if (astNode.Text == "OBJECT")
            {
                js.Append("{");
                foreach (CommonTree prop in astNode.Children)
                {
                    var name = (CommonTree)prop.Children[0];
                    var value = (CommonTree)prop.Children[1];
                    js.AppendFormat("{0} : {1}, ", name.Text, BackToJS(value));
                }

                if (js[js.Length - 1] != '[') js.Remove(js.Length - 2, 2);
                js.Append("}");
            }

            return js.ToString();
        }

        private RelinqScriptExpression ParseObjectInitializer(CommonTree astNode)
        {
            var names = new List<String>();
            var values = new ArrayList();

            var isSetOfNamedValues = true;
            foreach (CommonTree namedValueWannabe in astNode.Children)
            {
                isSetOfNamedValues &=
                    namedValueWannabe.Text == "NAMEDVALUE" &&
                    namedValueWannabe.Children.Count == 2 &&
                    ((CommonTree)namedValueWannabe.Children[0]).Children.Count == 0;

                if (isSetOfNamedValues)
                {
                    names.Add(((CommonTree)namedValueWannabe.Children[0]).Text);
                    values.Add(namedValueWannabe.Children[1]);
                }
            }

            if (isSetOfNamedValues)
            {
                if (IsCompileTimeConstant(astNode))
                {
                    return new ConstantExpression(astNode.Token, BackToJS(astNode));
                }
                else
                {
                    var nvp = new Dictionary<String, RelinqScriptExpression>();
                    for (var i = 0; i < names.Count; ++i)
                    {
                        nvp.Add(names[i], Parse(values[i]));
                    }

                    return new NewExpression(nvp);
                }
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseArrayInitializer(CommonTree astNode)
        {
            var items = new List<CommonTree>();

            var isSetOfArrayItems = true;
            foreach (CommonTree arrayItemWannabe in astNode.Children)
            {
                isSetOfArrayItems &=
                    arrayItemWannabe.Text == "ITEM" &&
                    arrayItemWannabe.Children.Count == 1;

                if (isSetOfArrayItems)
                {
                    items.Add((CommonTree)arrayItemWannabe.Children[0]);
                }
            }

            if (isSetOfArrayItems)
            {
                if (IsCompileTimeConstant(astNode))
                {
                    return new ConstantExpression(astNode.Token, BackToJS(astNode));
                }
                else
                {
                    throw new SemanticErrorException(
                        JSToCSharpExceptionType.ArrayLiteralsShouldBeCompileTimeConstants, astNode);
                }
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }

        private RelinqScriptExpression ParseOperator(CommonTree astNode)
        {
            if (astNode.Children.Count == 1)
            {
                if (OperatorsReference.IsUnaryOp(astNode.Text))
                {
                    return new OperatorExpression(
                        OperatorsReference.GetUnaryOperatorType(astNode.Text),
                        Parse(astNode.Children));
                }
            }
            else if (astNode.Children.Count == 2)
            {
                if (OperatorsReference.IsBinaryOp(astNode.Text))
                {
                    return new OperatorExpression(
                        OperatorsReference.GetBinaryOperatorType(astNode.Text),
                        Parse(astNode.Children));
                }
            }
            else if (astNode.Text == "?" && astNode.Children.Count == 3)
            {
                return new ConditionalExpression(
                    Parse(astNode.Children[0]),
                    Parse(astNode.Children[1]),
                    Parse(astNode.Children[2]));
            }

            throw new SemanticErrorException(
                JSToCSharpExceptionType.UnexpectedJsConstruct, astNode);
        }
    }
}