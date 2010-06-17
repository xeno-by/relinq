using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class XmlParameterExpression : XmlExpression
    {
        public XmlMetadataInfo Type { get; set; }
        public String Name { get; set; }

        public static XmlParameterExpression From(ParameterExpression pex)
        {
            return new XmlParameterExpression
            {
                Type = XmlMetadataInfo.FromMetadata(pex.Type),
                Name = pex.Name
            };
        }

        public static List<XmlParameterExpression> FromMany(IEnumerable<ParameterExpression> exs)
        {
            return new List<XmlParameterExpression>(FromManyImpl(exs));
        }

        private static IEnumerable<XmlParameterExpression> FromManyImpl(IEnumerable<ParameterExpression> exs)
        {
            foreach (var ex in exs)
            {
                yield return From(ex);
            }
        }

        protected override Expression ToExpression()
        {
            var current = Parent;
            while(current != null && !(current is XmlLambdaExpression))
                current = current.Parent;

            if (current == null)
                throw new NotSupportedException("Dangling parameter expressions are not supported");

            var lambda = (XmlLambdaExpression)current;
            if (!lambda.ParametersCache.ContainsKey(Name))
            {
                lambda.ParametersCache.Add(
                    Name, 
                    Expression.Parameter(XmlMetadataInfo.ToMetadata<Type>(Type), Name));
            }

            return lambda.ParametersCache[Name];
        }

        public static ParameterExpression[] ToMany(IEnumerable<XmlParameterExpression> exs)
        {
            return new List<ParameterExpression>(ToManyImpl(exs)).ToArray();
        }

        public static IEnumerable<ParameterExpression> ToManyImpl(IEnumerable<XmlParameterExpression> exs)
        {
            foreach (var ex in exs)
            {
                yield return (ParameterExpression)XmlExpression.ToExpression(ex);
            }
        }
    }
}