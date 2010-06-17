using System;
using System.Reflection;
using System.Linq;
using Relinq.RelinqScript.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    public class NaturalCoercionMethodForger : StaticToInstanceMethodForgerBase
    {
        public override Type[] GetMethodGenericArguments(MethodInfo method)
        {
            return new Type[0];
        }

        public override Type[] GetTypeGenericArguments(MethodInfo method)
        {
            var owner = GetDeclaringType(method);
            return owner.IsGenericType ? owner.GetGenericArguments() : new Type[0];
        }

        public override IMethodInfo GetGenericPattern(MethodInfo method)
        {
            var owner = GetDeclaringType(method);
            if (!owner.IsGenericType)
            {
                return ForgedMethod.Forge(method, this);
            }
            else
            {
                var convertFromPattern = owner.GetGenericTypeDefinition();

                // todo: fix this workaround
                var convertToPattern = convertFromPattern.ForgeNaturalCoercionMethods()
                    .Where(m => m.ReturnType.SameMetadataToken(GetReturnType(method)))
                    .Select(m => m.ReturnType)
                    .Single();

                var methodImpl = ForgedMethodsHost.NaturalCoercionImpl;
                var patternMethod = methodImpl.MakeGenericMethod(convertFromPattern, convertToPattern);
                return ForgedMethod.Forge(patternMethod, this);
            }
        }

        public override IMethodInfo ImplementGenericPattern(MethodInfo method, Type[] typeArgs, Type[] methodArgs)
        {
            var owner = GetDeclaringType(method);
            if (!owner.IsGenericType)
            {
                return ForgedMethod.Forge(method, this);
            }
            else
            {
                var convertFromPattern = owner.GetGenericTypeDefinition();
                var convertFromImpl = convertFromPattern.MakeGenericType(typeArgs);

                // todo: fix this workaround
                var convertToImpl = convertFromImpl.ForgeNaturalCoercionMethods()
                    .Where(m => m.ReturnType.SameMetadataToken(GetReturnType(method)))
                    .Select(m => m.ReturnType)
                    .Single();

                var methodImpl = ForgedMethodsHost.NaturalCoercionImpl;
                var patternMethod = methodImpl.MakeGenericMethod(convertFromImpl, convertToImpl);
                return ForgedMethod.Forge(patternMethod, this);
            }
        }
    }
}