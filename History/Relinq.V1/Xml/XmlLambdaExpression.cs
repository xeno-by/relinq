using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Relinq.V1.Xml
{
    public class XmlLambdaExpression : XmlExpression
    {
        public List<XmlParameterExpression> Parameters { get; set; }
        public XmlExpression Body { get; set; }

        public static XmlLambdaExpression FromExpression(LambdaExpression lex)
        {
            return new XmlLambdaExpression
            {
                Body = XmlExpression.FromExpression(lex.Body),
                Parameters = XmlParameterExpression.FromMany(lex.Parameters)
            };
        }

        internal Dictionary<String, ParameterExpression> ParametersCache { get; private set; }

        protected override Expression ToExpression()
        {
            try
            {
                ParametersCache = new Dictionary<String, ParameterExpression>();
                XmlParameterExpression.ToMany(Parameters);

                return Expression.Lambda(
                    XmlExpression.ToExpression(Body),
                    XmlParameterExpression.ToMany(Parameters));
            }
            finally
            {
                ParametersCache = null;
            }
        }
    }
}
