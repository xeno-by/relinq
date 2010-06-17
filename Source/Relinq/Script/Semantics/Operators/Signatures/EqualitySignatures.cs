using System;
using Relinq.Script.Compilation.SignatureValidation;
using Relinq.Script.Syntax.Operators;

// http://code.google.com/p/relinq/wiki/LookupOperators
namespace Relinq.Script.Semantics.Operators.Signatures
{
    [PredefinedOperator(OperatorType.Equal)]
    [PredefinedOperator(OperatorType.NotEqual)]
    public interface ISpecialEqualitySignatures
    {
        bool S(bool x, bool y);

        // disabled to avoid ambiguity between this sig and bool S(IntType x, IntType y);
        // when resolving comparisons like smth.EnumField == Enum.Value (since the latter will be xmitted as IntType literal)
        // will make the compiler produce IntType comparison rather than Enum comparison, tho it doesn't matter
        // todo. update the spec about roundtrip issues
//        bool S1<[IsEnum] E>(E x, E y);

        // todo. no idea why the lines below are disabled
//        bool S(String x, String y);
//        bool S2<[IsDelegate] D>(D x, D y);
//        bool S(Delegate x, Delegate y);

        bool S3<[IsClass] C>(C x, C y);
        bool S4<[IsNullable] S>(S x, [IsNull] S y);
        bool S5<[IsNullable] S>([IsNull] S x, S y);
    }
}
