using System;
using System.Reflection;

namespace Relinq.RelinqScript.Reflection.ForgedMethods
{
    public class DotNetParameterInfo : IParameterInfo
    {
        private ParameterInfo _pi;
        private DotNetParameterInfo(ParameterInfo pi)
        {
            _pi = pi;
        }

        public static IParameterInfo FromRuntimeParameter(ParameterInfo mi)
        {
            return new DotNetParameterInfo(mi);
        }

        public Type ParameterType
        {
            get { return _pi.ParameterType; }
        }
    }
}