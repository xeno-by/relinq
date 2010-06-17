using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using Relinq.Helpers.Collections;

namespace Relinq.Exceptions.JSToCSharp.TypeInference
{
    public class MethodGroupResolutionFailedException : TypeInferenceException
    {
        [IncludeInMessage]
        public MethodGroup MethodGroup { get; private set; }

        [IncludeInMessage]
        public Dictionary<MethodInfo, MethodResolutionException> Failboats { get; private set; }

        [IncludeInMessage]
        public RelinqScriptType[] ActualArguments { get; private set; }

        public MethodGroupResolutionFailedException(JSToCSharpExceptionType type, MethodGroup methodGroup, Dictionary<MethodInfo, MethodResolutionException> failboats, TypeInferenceContext ctx)
            : this(type, methodGroup, failboats, ctx, null)
        {
        }

        public MethodGroupResolutionFailedException(JSToCSharpExceptionType type, MethodGroup methodGroup, Dictionary<MethodInfo, MethodResolutionException> failboats, TypeInferenceContext ctx, Exception innerException)
            : base(type, ctx.Root.AsArray().Concat(ctx.Root.Parents()).Last(), ctx.Root, innerException)
        {
            MethodGroup = methodGroup;
            Failboats = failboats;

            try
            {
                ActualArguments = ctx.Root.ToXArgs(ctx.Inferences).ToArray();
            }
            catch (Exception)
            {
                // sigh, the client won't get full info from the crash site
                // sad, but true, so just ignore possible exceptions
            }
        }
    }
}