using System;
using System.Linq;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    public abstract class StaticToInstanceMethodForgerBase : IMethodForger
    {
        public Type GetDeclaringType(MethodInfo method)
        {
            return method.GetParameters()[0].ParameterType;
        }

        public string GetName(MethodInfo method)
        {
            return method.Name;
        }

        public Type GetReturnType(MethodInfo method)
        {
            return method.ReturnType;
        }

        public IParameterInfo[] GetParameters(MethodInfo method)
        {
            return method.GetParameters()
                .Skip(1)
                .Select(p => ForgedParameterInfo.Forge(p.ParameterType))
                .ToArray();
        }

        public abstract Type[] GetMethodGenericArguments(MethodInfo method);
        public abstract Type[] GetTypeGenericArguments(MethodInfo method);
        public abstract IMethodInfo GetGenericPattern(MethodInfo method);
        public abstract IMethodInfo ImplementGenericPattern(MethodInfo method, Type[] typeArgs, Type[] methodArgs);
    }
}