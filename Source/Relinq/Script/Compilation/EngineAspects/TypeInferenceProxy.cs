#define TRACE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.Closures;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Compilation.EngineAspects
{
    public class TypeInferenceProxy
    {
        // todo. if caching is here to stay we need to think about a way to clean the cache up
        private TypeInferenceEngineCache Cache { get; set; }

        public TypeInferenceProxy() 
        {
            History = new List<TypeInferenceHistoryEntry>();
        }

        public TypeInferenceProxy(TypeInferenceEngineCache cache) 
        {
            Cache = cache;
            History = new List<TypeInferenceHistoryEntry>();
        }

        public void InferTypes(RelinqScriptExpression e, TypeInferenceCache cache,
            Action<RelinqScriptExpression, TypeInferenceCache> impl)
        {
            Action<RelinqScriptExpression, TypeInferenceCache> wrappedImpl = (e1, cache1) =>
            {
#if TRACE
                var id = BeforeCall(e, cache);

                try
                {
                    impl(e, cache);
                }
                finally
                {
                    AfterCall(id, e, cache);
                }
#else
                impl(e, cache);
#endif
            };

            if (Cache == null)
            {
                wrappedImpl(e, cache);
            }
            else
            {
                Cache.RunThroughCache(e, cache, wrappedImpl);
            }
        }

        private Guid BeforeCall(RelinqScriptExpression e, TypeInferenceCache cache)
        {
            var startedEntry = new TypeInferenceHistoryEntry{Expression = e};
            startedEntry.InferredType = cache.ContainsKey(e) ? cache[e] : null;
            History.Add(startedEntry);

            Func<LambdaExpression, String, RelinqScriptType> argType = (lambda, arg) => {
                var argIndex = Array.IndexOf(lambda.Args.ToArray(), arg);
                var lambdaType = ((Lambda)cache[lambda]).Type.GetFunctionSignature();
                return lambdaType.GetParameters()[argIndex].ParameterType;
            };

            e.GetClosure().ForEach(kvp => startedEntry.Closure.Add(kvp.Key,
                cache.ContainsKey(kvp.Value) ? argType(kvp.Value, kvp.Key) : null));

            return startedEntry.Id;
        }

        private void AfterCall(Guid id, RelinqScriptExpression e, TypeInferenceCache cache)
        {
            var startedEntry = History.Single(entry2 => entry2.Id == id);
            var finishedEntry = new TypeInferenceHistoryEntry(startedEntry);

            finishedEntry.InferredType = cache.ContainsKey(e) ? cache[e] : null;

            if (e.IsCall())
            {
                RelinqScriptExpression invtarget = null;
                if (e is InvokeExpression)
                {
                    invtarget = e.Children.ElementAt(0);
                }
                else if (e is IndexerExpression || e is OperatorExpression)
                {
                    invtarget = e;
                }

                if (invtarget != null && cache.Invocations.ContainsKey(invtarget))
                {
                    finishedEntry.InferredInvocation = cache.Invocations[invtarget];
                }
            }

            History.Add(finishedEntry);
            if (History.Count % 20000 == 0) DumpHistory();
        }

        internal List<TypeInferenceHistoryEntry> History { get; set; }

        public void ClearHistory()
        {
            History.Clear();
        }

        public void DumpHistory()
        {
            if (Directory.Exists(@"d:\inferences\"))
            {
                if (!History.IsNullOrEmpty())
                {
                    using (var sw = new StreamWriter(@"d:\inferences\" + Guid.NewGuid() + ".log"))
                    {
                        Func<int, String> tab = depth => new String(' ', depth * 2);
                        History.ForEach(entry => sw.WriteLine(tab(entry.Expression.Depth()) + entry));
                    }
                }
            }
        }
    }
}