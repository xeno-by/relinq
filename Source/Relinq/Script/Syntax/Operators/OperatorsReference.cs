using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Syntax.Operators
{
    public static class OperatorsReference
    {
        private static List<Operator> _ops = new List<Operator>();

        private abstract class Operator
        {
            public String Code { get; set; }
            public abstract int Arity { get; }
            public OperatorType Type { get; set; }
            public int Priority { get; set; }
        }

        private class Unary : Operator
        {
            public override int Arity { get { return 1; } }
        }

        private class Binary : Operator
        {
            public override int Arity { get { return 2; } }
        }

        private class Ternary : Operator
        {
            public override int Arity { get { return 3; } }
        }

        static OperatorsReference()
        {
            _ops.Add(new Unary { Code = "+", Type = OperatorType.UnaryPlus, Priority = 11});
            _ops.Add(new Unary { Code = "-", Type = OperatorType.UnaryMinus, Priority = 11});
            _ops.Add(new Unary { Code = "!", Type = OperatorType.LogicalNot, Priority = 11});
            _ops.Add(new Unary { Code = "~", Type = OperatorType.OnesComplement, Priority = 11});

            _ops.Add(new Binary { Code = "||", Type = OperatorType.OrElse, Priority = 1});
            _ops.Add(new Binary { Code = "&&", Type = OperatorType.AndAlso, Priority = 2});
            _ops.Add(new Binary { Code = "|", Type = OperatorType.Or, Priority = 3});
            _ops.Add(new Binary { Code = "^", Type = OperatorType.ExclusiveOr, Priority = 4});
            _ops.Add(new Binary { Code = "&", Type = OperatorType.And, Priority = 5});
            _ops.Add(new Binary { Code = "==", Type = OperatorType.Equal, Priority = 6});
            _ops.Add(new Binary { Code = "!=", Type = OperatorType.NotEqual, Priority = 6});
            _ops.Add(new Binary { Code = ">", Type = OperatorType.GreaterThan, Priority = 7});
            _ops.Add(new Binary { Code = "<", Type = OperatorType.LessThan, Priority = 7});
            _ops.Add(new Binary { Code = ">=", Type = OperatorType.GreaterThanOrEqual, Priority = 7});
            _ops.Add(new Binary { Code = "<=", Type = OperatorType.LessThanOrEqual, Priority = 7});
            _ops.Add(new Binary { Code = ">>", Type = OperatorType.RightShift, Priority = 8});
            _ops.Add(new Binary { Code = "<<", Type = OperatorType.LeftShift, Priority = 8});
            _ops.Add(new Binary { Code = "+", Type = OperatorType.Add, Priority = 9});
            _ops.Add(new Binary { Code = "-", Type = OperatorType.Subtract, Priority = 9});
            _ops.Add(new Binary { Code = "%", Type = OperatorType.Modulo, Priority = 10});
            _ops.Add(new Binary { Code = "*", Type = OperatorType.Multiply, Priority = 10});
            _ops.Add(new Binary { Code = "/", Type = OperatorType.Divide, Priority = 10});

            _ops.Add(new Ternary { Code = "?", Type = OperatorType.Conditional, Priority = 0});
        }

        public static bool IsOperator(String wannabeOp)
        {
            return _ops.Where(op => op.Code == wannabeOp).Any();
        }

        public static bool IsUnaryOp(String wannabeOp)
        {
            return _ops.OfType<Unary>().Where(op => op.Code == wannabeOp).Any();
        }

        public static bool IsBinaryOp(String wannabeOp)
        {
            return _ops.OfType<Binary>().Where(op => op.Code == wannabeOp).Any();
        }

        public static bool IsRelational(this OperatorType opType)
        {
            return opType == OperatorType.Equal || opType == OperatorType.NotEqual ||
                opType == OperatorType.GreaterThan || opType == OperatorType.GreaterThanOrEqual ||
                opType == OperatorType.LessThan || opType == OperatorType.LessThanOrEqual;
        }

        public static OperatorType GetUnaryOperatorType(String wannabeOp)
        {
            return _ops.OfType<Unary>().Where(op => op.Code == wannabeOp).Single().Type;
        }

        public static OperatorType GetBinaryOperatorType(String wannabeOp)
        {
            return _ops.OfType<Binary>().Where(op => op.Code == wannabeOp).Single().Type;
        }

        public static string GetOperatorCode(this RelinqScriptExpression exp)
        {
            var opex = exp as OperatorExpression;
            return opex == null ? null : opex.Type.GetOpCode();
        }

        public static int GetArity(this OperatorType opType)
        {
            return _ops.Where(op => op.Type == opType).Single().Arity;
        }

        public static int GetPriority(this OperatorType opType)
        {
            return _ops.Where(op => op.Type == opType).Single().Priority;
        }

        public static string GetOpCode(this OperatorType opType)
        {
            return _ops.Where(op => op.Type == opType).Single().Code;
        }
    }
}