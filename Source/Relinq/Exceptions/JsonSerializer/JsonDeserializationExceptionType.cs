namespace Relinq.Exceptions.JsonSerializer
{
    public enum JsonDeserializationExceptionType
    {
        Unexpected,
        ExpectedTypeNotSpecified,
        InsufficientMetadata,
        NoMatchingCtor,
        NoMatchingProperty,
        JsonParseError,
    }
}