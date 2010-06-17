using System;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class ConditionalExpression : TypeInferenceUnit
    {
        public TypeInferenceUnit Condition { get { return Children.ElementAt(0); } }
        public TypeInferenceUnit IfTrue { get { return Children.ElementAt(1); } }
        public TypeInferenceUnit IfFalse { get { return Children.ElementAt(2); } }

        public ConditionalExpression(TypeInferenceUnit cond, TypeInferenceUnit ifTrue, TypeInferenceUnit ifFalse) 
            : base(ExpressionType.Ternary, new []{cond, ifTrue, ifFalse})
        {
        }

        protected override string ContentToString()
        {
            return String.Format("ternary");
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();

            var boolType = FuzzyType.FromRuntimeType(typeof(bool));
            Condition.Type.IsIdenticalTo(boolType);

            // todo: insert conversion nodes

            Type.IsIdenticalTo(IfTrue.Type);
            Type.IsIdenticalTo(IfFalse.Type);
        }
    }
}