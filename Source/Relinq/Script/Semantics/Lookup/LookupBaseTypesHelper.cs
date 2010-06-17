using System;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using System.Linq;

namespace Relinq.Script.Semantics.Lookup
{
    // http://code.google.com/p/relinq/wiki/LookupBaseTypes
    public static class LookupBaseTypesHelper
    {
        public static Type[] LookupBaseTypes(this Type t)
        {
            var baseTypes = t.GetBaseTypes();
            if (t.IsDelegate()) baseTypes = baseTypes.Except(
                typeof(MulticastDelegate).AsArray()).ToArray();
            return baseTypes;
        }

        public static Type[] LookupThisAndBaseTypes(this Type t)
        {
            return t.AsArray().Concat(t.LookupBaseTypes()).ToArray();
        }
    }
}
