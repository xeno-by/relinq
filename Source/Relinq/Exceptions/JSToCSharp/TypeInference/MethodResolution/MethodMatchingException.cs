using System;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.TypeInference;

namespace Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution
{
    public class MethodMatchingException : MethodResolutionException
    {
        [IncludeInMessage]
        public MethodMatchingExceptionType Type { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == MethodMatchingExceptionType.Unexpected; }
        }

        public MethodMatchingException(MethodMatchingExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx)
            : this(type, originalSignature, inferredSignature, ctx, null, null)
        {

        }

        public MethodMatchingException(MethodMatchingExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, int? mismatchIndex)
            : this(type, originalSignature, inferredSignature, ctx, mismatchIndex, null)
        {

        }

        public MethodMatchingException(MethodMatchingExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, Exception innerException)
            : this(type, originalSignature, inferredSignature, ctx, null, innerException)
        {

        }

        public MethodMatchingException(MethodMatchingExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceContext ctx, int? mismatchIndex, Exception innerException)
            : base(originalSignature, inferredSignature, ctx, mismatchIndex, innerException)
        {
            Type = type;
        }
    }
}