using System;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public interface IMethodInfo
    {
        Type DeclaringType { get; }
        String Name { get; }
        Type ReturnType { get; }
        IParameterInfo[] GetParameters();

        Type[] GetMethodGenericArguments();
        Type[] GetTypeGenericArguments();

        IMethodInfo GetGenericPattern();
        IMethodInfo ImplementGenericPattern(Type[] typeArgs, Type[] methodArgs);

        MethodInfo ToRuntimeMethod();
    }
}