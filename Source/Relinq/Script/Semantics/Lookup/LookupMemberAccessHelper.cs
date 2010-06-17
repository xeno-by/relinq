using System;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.Lookup
{
    // http://code.google.com/p/relinq/wiki/LookupMemberAccess
    public static class LookupMemberAccessHelper
    {
        public static RelinqScriptType LookupMemberAccess(this RelinqScriptType rt, String name)
        {
            var t = rt is ClrType ? ((ClrType)rt).Type : null;
            if (t == null)
            {
                return null;
            }
            else
            {
                var fopt = t.GetFieldOrPropertyType(name);
                return fopt ?? rt.LookupMethodGroup(name);
            }
        }

        public static RelinqScriptType LookupMethodGroup(this RelinqScriptType rt, String name)
        {
            var t = rt is ClrType ? ((ClrType)rt).Type : null;
            if (t == null)
            {
                return null;
            }
            else
            {
                var instanceMethods = t.LookupThisAndBaseTypes().Select(bt => bt
                .GetMember(name, MemberTypes.Method, BF.PublicInstance)
                .Cast<MethodInfo>()
                .Where(mi => mi.DeclaringType == bt))
                .Flatten();

                Func<Type, MethodInfo[]> emiGetter = emiHost => emiHost
                    .GetMember(name, MemberTypes.Method, BF.PublicStatic)
                    .Cast<MethodInfo>()
                    .Where(emi => emi.IsExtension())
                    .ToArray();

                var alternatives = instanceMethods
                    .Concat(emiGetter(typeof(Enumerable))
                        .Concat(emiGetter(typeof(Queryable))));
                alternatives = alternatives.Where(alt =>
                    alt.GetParameters().All(pi => !pi.IsOut && !pi.ParameterType.IsByRef));

                return alternatives.IsNullOrEmpty() ? null : new MethodGroup(alternatives, name);
            }
        }
    }
}
