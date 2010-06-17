using System.Reflection;
using Relinq.Script.Compilation.MethodRedirection;
using System.Linq;

namespace Relinq.Helpers.Reflection
{
    public static class MethodRedirectionsHelper
    {
        public static bool IsRedirected(this MethodInfo mi)
        {
            return mi.RedirectedTo() != null;
        }

        public static MethodInfo RedirectedTo(this MethodInfo mi)
        {
            var redirectedTo = mi.AttrOrNull<RedirectToAttribute>();
            return redirectedTo == null ? null : redirectedTo.Target;
        }

        public static MethodInfo RedirectionRoot(this MethodInfo mi)
        {
            var current = mi;
            while (current.RedirectedTo() != null) current = current.RedirectedTo();
            return current;
        }
    }
}
