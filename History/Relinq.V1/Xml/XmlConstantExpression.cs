using System;
using System.Linq.Expressions;
using Relinq.V1.Xml.Metadata;
using Relinq.V1.Xml.Serializers;

namespace Relinq.V1.Xml
{
    public class XmlConstantExpression : XmlExpression
    {
        public XmlMetadataInfo Type { get; set; }
        public String SerializedObject { get; set; }

        public static XmlConstantExpression From(ConstantExpression cex)
        {
            return ConstantExpressionSerializer.Serialize(cex);
        }

        protected override Expression ToExpression()
        {
            return Expression.Constant(
                ConstantExpressionSerializer.Deserialize(this),
                XmlMetadataInfo.ToMetadata<Type>(Type));
        }
    }
}