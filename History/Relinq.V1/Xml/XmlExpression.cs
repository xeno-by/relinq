using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace Relinq.V1.Xml
{
    [XmlInclude(typeof(XmlConstantExpression))]
    [XmlInclude(typeof(QueryProxyReference))]
    [XmlInclude(typeof(XmlMemberExpression))]
    [XmlInclude(typeof(XmlMethodCallExpression))]
    [XmlInclude(typeof(XmlNewExpression))]
    [XmlInclude(typeof(XmlAryExpression))]
    [XmlInclude(typeof(XmlParameterExpression))]
    [XmlInclude(typeof(XmlLambdaExpression))]
    public abstract class XmlExpression
    {
        [XmlIgnore]
        public XmlExpression Parent { get; private set; }

        public static XmlExpression FromExpression(Expression ex)
        {
            if (ex == null)
                return null;

            if (ex is ConstantExpression)
            {
                var cex = (ConstantExpression)ex;
                if (cex.Type.Name.Contains("QueryProxy"))
                {
                    return QueryProxyReference.From(cex);
                }
                else
                {
                    return XmlConstantExpression.From(cex);
                }
            }

            if (ex is MemberExpression)
            {
                return XmlMemberExpression.From((MemberExpression)ex);
            }

            if (ex is MethodCallExpression)
            {
                return XmlMethodCallExpression.From((MethodCallExpression)ex);
            }

            if (ex is NewExpression)
            {
                return XmlNewExpression.From((NewExpression)ex);
            }

            if (ex is UnaryExpression)
            {
                return XmlAryExpression.From((UnaryExpression)ex);
            }

            if (ex is BinaryExpression)
            {
                return XmlAryExpression.From((BinaryExpression)ex);
            }

            if (ex is ParameterExpression)
            {
                return XmlParameterExpression.From((ParameterExpression)ex);
            }

            if (ex is LambdaExpression)
            {
                return XmlLambdaExpression.FromExpression((LambdaExpression)ex);
            }

            throw new NotSupportedException(String.Format(
                                                "Expression '{0}' is not supported",
                                                ex));
        }

        public static List<XmlExpression> FromMany(IEnumerable<Expression> exs)
        {
            return new List<XmlExpression>(FromManyImpl(exs));
        }

        private static IEnumerable<XmlExpression> FromManyImpl(IEnumerable<Expression> exs)
        {
            foreach(var ex in exs)
            {
                yield return FromExpression(ex);
            }
        }

        protected abstract Expression ToExpression();

        public static Expression ToExpression(XmlExpression source)
        {
            if (source == null)
                return null;

            FillParentRecursive(source.Parent, source);
            return source.ToExpression();
        }

        public static IEnumerable<Expression> ToMany(IEnumerable<XmlExpression> xmlExpressions)
        {
            if (xmlExpressions == null)
                yield break;

            foreach (var xmlExpression in xmlExpressions)
            {
                yield return xmlExpression.ToExpression();
            }
        }

        private static void FillParentRecursive(XmlExpression parent, XmlExpression child)
        {
            if (child != null)
            {
                child.Parent = parent;
                foreach (var grandChild in AcquireChildren(child))
                {
                    FillParentRecursive(child, grandChild);
                }
            }
        }

        private static IEnumerable<XmlExpression> AcquireChildren(XmlExpression expression)
        {
            foreach (var property in expression.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.Name == "Parent")
                    continue;

                if (typeof(XmlExpression).IsAssignableFrom(property.PropertyType))
                {
                    yield return (XmlExpression)property.GetValue(expression, null);
                }

                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var enumerable = (IEnumerable)property.GetValue(expression, null);
                    if (enumerable == null)
                        continue;

                    foreach(var entry in enumerable)
                    {
                        if (entry is XmlExpression)
                        {
                            yield return (XmlExpression)entry;
                        }
                    }
                }
            }
        }
    }
}