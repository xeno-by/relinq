using System.Linq.Expressions;

namespace Relinq.Linq.Evaluation
{
    public static class Evaluator
    {
        public static object Evaluate(this Expression e)
        {
            return e == null ? null : 
                Expression.Lambda(e, new ParameterExpression[0]).Compile().DynamicInvoke();
        }
    }
}