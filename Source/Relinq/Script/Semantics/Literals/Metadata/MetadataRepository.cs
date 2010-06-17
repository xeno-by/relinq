using System;
using System.Collections.Generic;

namespace Relinq.Script.Semantics.Literals.Metadata
{
    public static class MetadataRepository
    {
        private static readonly Dictionary<Type, ITypeSerializationMetadata> _repo = 
            new Dictionary<Type, ITypeSerializationMetadata>();

        public static ITypeSerializationMetadata Get(Type t)
        {
            if (!_repo.ContainsKey(t))
            {
                _repo.Add(t, new DefaultSerializationMetadata(t));
            }

            return _repo[t];
        }
    }
}
