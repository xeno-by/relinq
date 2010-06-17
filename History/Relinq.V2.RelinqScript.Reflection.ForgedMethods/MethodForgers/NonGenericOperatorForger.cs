using System;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    public class NonGenericOperatorForger : StaticToInstanceMethodForgerBase
    {
        public override Type[] GetMethodGenericArguments(MethodInfo method)
        {
            return new Type[0];
        }

        public override Type[] GetTypeGenericArguments(MethodInfo method)
        {
            return new Type[0];
        }

        public override IMethodInfo GetGenericPattern(MethodInfo method)
        {
            return ForgedMethod.Forge(method, this);
        }

        public override IMethodInfo ImplementGenericPattern(MethodInfo method, Type[] typeArgs, Type[] methodArgs)
        {
            return ForgedMethod.Forge(method, this);
        }
    }
}