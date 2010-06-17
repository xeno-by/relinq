using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class XmlNewExpression : XmlMethodCallExpression
    {
        public List<XmlMetadataInfo> Members { get; set; }

        public static XmlNewExpression From(NewExpression nex)
        {
            return new XmlNewExpression
            {
                Method = XmlMetadataInfo.FromMetadata(nex.Constructor),
                Arguments = XmlExpression.FromMany(nex.Arguments),
                Members = XmlMetadataInfo.FromMany(nex.Members)
            };
        }

        protected override Expression ToExpression()
        {
            return Expression.New(
                XmlMetadataInfo.ToMetadata<ConstructorInfo>(Method),
                XmlExpression.ToMany(Arguments),
                XmlMetadataInfo.ToMany<MemberInfo>(Members));
        }
    }
}