using System;
using Relinq.Script.Syntax.Operators;

namespace Relinq.Script.Semantics.Operators
{
    [AttributeUsage(AttributeTargets.Interface 
        | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PredefinedOperatorAttribute : Attribute
    {
        public OperatorType Type { get; private set; }

        public PredefinedOperatorAttribute(OperatorType type)
        {
            Type = type;
        }
    }
}