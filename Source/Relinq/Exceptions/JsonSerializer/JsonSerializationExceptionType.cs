namespace Relinq.Exceptions.JsonSerializer
{
    public enum JsonSerializationExceptionType
    {
        Unexpected,
        ExpectedTypeNotSpecified,
        InsufficientMetadata,
        ActualVsExpectedTypeMismatch,
    }
}