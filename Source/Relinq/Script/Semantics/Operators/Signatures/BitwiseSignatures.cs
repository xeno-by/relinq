using Relinq.Script.Compilation.SignatureValidation;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Syntax.Operators;

// http://code.google.com/p/relinq/wiki/LookupOperators
namespace Relinq.Script.Semantics.Operators.Signatures
{
    [PredefinedOperator(OperatorType.OnesComplement)]
    public interface IUnaryBitwiseSignatures
    {
        int S(int x);
        uint S(uint x);
        long S(long x);
        ulong S(ulong x);
        E S<[IsEnum] E>(E x);
    }

    [PredefinedOperator(OperatorType.LeftShift)]
    [PredefinedOperator(OperatorType.RightShift)]
    public interface IShiftSignatures
    {
        int S(int x, int y);
        uint S(uint x, int y);
        long S(long x, int y);
        ulong S(ulong x, int y);
    }

    [PredefinedOperator(OperatorType.And)]
    [PredefinedOperator(OperatorType.Or)]
    [PredefinedOperator(OperatorType.ExclusiveOr)]
    public interface IBinaryBitwiseSignatures
    {
        int S(int x, int y);
        uint S(uint x, uint y);
        long S(long x, long y);
        ulong S(ulong x, ulong y);
        [Trap] void S(long x, ulong y);
        [Trap] void S(ulong x, long y);
    }

    [PredefinedOperator(OperatorType.AndAlso)]
    [PredefinedOperator(OperatorType.OrElse)]
    public interface IConditionalBitwiseSignatures
    {
        [Trap] void S(int x, int y);
        [Trap] void S(uint x, uint y);
        [Trap] void S(long x, long y);
        [Trap] void S(ulong x, ulong y);
        [Trap] void S(long x, ulong y);
        [Trap] void S(ulong x, long y);
    }
}