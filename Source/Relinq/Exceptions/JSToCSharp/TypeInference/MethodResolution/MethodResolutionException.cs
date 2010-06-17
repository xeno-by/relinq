using System;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution
{
    public abstract class MethodResolutionException : RelinqException
    {
        [IncludeInMessage]
        public MethodInfo OriginalSignature { get; private set; }

        [IncludeInMessage]
        public MethodInfo InferredSignature { get; private set; }

        [IncludeInMessage]
        public Type[] FormalArguments { get; private set; }

        [IncludeInMessage]
        public RelinqScriptExpression RootExpression { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType[] ActualArguments { get; private set; }

        [IncludeInMessage]
        public int? MismatchIndex { get; private set; }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx)
            : this(originalSignature, inferredSignature, new TypeInferenceEngine(ctx), null, null)
        {
            
        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, int? mismatchIndex)
            : this(originalSignature, inferredSignature, new TypeInferenceEngine(ctx), mismatchIndex, null)
        {

        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, Exception innerException)
            : this(originalSignature, inferredSignature, new TypeInferenceEngine(ctx), null, innerException)
        {
            
        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, int? mismatchIndex, Exception innerException)
            : this(originalSignature, inferredSignature, new TypeInferenceEngine(ctx), mismatchIndex, innerException)
        {
            
        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine)
            : this(originalSignature, inferredSignature, engine, null, null)
        {
            
        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, int? mismatchIndex)
            : this(originalSignature, inferredSignature, engine, mismatchIndex, null)
        {

        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, Exception innerException)
            : this(originalSignature, inferredSignature, engine, null, innerException)
        {
            
        }

        protected MethodResolutionException(MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, int? mismatchIndex, Exception innerException)
            : base(innerException)
        {
            OriginalSignature = originalSignature;
            InferredSignature = inferredSignature;
            RootExpression = engine.Root;
            MismatchIndex = mismatchIndex;

            try
            {
                // ensure that all call args are inferred successfully
                foreach (var arg in engine.Root.CallArgs())
                {
                    // we need this check because depending on the context
                    // call args might already be inferred by the moment of crash
                    if (!engine.Inferences.ContainsKey(arg))
                    {
                        // this is unlikely to crash and ruin our attempt to provide the client with comprehensive info
                        // since type inference for arguments has already been probed before entering TypeInferenceMethods.Resolve
                        engine.InferTypes(arg);
                    }
                }

                ActualArguments = engine.Ctx.Root.ToXArgs(engine.Inferences).ToArray();
                FormalArguments = (InferredSignature ?? OriginalSignature).ToXArgs(ActualArguments.Length).ToArray();

                // the lines below are copy&pasted from GenericArgsInference.cs
                for (var i = 0; i < ActualArguments.Length; ++i)
                {
                    var ai_r = ActualArguments.ElementAt(i);
                    var pi = FormalArguments.ElementAt(i);

                    if (!pi.IsOpenGeneric())
                    {
                        if (pi.IsFunctionType() && ai_r is Lambda)
                        {
                            var ai_s = ((Lambda)ai_r).Type.GetFunctionSignature();
                            var pi_s = pi.GetFunctionSignature();

                            if (ai_s.GetParameters().Length == pi_s.GetParameters().Length)
                            {
                                var callArg = engine.Ctx.Root.CallArgs().ElementAt(i - 1);
                                ActualArguments[i] = new Lambda(
                                    (LambdaExpression)callArg,
                                    (pi_s.GetParameters()
                                        .Select(pi2 => pi2.ParameterType.IsGenericParameter ? 
                                            typeof(Variant) : pi2.ParameterType)
                                        .Concat(typeof(Variant).AsArray()))
                                        .ForgeFuncType());
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // sigh, the client won't get full info from the crash site
                // sad, but true, so just ignore possible exceptions
            }
        }
    }
}