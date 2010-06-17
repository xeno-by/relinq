using System;

namespace Relinq.Script.Semantics.Lifting
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class LiftedAttribute : Attribute
    {
    }
}