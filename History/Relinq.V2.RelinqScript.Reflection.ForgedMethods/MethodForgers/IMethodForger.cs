using System;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    public interface IMethodForger
    {
        Type GetDeclaringType(MethodInfo method);
        String GetName(MethodInfo method);
        Type GetReturnType(MethodInfo method);
        IParameterInfo[] GetParameters(MethodInfo method);

        Type[] GetMethodGenericArguments(MethodInfo method);
        Type[] GetTypeGenericArguments(MethodInfo method);

        IMethodInfo GetGenericPattern(MethodInfo method);
        IMethodInfo ImplementGenericPattern(MethodInfo method, Type[] typeArgs, Type[] methodArgs);
    }
}