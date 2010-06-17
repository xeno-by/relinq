namespace Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution
{
    public enum GenericArgumentsInferenceExceptionType
    {
        Unexpected,
        VariantArg,
        ArgcMismatch,
        LambdaArgcMismatch,
        LambdaIncompatibleRetval,
        LambdaInvalidRetval,
        LambdaInconsistentBody,
        InferencesContradictGenericConstraints,
        FormalArgCannotBeInferredFromActualArg,
        IncompleteInferenceSet,
    }
}