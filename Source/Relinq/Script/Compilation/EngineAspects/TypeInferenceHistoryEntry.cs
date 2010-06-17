using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Compilation.EngineAspects
{
    public class TypeInferenceHistoryEntry
    {
        public Guid Id { get; private set; }
        public RelinqScriptExpression Expression { get; set; }
        public RelinqScriptType InferredType { get; set; }
        public ResolvedInvocation InferredInvocation { get; set; }
        public Dictionary<String, RelinqScriptType> Closure { get; set; }
        public bool Finished { get; set; }

        public TypeInferenceHistoryEntry()
        {
            Id = Guid.NewGuid();
            Closure = new Dictionary<String, RelinqScriptType>();
        }

        public TypeInferenceHistoryEntry(TypeInferenceHistoryEntry entry)
        {
            Id = entry.Id;
            Expression = entry.Expression;
            Closure = entry.Closure;
            Finished = true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TypeInferenceHistoryEntry;
            return other != null && Finished == other.Finished && Expression == other.Expression &&
                InferredType == other.InferredType && ClosureToString == other.ClosureToString;
        }

        public override int GetHashCode()
        {
            return Finished.GetHashCode() ^
                (Expression == null ? 0 : Expression.GetHashCode()) ^
                (InferredType == null ? 0 : InferredType.GetHashCode()) ^
                (ClosureToString == null ? 0 : ClosureToString.GetHashCode());
        }

        private String ClosureToString
        {
            get
            {
                return Closure.OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Key + " = " + kvp.Value).StringJoin();
            }
        }

        public override string ToString()
        {
            if (!Finished)
            {
                return String.Format(
                    "Started '{0}', info so far is '{1}' + [{2}]'",
                    Expression, InferredType, ClosureToString);
            }
            else
            {
                if (InferredType == null)
                {
                    return "EXCEPTION";
                }
                else
                {
                    if (InferredInvocation == null)
                    {
                        return String.Format(
                            "Finished '{0}', inferred '{1}'",
                            Expression, InferredType);
                    }
                    else
                    {
                        return String.Format(
                            "Finished '{0}', inferred '{1}'",
                            Expression, InferredInvocation);
                    }
                }
            }
        }
    }
}