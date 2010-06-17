using System;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class FieldExpression : TypeInferenceUnit
    {
        public String Name { get; private set; }
        public TypeInferenceUnit Target { get; private set; }

        public FieldExpression(String name, TypeInferenceUnit target)
            : base(ExpressionType.Field, Enumerable.Repeat(target, 1))
        {
            Name = name;
            Target = Children.ElementAt(0);
        }

        protected override string ContentToString()
        {
            return "f:" + Name;
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();
            Type.IsFieldOf(Target.Type, Name);
        }
    }
}