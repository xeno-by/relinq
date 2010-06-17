using System;
using Relinq.Exceptions.Core;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Exceptions.JsonSerializer
{
    public class JsonSerializationException : RelinqException
    {
        [IncludeInMessage]
        public JsonSerializationExceptionType Type { get; private set; }

        [IncludeInMessage]
        public Object Root { get; private set; }

        [IncludeInMessage]
        protected Type RootActualType { get { return Root == null ? null : Root.GetType(); } }

        [IncludeInMessage]
        public Type RootExpectedType { get; private set; }

        [IncludeInMessage]
        public Object Target { get; private set; }

        [IncludeInMessage]
        protected Type ActualType { get { return Target == null ? null : Target.GetType(); } }

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
            get { return Type == JsonSerializationExceptionType.Unexpected; }
        }

        public JsonSerializationException(JsonSerializationExceptionType type, Object root, Type rootExpectedType, Object target, Type expectedType)
        {
            Type = type;

            Root = root;
            RootExpectedType = rootExpectedType;
            Target = target;
            ExpectedType = expectedType;
        }

        public JsonSerializationException(JsonSerializationExceptionType type, Object root, Type rootExpectedType, Object target, Type expectedType, Exception innerException)
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