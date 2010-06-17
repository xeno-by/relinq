using System;
using System.Collections.Generic;
using System.Linq;

namespace Relinq.Script.Semantics.TypeSystem.Anonymous
{
    public static class AnonymousTypesHelper
    {
        public static Type ForgeTupleType(IEnumerable<String> args, IEnumerable<Type> types)
        {
            return new AnonymousTypeBuilder(args.ToArray(), types.ToArray()).ToAnonymousType();
        }
    }
}