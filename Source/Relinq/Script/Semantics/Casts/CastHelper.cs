using System;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.Casts
{
    public static class CastHelper
    {
        public static Type CastSource(this MethodInfo mi)
        {
            return mi.IsUserDefinedImplicitCast() ? mi.GetParameters().Single().ParameterType : null;
        }

        public static Type CastDestination(this MethodInfo mi)
        {
            return mi.IsUserDefinedImplicitCast() ? mi.ReturnType : null;
        }
    }
}
