using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Compilation;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.CSharpBuilder
{
    public class CSharpBuilderException : RelinqException
    {
        [IncludeInMessage]
        public JSToCSharpExceptionType Type { get; private set; }

        [IncludeInMessage]
        public RelinqScriptExpression Root { get; private set; }

        [IncludeInMessage]
        public RelinqScriptExpression Expression { get; private set; }

        [IncludeInMessage]
        public ExpressionType? ExpressionType
        {
            get { return Expression == null ? null : (ExpressionType?)Expression.NodeType; }
        }

#if TRACE
        [IncludeInMessage]
#endif
        public CompilationContext Context { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType InferredType
        {
            get { return Context.Types.ContainsKey(Expression) ? Context.Types[Expression] : null; }
        }

        [IncludeInMessage]
        public ResolvedInvocation InferredInvocation
        {
            get { return Context.Invocations.ContainsKey(Expression) ? Context.Invocations[Expression] : null; }
        }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get
            {
                return Type == JSToCSharpExceptionType.Unexpected ||
                    Type == JSToCSharpExceptionType.UnexpectedInferredAst;
            }
        }

        public CSharpBuilderException(JSToCSharpExceptionType type, RelinqScriptExpression root, RelinqScriptExpression expression, CompilationContext ctx)
            : this(type, root, expression, ctx, null)
        {
        }

        public CSharpBuilderException(JSToCSharpExceptionType type, RelinqScriptExpression root, RelinqScriptExpression expression, CompilationContext ctx, Exception innerException)
            : base(innerException)
        {
            Type = type;
            Root = root;
            Expression = expression;
            Context = ctx;
        }
    }
}
