using System;

namespace Relinq.Script.Compilation.SignatureValidation.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TrapAttribute : Attribute
    {
    }
}