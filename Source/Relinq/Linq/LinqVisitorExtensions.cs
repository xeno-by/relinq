using System;

namespace Relinq.Linq
{
    public static class LinqVisitorExtensions
    {
        public static T VisitThenAcq<V, T>(this V visitor, Func<V, T> getter)
            where V : LinqVisitor
        {
            visitor.Visit();
            return getter(visitor);
        }
    }
}