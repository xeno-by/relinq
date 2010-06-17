using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Relinq.RelinqScript.Reflection;
using Relinq.RelinqScript.Reflection.StructuralTrees;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    // extension host is a static and non-generic type.
    // this actually helps a lot

    public class ExtensionToInstanceMethodForger : StaticToInstanceMethodForgerBase
    {
        public override Type[] GetMethodGenericArguments(MethodInfo method)
        {
            var promotedArgs = GetTypeGenericArguments(method);
            var methodArgs = method.IsGenericMethod ? method.GetGenericArguments() : Enumerable.Empty<Type>();
            return methodArgs.Except(promotedArgs).ToArray();
        }

        public override Type[] GetTypeGenericArguments(MethodInfo method)
        {
            var mapping = method.GetGenericArgsMapping();
            var ownerArgs = mapping.Where(kvp => kvp.Key.StartsWith("a0")).Select(kvp => kvp.Value).Distinct();
            return ownerArgs.Select(arg => method.GetGenericArguments()[int.Parse(arg.Substring(2, 1))]).ToArray();
        }

        public override IMethodInfo GetGenericPattern(MethodInfo method)
        {
            var pattern = method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
            return ForgedMethod.Forge(pattern, this);
        }

        public override IMethodInfo ImplementGenericPattern(MethodInfo method, Type[] typeArgs, Type[] methodArgs)
        {
            var jointArgs = new List<Type>();

            var patternArgs = method.IsGenericMethod ? method.GetGenericMethodDefinition().GetGenericArguments() : new Type[0];
            var patternMethodArgs = GetGenericPattern(method).GetMethodGenericArguments();
            var patternTypeArgs = GetGenericPattern(method).GetTypeGenericArguments();

            for (var i = 0; i < patternArgs.Length; ++i)
            {
                var patternArg = patternArgs[i];

                for (var j = 0; j < patternMethodArgs.Length; ++j)
                {
                    var patternMethodArg = patternMethodArgs[j];
                    if (patternMethodArg.SameMetadataToken(patternArg))
                    {
                        jointArgs.Add(methodArgs[j]);
                        break;
                    }
                }

                for (var j = 0; j < patternTypeArgs.Length; ++j)
                {
                    var patternTypeArg = patternTypeArgs[j];
                    if (patternTypeArg.SameMetadataToken(patternArg))
                    {
                        jointArgs.Add(typeArgs[j]);
                        break;
                    }
                }
            }

            var pattern = method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
            return ForgedMethod.Forge(pattern.MakeGenericMethod(jointArgs.ToArray()), this);
        }
    }
}