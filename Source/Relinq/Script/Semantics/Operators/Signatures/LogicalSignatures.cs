using Relinq.Script.Syntax.Operators;

// http://code.google.com/p/relinq/wiki/LookupOperators
namespace Relinq.Script.Semantics.Operators.Signatures
{
    [PredefinedOperator(OperatorType.LogicalNot)]
    public interface IUnaryLogicalSignatures
    {
        bool S(bool x);
    }

    [PredefinedOperator(OperatorType.And)]
    [PredefinedOperator(OperatorType.Or)]
    [PredefinedOperator(OperatorType.ExclusiveOr)]
    public interface IBinaryLogicalSignatures
    {
        bool S(bool x, bool y);
    }

    [PredefinedOperator(OperatorType.AndAlso)]
    [PredefinedOperator(OperatorType.OrElse)]
    public interface IConditionalLogicalSignatures
    {
        bool S(bool x, bool y);
    }
}