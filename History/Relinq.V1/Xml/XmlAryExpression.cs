using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class XmlAryExpression : XmlExpression
    {
        public ExpressionType NodeType { get; set; }
        public XmlMetadataInfo Method { get; set; }
        public List<XmlExpression> Operands { get; set; }

        public static XmlAryExpression From(UnaryExpression uex)
        {
            return new XmlAryExpression
            {
                NodeType = uex.NodeType,
                Method = XmlMetadataInfo.FromMetadata(uex.Method),
                Operands = XmlExpression.FromMany(new Expression[]{uex.Operand})
            };
        }

        public static XmlAryExpression From(BinaryExpression bex)
        {
            return new XmlAryExpression
            {
                NodeType = bex.NodeType,
                Method = XmlMetadataInfo.FromMetadata(bex.Method),
                Operands = XmlExpression.FromMany(new Expression[]{bex.Left, bex.Right})
            };
        }

        protected override Expression ToExpression()
        {
            switch (NodeType)
            {
                case ExpressionType.Equal:
                    return Expression.Equal(
                        XmlExpression.ToExpression(Operands[0]),
                        XmlExpression.ToExpression(Operands[1]),
                        false,
                        XmlMetadataInfo.ToMetadata<MethodInfo>(Method));

                case ExpressionType.GreaterThan:
                    return Expression.GreaterThan(
                        XmlExpression.ToExpression(Operands[0]),
                        XmlExpression.ToExpression(Operands[1]),
                        false,
                        XmlMetadataInfo.ToMetadata<MethodInfo>(Method));

                case ExpressionType.NotEqual:
                    return Expression.NotEqual(
                        XmlExpression.ToExpression(Operands[0]),
                        XmlExpression.ToExpression(Operands[1]),
                        false,
                        XmlMetadataInfo.ToMetadata<MethodInfo>(Method));

                case ExpressionType.Quote:
                    return Expression.Quote(
                        XmlExpression.ToExpression(Operands[0]));

                default:
                    throw new NotSupportedException(String.Format(
                        "XmlAryExpression doesn't support node type '{0}'", NodeType));
            }
        }
    }
}