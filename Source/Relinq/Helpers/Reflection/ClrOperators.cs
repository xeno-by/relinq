using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Relinq.Helpers.Reflection
{
    public static class ClrOperators
    {
        public static int UnaryOpsPriority { get { return _ops.Max(op => op.Priority); } }
        public static int PrimaryOpsPriority { get { return _ops.Max(op => op.Priority) + 1; } }

        private static List<Operator> _ops = new List<Operator>();

        private abstract class Operator
        {
            public String Code { get; set; }
            public abstract int Arity { get; }
            public ExpressionType LinqType { get; set; }
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

        static ClrOperators()
        {
            _ops.Add(new Unary { Code = "+", LinqType = ExpressionType.UnaryPlus, Priority = 11});
            _ops.Add(new Unary { Code = "-", LinqType = ExpressionType.Negate, Priority = 11});
            _ops.Add(new Unary { Code = "!", LinqType = ExpressionType.Not, Priority = 11});
            _ops.Add(new Unary { Code = "~", LinqType = ExpressionType.Not, Priority = 11});

            _ops.Add(new Binary { Code = "||", LinqType = ExpressionType.OrElse, Priority = 1});
            _ops.Add(new Binary { Code = "&&", LinqType = ExpressionType.AndAlso, Priority = 2});
            _ops.Add(new Binary { Code = "|", LinqType = ExpressionType.Or, Priority = 3});
            _ops.Add(new Binary { Code = "^", LinqType = ExpressionType.ExclusiveOr, Priority = 4});
            _ops.Add(new Binary { Code = "&", LinqType = ExpressionType.And, Priority = 5});
            _ops.Add(new Binary { Code = "==", LinqType = ExpressionType.Equal, Priority = 6});
            _ops.Add(new Binary { Code = "!=", LinqType = ExpressionType.NotEqual, Priority = 6});
            _ops.Add(new Binary { Code = ">", LinqType = ExpressionType.GreaterThan, Priority = 7});
            _ops.Add(new Binary { Code = "<", LinqType = ExpressionType.LessThan, Priority = 7});
            _ops.Add(new Binary { Code = ">=", LinqType = ExpressionType.GreaterThanOrEqual, Priority = 7});
            _ops.Add(new Binary { Code = "<=", LinqType = ExpressionType.LessThanOrEqual, Priority = 7});
            _ops.Add(new Binary { Code = ">>", LinqType = ExpressionType.RightShift, Priority = 8});
            _ops.Add(new Binary { Code = "<<", LinqType = ExpressionType.LeftShift, Priority = 8});
            _ops.Add(new Binary { Code = "+", LinqType = ExpressionType.Add, Priority = 9});
            _ops.Add(new Binary { Code = "-", LinqType = ExpressionType.Subtract, Priority = 9});
            _ops.Add(new Binary { Code = "%", LinqType = ExpressionType.Modulo, Priority = 10});
            _ops.Add(new Binary { Code = "*", LinqType = ExpressionType.Multiply, Priority = 10});
            _ops.Add(new Binary { Code = "/", LinqType = ExpressionType.Divide, Priority = 10});

            _ops.Add(new Ternary { Code = "?", LinqType = ExpressionType.Conditional, Priority = 0});
        }

        public static bool IsRelational(this ExpressionType expType)
        {
            return expType == ExpressionType.Equal || expType == ExpressionType.NotEqual ||
                expType == ExpressionType.GreaterThan || expType == ExpressionType.GreaterThanOrEqual ||
                expType == ExpressionType.LessThan || expType == ExpressionType.LessThanOrEqual;
        }
        
        public static string GetOpCode(this Expression exp)
        {
            return _ops.Where(op => op.LinqType == exp.NodeType).First().Code;
        }

        public static bool HasPriority(this Expression e)
        {
            return e == null ? false : e.NodeType.GetPriority() != null;
        }

        public static int GetPriority(this Expression e)
        {
            var prio = e == null ? null : e.NodeType.GetPriority();
            if (prio.HasValue)
            {
                return prio.Value;
            }
            else
            {
                throw new NotSupportedException(e.ToString());
            }
        }

        public static int? GetPriority(this ExpressionType expType)
        {
            if (expType == ExpressionType.MemberAccess || expType == ExpressionType.Call ||
                expType == ExpressionType.ArrayIndex || expType == ExpressionType.ArrayLength ||
                expType == ExpressionType.Invoke)
            {
                return PrimaryOpsPriority;
            }
            else if (expType == ExpressionType.Convert)
            {
                return UnaryOpsPriority;
            }
            else if (expType == ExpressionType.Power)
            {
                return ExpressionType.ExclusiveOr.GetPriority();
            }
            else
            {
                var opInfo = _ops.Where(op => op.LinqType == expType).FirstOrDefault();
                return opInfo == null ? null : (int?)opInfo.Priority;
            }
        }
    }
}