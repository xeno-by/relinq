using System.Collections.Generic;
using System.Linq;

namespace Relinq.Script.Semantics.Literals.AuxiliaryTypes
{
    public static class ListConversions
    {
        public static IOrderedEnumerable<T> ToOrderedEnumerable<T>(this List<T> list)
        {
            return list.OrderBy(t => 0);
        }

        public static IQueryable<T> ToQueryable<T>(this List<T> list)
        {
            return list.AsQueryable();
        }

        public static IOrderedQueryable<T> ToOrderedQueryable<T>(this List<T> list)
        {
            return list.AsQueryable().OrderBy(t => 0);
        }
    }
}