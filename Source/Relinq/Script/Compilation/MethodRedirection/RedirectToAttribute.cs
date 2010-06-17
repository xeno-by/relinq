using System;
using System.Linq;
using System.Reflection;

namespace Relinq.Script.Compilation.MethodRedirection
{
    // only valid for annotating predefined/lifted operators
    // todo. introduce checks to ensure this

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RedirectToAttribute : Attribute
    {
        public MethodInfo Target { get; private set; }

        public RedirectToAttribute(Type declaringType, String signature)
        {
            Target = declaringType.GetMethods().Where(mi => mi.ToString() == signature).Single();
        }
    }
}