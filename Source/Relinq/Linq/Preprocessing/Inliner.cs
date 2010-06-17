using System.Linq.Expressions;

namespace Relinq.Linq.Preprocessing
{
    // http://code.google.com/p/relinq/wiki/Inliner
    public class Inliner : LinqVisitor
    {
        // todo: to be implemented

        public Inliner(Expression root)
            : base(root)
        {
        }
    }
}
