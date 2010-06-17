using System;
using System.Collections.Generic;

namespace Relinq.Script.Semantics.Literals.Metadata
{
    public interface ITypeSerializationMetadata
    {
        Type Type { get; }
        IEnumerable<String> Properties { get; }

        bool IsList { get; }
        bool IsDictionary { get; }
        bool IsPropertyBag { get; }
    }
}