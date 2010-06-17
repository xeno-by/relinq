using System;
using System.Reflection;
using System.Linq;
using Relinq.Helpers.Collections;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public class DotNetMethodInfo : IMethodInfo
    {
        private MethodInfo _methodInfo;
        private DotNetMethodInfo(MethodInfo mi)
        {
            _methodInfo = mi;
        }

        public MethodInfo ToRuntimeMethod()
        {
            return _methodInfo;
        }

        public static IMethodInfo FromRuntimeMethod(MethodInfo mi)
        {
            return new DotNetMethodInfo(mi);
        }

        public Type DeclaringType
        {
            get { return _methodInfo.DeclaringType; }
        }

        public string Name
        {
            get { return _methodInfo.Name; }
        }

        public Type ReturnType
        {
            get { return _methodInfo.ReturnType; }
        }

        public IParameterInfo[] GetParameters()
        {
            return _methodInfo.GetParameters()
                .Select(param => DotNetParameterInfo.FromRuntimeParameter(param))
                .ToArray();
        }

        public Type[] GetMethodGenericArguments()
        {
            return _methodInfo.IsGenericMethod ? _methodInfo.GetGenericArguments() : Enumerable.Empty<Type>().ToArray();
        }

        public Type[] GetTypeGenericArguments()
        {
            return DeclaringType.IsGenericType ? DeclaringType.GetGenericArguments() : Enumerable.Empty<Type>().ToArray();
        }

        public IMethodInfo GetGenericPattern()
        {
            // very gimmick tho concise solution
            return FromRuntimeMethod((MethodInfo)_methodInfo.Module.ResolveMethod(_methodInfo.MetadataToken));
        }

        public IMethodInfo ImplementGenericPattern(Type[] typeArgs, Type[] methodArgs)
        {
            var pattern = GetGenericPattern();

            var typeImpl = pattern.DeclaringType;
            if (!typeArgs.IsNullOrEmpty()) typeImpl = typeImpl.MakeGenericType(typeArgs);

            var methodImpl = typeImpl.GetMethods(
                BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static)
                .Single(mi => mi.MetadataToken == _methodInfo.MetadataToken);
            if (!methodArgs.IsNullOrEmpty()) methodImpl = methodImpl.MakeGenericMethod(methodArgs);

            return FromRuntimeMethod(methodImpl);
        }
    }
}