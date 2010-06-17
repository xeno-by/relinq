using System;
using System.Collections.Generic;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;
using Relinq.V2.RelinqScript.Reflection;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class JsonExpression : TypeInferenceUnit
    {
        public IDictionary<String, TypeInferenceUnit> Props { get; private set; }

        public JsonExpression(IDictionary<String, TypeInferenceUnit> props)
            : base(ExpressionType.Json, props.Values)
        {
            // bug: jeez.. wtf alert
//            Props = props;

            Props = new Dictionary<String, TypeInferenceUnit>();
            for (var i = 0; i < props.Count; i++)
            {
                Props.Add("Field" + i, props.ElementAt(i).Value);
            }
        }

        protected override string ContentToString()
        {
            return "json";
        }

        protected override FuzzyType OnInitializeMainTypePoint()
        {
            // bug: jeez.. wtf alert
            var anonymousType = ReflectionHelper.ForgeTupleType(Props.Count);
            return FuzzyType.FromRuntimeType(anonymousType);
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();

            foreach (var propName in Props.Keys)
                Props[propName].Type.IsFieldOf(Type, propName);
        }
    }
}