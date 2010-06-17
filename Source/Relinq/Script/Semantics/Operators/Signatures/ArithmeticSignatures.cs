using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Syntax.Operators;

// http://code.google.com/p/relinq/wiki/LookupOperators
namespace Relinq.Script.Semantics.Operators.Signatures
{
    [PredefinedOperator(OperatorType.UnaryPlus)]
    public interface IUnaryPlusSignatures
    {
        int S(int x);
        uint S(uint x);
        long S(long x);
        ulong S(ulong x);
        float S(float x);
        double S(double x);
    }

    [PredefinedOperator(OperatorType.UnaryMinus)]
    public interface IUnaryMinusSignatures
    {
        int S(int x);
        long S(long x);
        [Trap] void S(ulong x);
        float S(float x);
        double S(double x);
    }
    
    [PredefinedOperator(OperatorType.Add)]
    [PredefinedOperator(OperatorType.Subtract)]
    [PredefinedOperator(OperatorType.Multiply)]
    [PredefinedOperator(OperatorType.Divide)]
    [PredefinedOperator(OperatorType.Modulo)]
    public interface IBinaryArithmeticSignatures
    {
        int S(int x, int y);
        uint S(uint x, uint y);
        long S(long x, long y);
        ulong S(ulong x, ulong y);
        [Trap] void S(long x, ulong y);
        [Trap] void S(ulong x, long y);
        float S(float x, float y);
        double S(double x, double y);
    }

    [PredefinedOperator(OperatorType.GreaterThan)]
    [PredefinedOperator(OperatorType.GreaterThanOrEqual)]
    [PredefinedOperator(OperatorType.LessThan)]
    [PredefinedOperator(OperatorType.LessThanOrEqual)]
    [PredefinedOperator(OperatorType.Equal)]
    [PredefinedOperator(OperatorType.NotEqual)]
    public interface IRelationalArithmeticSignatures
    {
        bool S(int x, int y);
        bool S(uint x, uint y);
        bool S(long x, long y);
        bool S(ulong x, ulong y);
        [Trap] void S(long x, ulong y);
        [Trap] void S(ulong x, long y);
        bool S(float x, float y);
        bool S(double x, double y);
    }
}
