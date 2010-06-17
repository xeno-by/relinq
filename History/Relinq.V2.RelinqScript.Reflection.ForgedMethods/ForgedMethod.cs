using System;
using System.Reflection;
using Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers;
using Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public class ForgedMethod : IMethodInfo
    {
        private readonly MethodInfo _method;
        private readonly IMethodForger _forger;

        private ForgedMethod(MethodInfo method, IMethodForger forger)
        {
            _method = method;
            _forger = forger;
        }

        public static IMethodInfo Forge(MethodInfo method, IMethodForger forger)
        {
            return new ForgedMethod(method, forger);
        }

        public MethodInfo ToRuntimeMethod()
        {
            return _method;
        }

        public Type DeclaringType
        {
            get { return _forger.GetDeclaringType(_method); }
        }

        public string Name
        {
            get { return _forger.GetName(_method); }
        }

        public Type ReturnType
        {
            get { return _forger.GetReturnType(_method); }
        }

        public IParameterInfo[] GetParameters()
        {
            return _forger.GetParameters(_method);
        }

        public Type[] GetMethodGenericArguments()
        {
            return _forger.GetMethodGenericArguments(_method);
        }

        public Type[] GetTypeGenericArguments()
        {
            return _forger.GetTypeGenericArguments(_method);
        }

        public IMethodInfo GetGenericPattern()
        {
            return _forger.GetGenericPattern(_method);
        }

        public IMethodInfo ImplementGenericPattern(Type[] typeArgs, Type[] methodArgs)
        {
            return _forger.ImplementGenericPattern(_method, typeArgs, methodArgs);
        }
    }
}