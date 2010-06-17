using System;
using System.Linq;
using Antlr.Runtime;
using Relinq.Helpers;
using Relinq.Script.Syntax.Grammar;

namespace Relinq.Script.Syntax.Expressions
{
    public class ConstantExpression : RelinqScriptExpression
    {
        public IToken Token { get; private set; }
        public String Data { get; private set; }

        // simple constants: null, booleans, numbers, strings
        public ConstantExpression(IToken token)
            : base(ExpressionType.Constant, Enumerable.Empty<RelinqScriptExpression>())
        {
            Token = token;
            Data = Token.Text;
        }

        // complex constants: objects and arrays
        public ConstantExpression(IToken token, String data)
            : base(ExpressionType.Constant, Enumerable.Empty<RelinqScriptExpression>())
        {
            Token = token;
            Data = data;
        }

        protected override string GetTPathNode() { return 
            String.Format("c:({0}, {1})", Data, Token.Type.GetSymbolicName<EcmaScriptV3Parser>()); }
        protected override string GetTPathSuffix(int childIndex) { return null; }
        protected override string GetContent() { return Data; }
    }
}