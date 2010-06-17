using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Relinq.Helpers.Collections;
using Relinq.Linq.Evaluation;
using Relinq.Helpers.Reflection;

namespace Relinq.Linq.Preprocessing
{
    // http://code.google.com/p/relinq/wiki/Funcletizer
    public class Funcletizer : LinqVisitor
    {
        private readonly Dictionary<Expression, bool> _canBeSimplified;

        public Funcletizer(Expression root)
            : base(root)
        {
            var marker = new SimplifiableExpressionsMarker(root);
            _canBeSimplified = marker.VisitThenAcq(m => m._simplificationMap);
        }

        protected override Expression VisitExpression(Expression exp)
        {
            if (exp != null && 
                _canBeSimplified[exp] && 
                !(exp.NodeType == ExpressionType.Lambda) && 
                !(exp.NodeType == ExpressionType.Quote))
            {
                var eval = exp.Evaluate();
                return Expression.Constant(eval, exp.Type);
            }

            return base.VisitExpression(exp);
        }

        private class SimplifiableExpressionsMarker : LinqVisitor
        {
            private IEnumerable<ParameterExpression> _unresolvedVars = Enumerable.Empty<ParameterExpression>();
            private bool _canBeExecutedLocally = true;
            public Dictionary<Expression, bool> _simplificationMap = new Dictionary<Expression, bool>();

            public SimplifiableExpressionsMarker(Expression root) 
                : base(root)
            {
            }

            protected override Expression VisitExpression(Expression exp)
            {
                // Store the opinion of previously traversed node (which is most likely our left sibling or
                // alternatively the right-most child of one of our parents' left-sibling)
                var prevCanBeExecutedLocally = _canBeExecutedLocally;
                var prevUnresolvedVars = _unresolvedVars;

                // Find out the opinion of the node itself (which will typically ask its children via base.Visit
                // and will add spice it up with its own logics)
                _canBeExecutedLocally = true;
                _unresolvedVars = new List<ParameterExpression>();
                exp = base.VisitExpression(exp);

                if (exp != null)
                {
                    if (!_simplificationMap.ContainsKey(exp))
                    {
                        _simplificationMap.Add(exp, _canBeExecutedLocally && _unresolvedVars.Count() == 0);
                    }
                }

                // Restore the opinion of previously traversed node (this is important so that parent will get a
                // joint opinion of all its children, not just the very right-most one's)
                _canBeExecutedLocally &= prevCanBeExecutedLocally;
                _unresolvedVars = _unresolvedVars.Concat(prevUnresolvedVars);

                return exp;
            }

            protected override Expression VisitLambda(LambdaExpression lambda)
            {
                var exp = base.VisitLambda(lambda);

                var resolvedVars = new List<ParameterExpression>();
                foreach (var unresolved in _unresolvedVars)
                {
                    if (lambda.Parameters.Contains(unresolved))
                    {
                        resolvedVars.Add(unresolved);
                    }
                }

                _unresolvedVars = _unresolvedVars.Except(resolvedVars);

                return exp;
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                var exp = base.VisitParameter(p);
                _unresolvedVars = _unresolvedVars.Concat(p.AsArray());
                return exp;
            }

            protected override Expression VisitConstant(ConstantExpression c)
            {
                var exp = base.VisitConstant(c);
                _canBeExecutedLocally = !c.Type.IsRemote();
                return exp;
            }
        }
    }
}