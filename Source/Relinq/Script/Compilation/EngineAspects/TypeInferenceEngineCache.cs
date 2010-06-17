using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.Closures;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Compilation.EngineAspects
{
    // todo. thread-safe version (if caching will become a final choice)
    public class TypeInferenceEngineCache
    {
        private class DehydratedClosure : List<DehydratedClosureEntry> { }
        private class DehydratedClosureEntry
        {
            public LambdaExpression Lambda { get; private set; }
            public int Index { get; private set; }

            public DehydratedClosureEntry(LambdaExpression lambda, int index)
            {
                Lambda = lambda;
                Index = index;
            }
        }

        private Dictionary<RelinqScriptExpression, DehydratedClosure> _closuresCache =
            new Dictionary<RelinqScriptExpression, DehydratedClosure>();

        private DehydratedClosure GetDehydratedClosure(RelinqScriptExpression e)
        {
            if (!_closuresCache.ContainsKey(e))
            {
                var hydrated = e.GetClosure().OrderBy(kvp => kvp.Key).Select(kvp =>
                    new { Lambda = kvp.Value, Index = Array.IndexOf(kvp.Value.Args.ToArray(), kvp.Key) });

                _closuresCache.Add(e, new DehydratedClosure());
                hydrated.ForEach(h => _closuresCache[e].Add(new DehydratedClosureEntry(h.Lambda, h.Index)));
            }

            return _closuresCache[e];
        }

        private IEnumerable<Type> HydrateClosure(DehydratedClosure dc, TypeInferenceCache c)
        {
            return dc.Select(entry => ((Lambda)c[entry.Lambda]).Type.XGetGenericArguments()[entry.Index]);
        }

        private TypeInferenceOperationKey GetKey(RelinqScriptExpression e, TypeInferenceCache c)
        {
            return new TypeInferenceOperationKey(e, c.ContainsKey(e) ? c[e] : null,
                HydrateClosure(GetDehydratedClosure(e), c));
        }

        private Dictionary<TypeInferenceOperationKey, TypeInferenceCache> _cachedInferences = 
            new Dictionary<TypeInferenceOperationKey, TypeInferenceCache>();

        public void RunThroughCache(RelinqScriptExpression e, TypeInferenceCache c,
            Action<RelinqScriptExpression, TypeInferenceCache> operation)
        {
            var key = GetKey(e, c);
            if (!_cachedInferences.ContainsKey(key))
            {
                operation(e, c);
                _cachedInferences.Add(key, c.Clone());
            }

            var result = _cachedInferences[key];
            c.Upgrade(result, e);
        }
    }
}