using System;
using System.Reflection;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.Helpers.Reflection
{
    public static class ComprehensiveLoggingHelper
    {
        // also includes attributes in the string representation
        // supported targets: method, retval, args, generic type params
        public static String ToComprehensiveString(this MethodInfo mi)
        {
            var retvalAttrs = mi.ReturnTypeCustomAttributes.ToComprehensiveString();
            var retval = mi.ReturnType.ToComprehensiveString();
            var methodAttrs = ((ICustomAttributeProvider)mi).ToComprehensiveString();
            var name = mi.Name;
            var gargc = mi.XGetGenericArguments().IsEmpty() ? String.Empty : "`" + mi.XGetGenericArguments().Count();
            var gargs = mi.XGetGenericArguments().ToComprehensiveString(true);

            var args = mi.GetParameters().Select(pi =>
            {
                var paramAttrs = ((ICustomAttributeProvider)pi).ToComprehensiveString();
                var paramType = pi.ParameterType.ToComprehensiveString();
                var paramName = pi.Name;

                return String.Format("{0}{1}{2} {3}",
                    paramAttrs,
                    paramAttrs.IsEmpty() ? "" : " ",
                    paramType,
                    paramName);
            }).StringJoin();

            return String.Format("{0}{1}{2} {3}{4}{5}{6}{7}({8})",
                retvalAttrs,
                retvalAttrs.IsEmpty() ? "" : " ",
                retval,
                methodAttrs,
                methodAttrs.IsEmpty() ? "" : " ",
                name,
                gargc,
                gargs,
                args);
        }

        // sanely handles generic type arguments in full name
        public static String ToComprehensiveString(this Type t)
        {
            return t.ToComprehensiveString(false);
        }

        public static String ToComprehensiveString(this Type t, bool withCaps)
        {
            if (t.IsGenericParameter)
            {
                return t.Name;
            }
            else
            {
                var caps = t.IsGenericParameter && withCaps ? ((ICustomAttributeProvider)t).ToComprehensiveString() : String.Empty;
                return caps + t.Namespace + "." + t.Name + t.XGetGenericArguments().ToComprehensiveString();
            }
        }

        // sanely handles generic type arguments in full name
        public static String ToComprehensiveString(this Type[] types)
        {
            return types.ToComprehensiveString(false);
        }

        public static String ToComprehensiveString(this Type[] types, bool withCaps)
        {
            var sj = (types ?? new Type[0]).Select(t => t.ToComprehensiveString(withCaps)).StringJoin();
            if (sj.Length > 0) sj = "[" + sj + "]";
            return sj;
        }

        // correctly formats attribute strings as they're written in code
        public static String ToComprehensiveString(this ICustomAttributeProvider cap)
        {
            var sj = (cap.Attrs<Attribute>(true) ?? new Attribute[0]).Select(a => 
                a.GetType().Name.EndsWith("Attribute") ? 
                a.GetType().Name.Substring(0, a.GetType().Name.Length - "Attribute".Length) :
                a.GetType().Name).StringJoin();
            if (sj.Length > 0) sj = "[" + sj + "]";
            return sj;
        }
    }
}