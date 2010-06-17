using System;
using System.Linq;
using System.Linq.Expressions;
using Relinq.Infrastructure.Client.Beans;
using Relinq.Infrastructure.Shared;
using Relinq.Script.Semantics.Literals;

namespace Relinq.Infrastructure.Client.DataContexts
{
    public class ClientDataContext : IQueryProvider
    {
        public ClientDataContext()
        {
        }

        private Expression Expression { get; set; }

        public ClientDataContext(Expression expression)
        {
            Expression = expression;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new System.NotSupportedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(expression){Provider = this};
        }

        public object Execute(Expression expression)
        {
            throw new NotSupportedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var transformationFramework = new TransformationFramework();
            transformationFramework.Integration.RegisterCSharp(
                o => o is IBean,
                o => "ctx." + (String)o.GetType().GetProperty("Name").GetValue(o, null));

            var serializedAtClient = transformationFramework.CSharpToJS(expression);
            var serializedResultAtClient = RemoteTransportMock.Relinq(serializedAtClient);
            return (TResult)JsonDeserializer.Deserialize(serializedResultAtClient, typeof(TResult));
        }

        internal protected IBean GetBean(Type type)
        {
            var beanType = typeof(Bean<>).MakeGenericType(type);
            var beanInstance = Activator.CreateInstance(beanType);
            beanType.GetProperty("Provider").SetValue(beanInstance, this, null);
            return (IBean)beanInstance;
        }
    }
}