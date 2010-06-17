using System;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class XmlMemberExpression : XmlExpression
    {
        public XmlMetadataInfo Member { get; set; }
        public XmlExpression Expression { get; set; }

        public static XmlMemberExpression From(MemberExpression mex)
        {
            return new XmlMemberExpression
            {
                Member = XmlMetadataInfo.FromMetadata(mex.Member),
                Expression = XmlExpression.FromExpression(mex.Expression)
            };
        }

        protected override Expression ToExpression()
        {
            switch (Member.MemberType)
            {
                case MemberTypes.Field:
                    return System.Linq.Expressions.Expression.Field(
                        XmlExpression.ToExpression(Expression),
                        XmlMetadataInfo.ToMetadata<FieldInfo>(Member));

                case MemberTypes.Property:
                    return System.Linq.Expressions.Expression.Property(
                        XmlExpression.ToExpression(Expression),
                        XmlMetadataInfo.ToMetadata<MethodInfo>(Member));

                default:
                    throw new NotSupportedException(String.Format(
                        "MemberType '{0}' is not supported",
                        Member.MemberType));
            }

        }
    }
}