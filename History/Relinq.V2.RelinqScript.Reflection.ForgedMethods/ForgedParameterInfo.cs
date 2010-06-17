using System;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public class ForgedParameterInfo : IParameterInfo
    {
        private Type _parameterType;
        private ForgedParameterInfo(Type t)
        {
            _parameterType = t;
        }

        public static IParameterInfo Forge(Type t)
        {
            return new ForgedParameterInfo(t);
        }

        public Type ParameterType
        {
            get { return _parameterType; }
        }
    }
}