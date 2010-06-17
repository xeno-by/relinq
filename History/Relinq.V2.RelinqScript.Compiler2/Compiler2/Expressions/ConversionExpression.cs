using System.Linq;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class ConversionExpression : CallExpression
    {
        public ConversionExpression(TypeInferenceUnit target)
            : base("<>op_Convert", target, Enumerable.Empty<TypeInferenceUnit>())
        {
        }
    }
}