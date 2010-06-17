using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;
using Relinq.V2.RelinqScript.Grammar.Keywords;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public class LambdaExpression : TypeInferenceUnit
    {
        public IEnumerable<String> Args { get; private set; }
        public TypeInferenceUnit Body { get; private set; }

        public FuzzyLambda Lambda { get; set; }

        public LambdaExpression(IEnumerable<String> args, TypeInferenceUnit body)
            : base(ExpressionType.Lambda, Enumerable.Repeat(body, 1))
        {
            Args = args;
            Body = Children.ElementAt(0);
        }

        public override Dictionary<string, LambdaExpression> Closure
        {
            get
            {
                var closure = new Dictionary<string, LambdaExpression>(base.Closure);

                foreach (var argName in Args)
                {
                    if (RelinqScriptKeywords.IsKeyword(argName))
                    {
                        throw new ArgumentException(String.Format(
                            "Argument '{0}' overrides keyword with the same name", argName));
                    }
                    else if (closure.ContainsKey(argName))
                    {
                        throw new ArgumentException(String.Format(
                            "Argument '{0}' is already defined within current context", argName));
                    }
                    else
                    {
                        closure.Add(argName, this);
                    }
                }

                return closure;
            }
        }

        protected override string ContentToString()
        {
            return "lambda";
        }

        protected override FuzzyType OnInitializeMainTypePoint()
        {
            Lambda = FuzzyLambda.Unknown(Args);
            return base.OnInitializeMainTypePoint();
        }

        protected override void OnInitializeTypeLinks()
        {
            base.OnInitializeTypeLinks();

            Type.IsIdenticalTo(Lambda);
            Body.Type.IsRetValOf(Lambda);
            // Typelinks between arg types and all variables will be established by VariableExpressions
        }
    }
}