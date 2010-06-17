using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class CallExpression : TypeInferenceUnit
    {
        public String Name { get; private set; }
        public TypeInferenceUnit Target { get; private set; }
        public IEnumerable<TypeInferenceUnit> Args { get; private set; }

        public FuzzyMethodBinding Method { get; set; }

        public CallExpression(String name, TypeInferenceUnit target, IEnumerable<TypeInferenceUnit> args)
            : base(ExpressionType.Call, Enumerable.Repeat(target, 1).Union(args))
        {
            Name = name;
            Target = Children.ElementAt(0);
            Args = Children.Skip(1);
        }

        protected override string ContentToString()
        {
            return "m:" + Name;
        }

        protected override FuzzyType OnInitializeMainTypePoint()
        {
            Method = FuzzyMethodBinding.Unknown(Name, Args.Count());
            return base.OnInitializeMainTypePoint();
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();

            Target.Type.IsTargetOf(Method);

            var argIndex = 0;
            foreach (var arg in Args)
                arg.Type.IsArgOf(Method, argIndex++);

            Type.IsRetValOf(Method);
        }
    }
}