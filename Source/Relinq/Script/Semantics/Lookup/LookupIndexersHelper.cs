using System;
using System.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeSystem;
using System.Linq;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.Lookup
{
    public static class LookupIndexersHelper
    {
        // http://code.google.com/p/relinq/wiki/LookupIndexers
        public static MethodGroup LookupIndexers(this RelinqScriptType rt)
        {
            var t = rt is ClrType ? ((ClrType)rt).Type : null;
            if (t == null)
            {
                return null;
            }
            else
            {
                var alternatives = t.LookupThisAndBaseTypes().Select(bt => bt
                    .GetMethods(BF.PublicInstance)
                    .Where(mi => mi.DeclaringType == bt)
                    .Where(mi => mi.IsIndexer())).Flatten();

                return alternatives.IsNullOrEmpty() ? null : new MethodGroup(alternatives, "[]");
            }
        }
    }
}
