using System;
using System.Linq;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class LiteralExpression : TypeInferenceUnit
    {
        public String Data { get; private set; }

        public LiteralExpression(String data)
            : base(ExpressionType.Literal, Enumerable.Empty<TypeInferenceUnit>())
        {
            Data = data;
        }

        protected override string ContentToString()
        {
            return "l:" + Data;
        }

        protected override void OnInitializeTypeLinks()
        {
            Type.SomeMoreStuffInferred += OnExtraStuffInferred;
        }

        void OnExtraStuffInferred(object sender, EventArgs e)
        {
            // todo. possible refinement of type inference engine
            //
            // In future I might add sophisticated validations here, e.g. 
            //  * If basis type of Type is enumerable, but Data is not decorated with brackets -> FAIL
            //  * If type aint a primitve or a nullable of primitve, but Data is in verbatim format -> FAIL
            //
            // Another possible enhancement of type inference would be inferring type constraints based on 
            // peculiarities of Data, e.g. aforementioned examples can be easily reformulated to become constraints.
        }
    }
}