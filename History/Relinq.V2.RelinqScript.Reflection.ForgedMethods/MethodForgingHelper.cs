using System;
using System.Collections.Generic;
using System.Reflection;
using Relinq.RelinqScript.Grammar.Operators;
using Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers;
using Relinq.RelinqScript.Grammar.Operators;
using Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public static class MethodForgingHelper
    {
        public static IEnumerable<IMethodInfo> ForgePrimitiveUnaryOperators(this Type t, String opMethod)
        {
            // 3) arithmetic operators for primitive types (according to ECMAScript spec "9. Type Conversion")
            // todo: do not yield 214621356213 operators here

            if (t.IsJSPrimitiveOrNullable())
            {
                throw new NotImplementedException(String.Format(
                    "Method '{0}' for primitive type '{1}'", opMethod, t));
            }
            else
            {
                yield break;
            }
        }

        public static IEnumerable<IMethodInfo> ForgePrimitiveBinaryOperators(this Type t, String opMethod)
        {
            // 3) arithmetic operators for primitive types (according to ECMAScript spec "9. Type Conversion")
            // todo: do not yield 214621356213 operators here

            if (t.IsJSPrimitiveOrNullable())
            {
                MethodInfo impl = null;
                switch (opMethod)
                {
                    case "op_Equality":
                        impl = ForgedMethodsHost.PrimitiveEqualityImpl;
                        break;
                    case "op_Inequality":
                        impl = ForgedMethodsHost.PrimitiveInequalityImpl;
                        break;
                    case "op_Addition":
                        impl = ForgedMethodsHost.PrimitiveAdditionImpl;
                        break;
                    case "op_Subtraction":
                        impl = ForgedMethodsHost.PrimitiveSubtractionImpl;
                        break;
                    case "op_Multiply":
                        impl = ForgedMethodsHost.PrimitiveMultiplicationImpl;
                        break;
                    case "op_BitwiseAnd":
                        impl = ForgedMethodsHost.PrimitiveAndImpl;
                        break;
                    case "op_BitwiseOr":
                        impl = ForgedMethodsHost.PrimitiveOrImpl;
                        break;
                }

                if (impl != null)
                {
                    yield return ForgedMethod.Forge(
                        impl.MakeGenericMethod(t),
                        new NonGenericOperatorForger());
                }
                else
                {
                    throw new NotImplementedException(String.Format(
                        "Method '{0}' for primitive type '{1}'", opMethod, t));
                }
            }
            else
            {
                yield break;
            }
        }

        public static IEnumerable<IMethodInfo> ForgeNaturalCoercionMethods(this Type t)
        {
            // todo. stuff that is yet to be implemented:
            // 1) Func<int, int> <-> int delegate(int)
            // 2) Func<int, int> -> Action<int>
            // 3) Expression<F> -> G, if F -> G
            // 4) Expression<F> <- G, if F <- G

            if (t.IsJSPrimitiveOrNullable())
            {
                // todo. stuff that is yet to be implemented:
                // 5) Coercions for primitive types (according to javascript scheme!)
                // according to ECMAScript spec "9. Type Conversion" (p. 30)
                throw new NotImplementedException();
            }
            else
            {
                // todo: this doesn't yield the full graph of coercions
                // e.g. (double)((int)enum): conversion to double will not be found
                // what's more: this should be javascript-compliant (i mean type conversions)

                yield return ForgeCoercionMethod(t, t);

                foreach (var iface in t.GetInterfaces())
                    yield return ForgeCoercionMethod(t, iface);

                for (var baseType = t.BaseType; baseType != null; baseType = baseType.BaseType)
                    yield return ForgeCoercionMethod(t, baseType);
            }
        }

        private static IMethodInfo ForgeCoercionMethod(Type convertFrom, Type convertTo)
        {
            var impl = ForgedMethodsHost.NaturalCoercionImpl;
            impl = impl.MakeGenericMethod(convertFrom, convertTo);
            return ForgedMethod.Forge(impl, new NaturalCoercionMethodForger());
        }

        public static IMethodInfo ForgeFromExtensionMethod(this MethodInfo extension)
        {
            if (extension.IsExtension())
            {
                return ForgedMethod.Forge(extension, new ExtensionToInstanceMethodForger());
            }
            else
            {
                throw new ArgumentException(String.Format(
                    "'{0}' is not an extension method", extension));
            }
        }

        public static IMethodInfo ForgeFromUserDefinedOperator(this MethodInfo op)
        {
            // unary operators or wtf
            if (OperatorHelper.IsBinaryOpMethodImpl(op.Name))
            {
                if (op.IsGenericMethod || op.DeclaringType.IsGenericType)
                {
                    throw new NotSupportedException(op.ToString());
                }
                else
                {
                    return ForgedMethod.Forge(op, new NonGenericOperatorForger());
                }
            }
            else
            {
                throw new ArgumentException(String.Format(
                    "'{0}' is not a supported user-defined operator", op));
            }
        }
    }
}