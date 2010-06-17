using System;

namespace Relinq.Exceptions.Core
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IncludeInMessageAttribute : Attribute
    {
    }
}