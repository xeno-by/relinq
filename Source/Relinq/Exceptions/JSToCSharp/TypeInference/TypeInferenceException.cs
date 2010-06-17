using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Compilation;
using Relinq.Script.Compilation.EngineAspects;
using Relinq.Script.Syntax.Expressions;
using System.Linq;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class TypeInferenceException : RelinqException
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
        internal TypeInferenceHistoryEntry[] History { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get 
            { 
                return Type == JSToCSharpExceptionType.Unexpected ||
                    Type == JSToCSharpExceptionType.UnexpectedJsConstruct;
            }
        }

        public TypeInferenceException(JSToCSharpExceptionType type, RelinqScriptExpression root, RelinqScriptExpression expression)
            : this(type, root, expression, null)
        {
        }

        public TypeInferenceException(JSToCSharpExceptionType type, RelinqScriptExpression root, RelinqScriptExpression expression, Exception innerException)
            : base(innerException)
        {
            Type = type;
            Root = root;
            Expression = expression;

            var full = TypeInferenceEngine._sap.History.ToArray();
            History = full.Where(e => e.Expression.IsIndirectChildOf(root)).ToArray();
        }
    }
}