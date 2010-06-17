using System;
using Relinq.Script.Compilation.MethodRedirection;
using Relinq.Script.Semantics.Lifting;
using Relinq.Script.Compilation.SignatureValidation;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.Operators;
using Relinq.Script.Semantics.Operators.Signatures;
using Relinq.Script.Syntax.Operators;

namespace Relinq.Playground
{
    [PredefinedOperator(OperatorType.Equal)]
    [PredefinedOperator(OperatorType.NotEqual)]
    public interface IReferencePredefinedSignatures
    {
        bool S(int x, int y);
        bool S(uint x, uint y);
        bool S(long x, long y);
        bool S(ulong x, ulong y);
        [Trap] void S(long x, ulong y);
        [Trap] void S(ulong x, long y);
        bool S(float x, float y);
        bool S(double x, double y);

        bool S(bool x, bool y);
        bool S1<[IsEnum] E>(E x, E y);
        bool S(String x, String y);
        bool S2<[IsDelegate] D>(D x, D y);
        bool S(Delegate x, Delegate y);
        bool S3<[IsClass] C>(C x, C y);
        bool S4<[IsNullable] S>(S x, [IsNull] S y);
        bool S5<[IsNullable] S>([IsNull] S x, S y);
    }

    // theoretical
    internal interface IReferenceLiftedPredefinedSignatures
    {
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Int32, Int32)")]
        bool S0(int? x, int? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(UInt32, UInt32)")]
        bool S1(uint? x, uint? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Int64, Int64)")]
        bool S2(long? x, long? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(UInt64, UInt64)")]
        bool S3(ulong? x, ulong? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Single, Single)")]
        bool S4(float? x, float? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Double, Double)")]
        bool S5(double? x, double? y);
        [Lifted, RedirectTo(typeof(ISpecialEqualitySignatures), "Boolean S(Boolean, Boolean)")]
        bool S6(bool? x, bool? y);
        [Lifted, RedirectTo(typeof(ISpecialEqualitySignatures), "Boolean S1[E](E, E)")]
        bool S7<[IsEnum] E>(E? x, E? y) where E : struct;
    }

    // copied from reflector (reflector doesn't support attributes of generic params)
    internal interface ILiftedPredefinedSignatures
    {
        // Methods
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Int32, Int32)")]
        bool S0(int? x, int? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(UInt32, UInt32)")]
        bool S1(uint? x, uint? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Int64, Int64)")]
        bool S2(long? x, long? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(UInt64, UInt64)")]
        bool S3(ulong? x, ulong? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Single, Single)")]
        bool S4(float? x, float? y);
        [Lifted, RedirectTo(typeof(IRelationalArithmeticSignatures), "Boolean S(Double, Double)")]
        bool S5(double? x, double? y);
        [Lifted, RedirectTo(typeof(ISpecialEqualitySignatures), "Boolean S(Boolean, Boolean)")]
        bool S6(bool? x, bool? y);
        [Lifted, RedirectTo(typeof(ISpecialEqualitySignatures), "Boolean S1[E](E, E)")]
        bool S7<E>(E? x, E? y) where E : struct; 
    }
}