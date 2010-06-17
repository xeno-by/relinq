using System;
using System.Linq.Expressions;

namespace Relinq.Infrastructure.Client.Beans
{
    public class Bean<T> : Query<T>, IBean
    {
        public String Name { get; set; }

        public Bean()
        {
            Expression = Expression.Constant(this);
        }
    }
}