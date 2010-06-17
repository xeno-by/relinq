using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Relinq.Helpers.Reflection
{
    public static class FunctionTypesHelper
    {
        public static Type ForgeTypedLambda(this Type functionType)
        {
            return typeof(Expression<>).XMakeGenericType(functionType);
        }

        private static Type ForgeFuncType(int argCount)
        {
            switch (argCount)
            {
                case 0:
                    return typeof(Func<>);
                case 1:
                    return typeof(Func<,>);
                case 2:
                    return typeof(Func<,,>);
                case 3:
                    return typeof(Func<,,,>);
                case 4:
                    return typeof(Func<,,,,>);
                default:
                    throw new NotSupportedException(String.Format(
                        "Funcs with '{0}' arg(s) are not supported", argCount));
            }
        }

        public static Type ForgeFuncType(this IEnumerable<Type> args)
        {
            var genericDef = ForgeFuncType(args.Count() - 1);
            return genericDef.XMakeGenericType(args.ToArray());
        }

        private static Type ForgeActionType(int argCount)
        {
            switch (argCount)
            {
                case 0:
                    return typeof(Action);
                case 1:
                    return typeof(Action<>);
                case 2:
                    return typeof(Action<,>);
                case 3:
                    return typeof(Action<,,>);
                case 4:
                    return typeof(Action<,,,>);
                default:
                    throw new NotSupportedException(String.Format(
                        "Actions with '{0}' arg(s) are not supported", argCount));
            }
        }

        public static Type ForgeActionType(this IEnumerable<Type> args)
        {
            var genericDef = ForgeActionType(args.Count());
            return genericDef.XMakeGenericType(args.ToArray());
        }

        public static Type NormalizeFunctionType(this Type t)
        {
            var desc = t.GetFunctionDesc();
            if (desc == null)
            {
                return null;
            }
            else
            {
                if (desc.ReturnValue == typeof(void))
                {
                    return ForgeActionType(desc.Args.Count()).XMakeGenericType(desc.Args.ToArray());
                }
                else
                {
                    var funcArgs = new List<Type>(desc.Args){desc.ReturnValue};
                    return ForgeFuncType(desc.Args.Count()).XMakeGenericType(funcArgs.ToArray());
                }
            }
        }

        public static IFunction GetFunctionDesc(this Type t)
        {
            if (!t.IsFunctionType())
            {
                return null;
            }
            else
            {
                return t.IsDelegate() ? new DelegateFunction(t) : new LambdaFunction(t);
            }
        }

        public interface IFunction
        {
            IEnumerable<Type> Args { get; }
            Type ReturnValue { get; }
        }

        private class DelegateFunction : IFunction
        {
            public IEnumerable<Type> Args { get; private set; }
            public Type ReturnValue { get; private set; }

            public DelegateFunction(Type @delegate)
            {
                var sig = @delegate.GetFunctionSignature();
                Args = sig.GetParameters().Select(pi => pi.ParameterType);
                ReturnValue = sig.ReturnType;
            }
        }

        private class LambdaFunction : DelegateFunction
        {
            public LambdaFunction(Type lambda)
                : base(lambda.GetGenericArguments()[0])
            {
            }
        }
    }
}