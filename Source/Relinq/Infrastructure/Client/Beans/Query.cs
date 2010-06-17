using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Relinq.Infrastructure.Client.Beans
{
    public class Query<T> : IOrderedQueryable<T>
    {
        internal Expression Expression { get; set; }

        protected Query()
        {
        }

        public Query(Expression expression)
        {
            Expression = expression;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var result = Provider.Execute<IEnumerable<T>>(Expression);
            return result.GetEnumerator();
        }

        Expression IQueryable.Expression
        {
            get { return Expression; }
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IQueryProvider Provider
        {
            get; internal set;
        }
    }
}