using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Script.Syntax.Operators;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Semantics.UserDefinedOperators
{
    public static class UserDefinedOperatorsHelper
    {
        private static List<Info> _ops = new List<Info>();

        private class Info
        {
            public OperatorType Type { get; set; }
            public String MethodName { get; set; }
        }

        static UserDefinedOperatorsHelper()
        {
            _ops.Add(new Info { Type = OperatorType.UnaryPlus, MethodName = "op_UnaryPlus" });
            _ops.Add(new Info { Type = OperatorType.UnaryMinus, MethodName = "op_UnaryMinus" });
            _ops.Add(new Info { Type = OperatorType.LogicalNot, MethodName = "op_LogicalNot" });
            _ops.Add(new Info { Type = OperatorType.OnesComplement, MethodName = "op_OnesComplement" });

            _ops.Add(new Info { Type = OperatorType.OrElse, MethodName = "op_BitwiseOr" });
            _ops.Add(new Info { Type = OperatorType.AndAlso, MethodName = "op_BitwiseAnd" });
            _ops.Add(new Info { Type = OperatorType.Or, MethodName = "op_BitwiseOr" });
            _ops.Add(new Info { Type = OperatorType.ExclusiveOr, MethodName = "op_ExclusiveOr" });
            _ops.Add(new Info { Type = OperatorType.And, MethodName = "op_BitwiseAnd" });
            _ops.Add(new Info { Type = OperatorType.Equal, MethodName = "op_Equality" });
            _ops.Add(new Info { Type = OperatorType.NotEqual, MethodName = "op_Inequality" });
            _ops.Add(new Info { Type = OperatorType.GreaterThan, MethodName = "op_GreaterThan" });
            _ops.Add(new Info { Type = OperatorType.LessThan, MethodName = "op_LessThan" });
            _ops.Add(new Info { Type = OperatorType.GreaterThanOrEqual, MethodName = "op_GreaterThanOrEqual" });
            _ops.Add(new Info { Type = OperatorType.LessThanOrEqual, MethodName = "op_LessThanOrEqual" });
            _ops.Add(new Info { Type = OperatorType.RightShift, MethodName = "op_RightShift" });
            _ops.Add(new Info { Type = OperatorType.LeftShift, MethodName = "op_LeftShift" });
            _ops.Add(new Info { Type = OperatorType.Add, MethodName = "op_Addition" });
            _ops.Add(new Info { Type = OperatorType.Subtract, MethodName = "op_Subtraction" });
            _ops.Add(new Info { Type = OperatorType.Modulo, MethodName = "op_Modulus" });
            _ops.Add(new Info { Type = OperatorType.Multiply, MethodName = "op_Multiply" });
            _ops.Add(new Info { Type = OperatorType.Divide, MethodName = "op_Division" });
        }

        public static bool IsOpMethodImpl(String wannabeOpImpl)
        {
            return _ops.Where(op => op.MethodName == wannabeOpImpl).Any();
        }

        public static bool IsBooleanOpImpl(String wannabeOpImpl)
        {
            return wannabeOpImpl == "op_False" || wannabeOpImpl == "op_True";
        }

        public static IEnumerable<OperatorType> GetOperatorTypeByMethodImpl(String wannabeOpImpl)
        {
            var ops = _ops.Where(op => op.MethodName == wannabeOpImpl).Select(op => op.Type).Cast<OperatorType>();
            if (ops.Count() > 1 && 
                !ops.SequenceEqual(new []{OperatorType.AndAlso, OperatorType.And}) &&
                !ops.SequenceEqual(new []{OperatorType.OrElse, OperatorType.Or}))
            {
                throw new NotSupportedException("Something was overlooked: " +
                    "no way a method name '" + wannabeOpImpl + "' can have more than one " +
                    "corresponding operator type: [" + ops.StringJoin() + "]");
            }

            return ops;
        }
    }
}
