using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Relinq.Helpers.Collections;

namespace Relinq.Helpers.Reflection
{
    public static class EmitHelper
    {
        public static void DefineAttribute<A>(object builder, params object[] @params)
        {
            DefineAttribute(typeof(A), builder, @params);
        }

        public static void DefineAttribute(Type a, object builder, params object[] @params)
        {
            var ctor = a.GetConstructor(@params.Select(param => param.GetType()).ToArray());
            builder.SetCustomAttribute(ctor, @params);
        }

        private static void SetCustomAttribute(this object builder, ConstructorInfo ci, params object[] args)
        {
            var method = builder.GetType().GetMethod("SetCustomAttribute",
                new Type[] { typeof(CustomAttributeBuilder) });
            method.Invoke(builder, new CustomAttributeBuilder(ci, args).AsArray());
        }

        public static void CopyAttributesTo(this ICustomAttributeProvider source, object destination)
        {
            source.CopyAttributesTo(destination, cad => true);
        }
        
        public static void CopyAttributesTo(this ICustomAttributeProvider source,
            object destination, Func<CustomAttributeData, bool> filter)
        {
            // todo. lets just hope this will return all inherited datum
            foreach (var cad in source.GetCustomAttributeData().Where(filter))
            {
                // todo. support stuff more complex than just ctor invocation
                if (cad.NamedArguments.Count != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    destination.SetCustomAttribute(cad.Constructor, 
                        cad.ConstructorArguments.Select(arg => arg.Value).ToArray());
                }
            }
        }

        private static IEnumerable<CustomAttributeData> GetCustomAttributeData(this ICustomAttributeProvider cap)
        {
            if (cap is Assembly)
            {
                return CustomAttributeData.GetCustomAttributes((Assembly)cap);
            }
            else if (cap is Module)
            {
                return CustomAttributeData.GetCustomAttributes((Module)cap);
            }
            else if (cap is MemberInfo)
            {
                return CustomAttributeData.GetCustomAttributes((MemberInfo)cap);
            }
            else if (cap is ParameterInfo)
            {
                return CustomAttributeData.GetCustomAttributes((ParameterInfo)cap);
            }
            else
            {
                throw new NotSupportedException(cap.GetType().ToString());
            }
        }
    }
}