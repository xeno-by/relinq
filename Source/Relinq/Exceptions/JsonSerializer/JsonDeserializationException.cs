using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Exceptions.JsonSerializer
{
    public class JsonDeserializationException : RelinqException
    {
        [IncludeInMessage]
        public JsonDeserializationExceptionType Type { get; private set; }

        [IncludeInMessage]
        public String Root { get; private set; }

        [IncludeInMessage]
        public Type RootExpectedType { get; private set; }

        [IncludeInMessage]
        public String Target { get; private set; }

        [IncludeInMessage]
        public Type ExpectedType { get; private set; }

        [IncludeInMessage]
        protected ITypeSerializationMetadata Metadata
        {
            get { return ExpectedType == null ? null : MetadataRepository.Get(ExpectedType); }
        }

        [IncludeInMessage]
        public override bool IsUnexpected
        {
            get
            {
                return Type == JsonDeserializationExceptionType.Unexpected ||
                    Type == JsonDeserializationExceptionType.JsonParseError;
            }
        }

        public JsonDeserializationException(JsonDeserializationExceptionType type, String root, Type rootExpectedType, String target, Type expectedType)
        {
            Type = type;

            Root = root;
            RootExpectedType = rootExpectedType;
            Target = target;
            ExpectedType = expectedType;
        }

        public JsonDeserializationException(JsonDeserializationExceptionType type, String root, Type rootExpectedType, String target, Type expectedType, Exception innerException)
            : base(innerException)
        {
            Type = type;

            Root = root;
            RootExpectedType = rootExpectedType;
            Target = target;
            ExpectedType = expectedType;
        }
    }
}