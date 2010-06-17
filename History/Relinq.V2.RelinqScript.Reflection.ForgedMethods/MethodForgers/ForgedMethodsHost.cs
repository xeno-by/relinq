using System;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods.MethodForgers
{
    public static class ForgedMethodsHost
    {
        public static MethodInfo NaturalCoercionImpl { get { 
            return typeof(ForgedMethodsHost).GetMethod("NaturalCoercion");
        }}

        public static TConvertTo NaturalCoercion<TConvertFrom, TConvertTo>(this TConvertFrom from)
        {
            // todo. stuff that is yet to be implemented:
            // 1) Func<int, int> <-> int delegate(int)
            // 2) Func<int, int> -> Action<int>
            // 3) Expression<F> -> G, if F -> G
            // 4) Expression<F> <- G, if F <- G
            // 5) Coercions for primitive types (according to ECMAScript spec "9. Type conversions", p.30)

            throw new NotImplementedException();
        }

        public static MethodInfo PrimitiveEqualityImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveEquality");
        }}
        public static MethodInfo PrimitiveInequalityImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveInequality");
        }}
        public static MethodInfo PrimitiveAdditionImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveAddition");
        }}
        public static MethodInfo PrimitiveSubtractionImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveSubtraction");
        }}
        public static MethodInfo PrimitiveMultiplicationImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveMultiplication");
        }}
        public static MethodInfo PrimitiveAndImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveAnd");
        }}
        public static MethodInfo PrimitiveOrImpl { get {
            return typeof(ForgedMethodsHost).GetMethod("PrimitiveOr");
        }}

        public static bool PrimitiveEquality<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Int32))
            {
                Func<int, int, bool> eq = (a, b) => a == b;
                return (bool)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static bool PrimitiveInequality<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Int32))
            {
                Func<int, int, bool> eq = (a, b) => a != b;
                return (bool)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static TPrimitive PrimitiveAddition<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Int32))
            {
                Func<int, int, int> eq = (a, b) => a + b;
                return (TPrimitive)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static TPrimitive PrimitiveSubtraction<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Int32))
            {
                Func<int, int, int> eq = (a, b) => a - b;
                return (TPrimitive)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static TPrimitive PrimitiveMultiplication<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Int32))
            {
                Func<int, int, int> eq = (a, b) => a * b;
                return (TPrimitive)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static TPrimitive PrimitiveAnd<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Boolean))
            {
                Func<bool, bool, bool> eq = (a, b) => a && b;
                return (TPrimitive)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }

        public static TPrimitive PrimitiveOr<TPrimitive>(this TPrimitive p1, TPrimitive p2)
            where TPrimitive : struct
        {
            if (typeof(TPrimitive) == typeof(Boolean))
            {
                Func<bool, bool, bool> eq = (a, b) => a || b;
                return (TPrimitive)eq.DynamicInvoke(p1, p2);
            }
            else
            {
                throw new NotImplementedException(typeof(TPrimitive).ToString());
            }
        }
    }
}