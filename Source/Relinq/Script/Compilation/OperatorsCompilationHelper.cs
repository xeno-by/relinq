using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Script.Syntax.Operators;
using LinqExpression = System.Linq.Expressions.Expression;
using LinqExpressionType = System.Linq.Expressions.ExpressionType;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Compilation
{
    public static class OperatorsCompilationHelper
    {
        private static List<Info> _ops = new List<Info>();

        private class Info
        {
            public OperatorType OpType { get; set; }
            public LinqExpressionType LinqType { get; set; }
        }

        static OperatorsCompilationHelper()
        {
            _ops.Add(new Info { OpType = OperatorType.UnaryPlus, LinqType = LinqExpressionType.UnaryPlus });
            _ops.Add(new Info { OpType = OperatorType.UnaryMinus, LinqType = LinqExpressionType.Negate });
            _ops.Add(new Info { OpType = OperatorType.LogicalNot, LinqType = LinqExpressionType.Not });
            _ops.Add(new Info { OpType = OperatorType.OnesComplement, LinqType = LinqExpressionType.Not });

            _ops.Add(new Info { OpType = OperatorType.OrElse, LinqType = LinqExpressionType.OrElse });
            _ops.Add(new Info { OpType = OperatorType.AndAlso, LinqType = LinqExpressionType.AndAlso });
            _ops.Add(new Info { OpType = OperatorType.Or, LinqType = LinqExpressionType.Or });
            _ops.Add(new Info { OpType = OperatorType.ExclusiveOr, LinqType = LinqExpressionType.ExclusiveOr });
            _ops.Add(new Info { OpType = OperatorType.And, LinqType = LinqExpressionType.And });
            _ops.Add(new Info { OpType = OperatorType.Equal, LinqType = LinqExpressionType.Equal });
            _ops.Add(new Info { OpType = OperatorType.NotEqual, LinqType = LinqExpressionType.NotEqual });
            _ops.Add(new Info { OpType = OperatorType.GreaterThan, LinqType = LinqExpressionType.GreaterThan });
            _ops.Add(new Info { OpType = OperatorType.LessThan, LinqType = LinqExpressionType.LessThan });
            _ops.Add(new Info { OpType = OperatorType.GreaterThanOrEqual, LinqType = LinqExpressionType.GreaterThanOrEqual });
            _ops.Add(new Info { OpType = OperatorType.LessThanOrEqual, LinqType = LinqExpressionType.LessThanOrEqual });
            _ops.Add(new Info { OpType = OperatorType.RightShift, LinqType = LinqExpressionType.RightShift });
            _ops.Add(new Info { OpType = OperatorType.LeftShift, LinqType = LinqExpressionType.LeftShift });
            _ops.Add(new Info { OpType = OperatorType.Add, LinqType = LinqExpressionType.Add });
            _ops.Add(new Info { OpType = OperatorType.Subtract, LinqType = LinqExpressionType.Subtract });
            _ops.Add(new Info { OpType = OperatorType.Modulo, LinqType = LinqExpressionType.Modulo });
            _ops.Add(new Info { OpType = OperatorType.Multiply, LinqType = LinqExpressionType.Multiply });
            _ops.Add(new Info { OpType = OperatorType.Divide, LinqType = LinqExpressionType.Divide });

            _ops.Add(new Info { OpType = OperatorType.Conditional, LinqType = LinqExpressionType.Conditional });
        }

        public static Func<Object[], LinqExpression> LinqExpressionFactory(this OperatorType opType)
        {
            var linqType = _ops.Where(op => op.OpType == opType).Single().LinqType;
            return @params =>
            {
                var userDefined = @params.Last() is MethodInfo;
                var factory = (from mi in typeof(LinqExpression).GetMethods()
                    where mi.Name == linqType.ToString()
                    let lastptype = mi.GetParameters().Last().ParameterType
                    where userDefined ? 
                        lastptype == typeof(MethodInfo) :
                        lastptype == typeof(LinqExpression)
                    select mi).Single();

                // todo. implement lifting here (if necessary)
                var fparams = factory.GetParameters();
                if (fparams.Length > 2 && fparams[2].ParameterType == typeof(bool))
                    @params = new object[]{@params[0], @params[1], false, @params[2]};

                try
                {
                    return (LinqExpression)factory.Invoke(null, @params);
                }
                catch(TargetInvocationException tie)
                {
                    throw tie.InnerException;
                }
            };
        }
    }
}
