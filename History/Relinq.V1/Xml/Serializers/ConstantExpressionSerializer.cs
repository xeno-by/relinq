using System;
using System.Globalization;
using System.Linq.Expressions;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml.Serializers
{
    public static class ConstantExpressionSerializer
    {
        public static XmlConstantExpression Serialize(ConstantExpression cex)
        {
            return new XmlConstantExpression
            {
                SerializedObject = (String)Convert.ChangeType(cex.Value, typeof(String), CultureInfo.InvariantCulture),
                Type = XmlMetadataInfo.FromMetadata(cex.Type)
            };
        }

        public static Object Deserialize(XmlConstantExpression xcex)
        {
            var t = XmlMetadataInfo.ToMetadata<Type>(xcex.Type);
            return Convert.ChangeType(xcex.SerializedObject, t);
        }
    }
}
