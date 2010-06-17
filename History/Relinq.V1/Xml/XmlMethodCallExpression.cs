using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class XmlMethodCallExpression : XmlExpression
    {
        public XmlMetadataInfo Method { get; set; }
        public XmlExpression Target { get; set; }
        public List<XmlExpression> Arguments { get; set; }

        public static XmlMethodCallExpression From(MethodCallExpression mcex)
        {
            return new XmlMethodCallExpression
            {
                Method = XmlMetadataInfo.FromMetadata(mcex.Method),
                Target = XmlExpression.FromExpression(mcex.Object),
                Arguments = XmlExpression.FromMany(mcex.Arguments)
            };
        }

        protected override Expression ToExpression()
        {
            return Expression.Call(
                XmlExpression.ToExpression(Target),
                XmlMetadataInfo.ToMetadata<MethodInfo>(Method),
                XmlExpression.ToMany(Arguments));
        }
    }
}