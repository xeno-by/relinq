using System;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class VariableExpression : TypeInferenceUnit
    {
        public String Name { get; private set; }

        public VariableExpression(String name)
            : base(ExpressionType.Variable, Enumerable.Empty<TypeInferenceUnit>())
        {
            Name = name;
        }

        protected override string ContentToString()
        {
            return "v:" + Name;
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();

            LambdaExpression lambdaUnit = null;
            for (var current = Parent; current != null; current = current.Parent)
            {
                lambdaUnit = current as LambdaExpression;
                if (lambdaUnit != null) break;
            }

            if (lambdaUnit == null)
            {
                throw new ArgumentException("Cannot find declaration of the variable");
            }
            else
            {
                Type.IsIdenticalTo(lambdaUnit.Lambda.Args[Name]);
            }
        }
    }
}