using Relinq.Exceptions.Core;
using Relinq.Script.Compilation;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.CSharpBuilder
{
    public class CannotResolveQuasiTypeFromContextException : CSharpBuilderException
    {
        [IncludeInMessage]
        public RelinqScriptExpression Parent
        {
            get { return Expression.Parent; }
        }

        [IncludeInMessage]
        public ExpressionType? ParentType
        {
            get { return Parent == null ? null : (ExpressionType?)Parent.NodeType; }
        }

        public CannotResolveQuasiTypeFromContextException(RelinqScriptExpression root, RelinqScriptExpression expression, CompilationContext ctx)
            : base(JSToCSharpExceptionType.CannotResolveQuasiTypeFromContext, root, expression, ctx)
        {
        }
    }
}