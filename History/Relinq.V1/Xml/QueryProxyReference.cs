using System;
using System.Linq.Expressions;
using System.Reflection;
using Relinq.V1.Xml.Metadata;

namespace Relinq.V1.Xml
{
    public class QueryProxyReference : XmlExpression
    {
        public XmlMetadataInfo Type { get; set; }

        public static QueryProxyReference From(ConstantExpression cex)
        {
            return new QueryProxyReference
            {
                Type = XmlMetadataInfo.FromMetadata(cex.Type)
            };
        }
        
        protected override Expression ToExpression()
        {
            var ass = Assembly.Load("Playground");
            var qimpl_t = ass.GetType("Playground.V1.Server.RelinqImpl+QueryImpl");
            var qimpl = Activator.CreateInstance(qimpl_t);

            return Expression.Constant(qimpl, qimpl_t);
        }
    }
}