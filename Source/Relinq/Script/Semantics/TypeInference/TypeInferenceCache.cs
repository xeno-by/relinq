using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using Relinq.Helpers.Collections;
using System.Linq;

namespace Relinq.Script.Semantics.TypeInference
{
    public class TypeInferenceCache : InitOnlyDictionary<RelinqScriptExpression, RelinqScriptType>
    {
        public InitOnlyDictionary<RelinqScriptExpression, ResolvedInvocation> Invocations =
            new InitOnlyDictionary<RelinqScriptExpression, ResolvedInvocation>();

        public TypeInferenceCache Clone()
        {
            var clone = new TypeInferenceCache();
            clone.AddRange(this);
            clone.Invocations.AddRange(this.Invocations);
            return clone;
        }

        public void Upgrade(TypeInferenceCache source)
        {
            source.ForEach(kvp => this[kvp.Key] = kvp.Value);
            source.Invocations.ForEach(kvp => this.Invocations[kvp.Key] = kvp.Value);
        }

        public void Upgrade(TypeInferenceCache source, RelinqScriptExpression root)
        {
            source.Where(kvp => kvp.Key.IsIndirectChildOf(root))
                .ForEach(kvp => this[kvp.Key] = kvp.Value);

            source.Invocations.Where(kvp => kvp.Key.IsIndirectChildOf(root))
                .ForEach(kvp => this.Invocations[kvp.Key] = kvp.Value);
        }
    }
}