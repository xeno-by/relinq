using System;
using Relinq.Script.Compilation.MethodRedirection;
using Relinq.Script.Compilation.SignatureValidation;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Syntax.Operators;

// http://code.google.com/p/relinq/wiki/LookupOperators
namespace Relinq.Script.Semantics.Operators.Signatures
{
    [PredefinedOperator(OperatorType.Add)]
    public interface ISpecialAddSignatures
    {
        E S<[IsEnum("U")] E, U>(E x, U y);
        E S<[IsEnum("U")] E, U>(U x, E y);
        [RedirectTo(typeof(String), "System.String Concat(System.Object, System.Object)")]
        String S(String x, String y);
        [RedirectTo(typeof(String), "System.String Concat(System.Object, System.Object)")]
        String S(String x, Object y);
        [RedirectTo(typeof(String), "System.String Concat(System.Object, System.Object)")]
        String S(Object x, String y);
        D S<[IsDelegate] D>(D x, D y);
    }

    [PredefinedOperator(OperatorType.Subtract)]
    public interface ISpecialSubtractSignatures
    {
        U S<[IsEnum("U")] E, U>(E x, E y);
        E S<[IsEnum("U")] E, U>(E x, U y);
        D S<[IsDelegate] D>(D x, D y);
    }

    [PredefinedOperator(OperatorType.And)]
    [PredefinedOperator(OperatorType.Or)]
    [PredefinedOperator(OperatorType.ExclusiveOr)]
    public interface ISpecialBitwiseSignatures
    {
        E S<[IsEnum] E>(E x, E y);
    }

    [PredefinedOperator(OperatorType.AndAlso)]
    [PredefinedOperator(OperatorType.OrElse)]
    public interface ISpecialConditionalSignatures
    {
        [Trap] E S<[IsEnum] E>(E x, E y);
    }

    [PredefinedOperator(OperatorType.GreaterThan)]
    [PredefinedOperator(OperatorType.GreaterThanOrEqual)]
    [PredefinedOperator(OperatorType.LessThan)]
    [PredefinedOperator(OperatorType.LessThanOrEqual)]
    public interface ISpecialRelationalSignatures
    {
        bool S<[IsEnum] E>(E x, E y);
    }
}
