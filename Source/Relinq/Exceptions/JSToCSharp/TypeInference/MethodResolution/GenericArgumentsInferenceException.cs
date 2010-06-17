using System;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Script.Compilation;

namespace Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution
{
    public class GenericArgumentsInferenceException : MethodResolutionException
    {
        [IncludeInMessage]
        public GenericArgumentsInferenceExceptionType Type { get; private set; }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get { return Type == GenericArgumentsInferenceExceptionType.Unexpected; }
        }

        public GenericArgumentsInferenceException(GenericArgumentsInferenceExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine)
            : this(type, originalSignature, inferredSignature, engine, null, null)
        {
            
        }

        public GenericArgumentsInferenceException(GenericArgumentsInferenceExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, int? mismatchIndex)
            : this(type, originalSignature, inferredSignature, engine, mismatchIndex, null)
        {

        }

        public GenericArgumentsInferenceException(GenericArgumentsInferenceExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, Exception innerException)
            : this(type, originalSignature, inferredSignature, engine, null, innerException)
        {
            
        }

        public GenericArgumentsInferenceException(GenericArgumentsInferenceExceptionType type, MethodInfo originalSignature, MethodInfo inferredSignature, TypeInferenceEngine engine, int? mismatchIndex, Exception innerException)
            : base(originalSignature, inferredSignature, engine, mismatchIndex, innerException)
        {
            Type = type;
        }
    }
}
