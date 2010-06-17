namespace Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution
{
    public enum MethodMatchingExceptionType
    {
        Unexpected,
        ArgcMismatch,
        GenericArgInferenceFailed,
        SignatureFailsMetaConstrains,
        ActualArgCannotBeCastToFormalArg,
        SignatureIsSubPar,
        SignatureIsEntrapped,
        SignatureIsOkButAmbiguous,
    }
}