using System;
using System.Linq;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    public static class FuzzyHelper
    {
        public static int GetFuzzinessLevel(this Type runtimeType)
        {
            if (runtimeType == null)
            {
                return (int)TypeFuzzinessLevel.Unknown;
            }
            else if (runtimeType.IsGenericParameter)
            {
                return (int)TypeFuzzinessLevel.Parameter;
            }
            else
            {
                return (int)TypeFuzzinessLevel.Crisp;
            }
        }

        public static bool IsFullyInferred(this FuzzyType t)
        {
            return 
                t.IsCrisp() && 
                    t.GenericArgs.All(arg => arg.IsFullyInferred());
        }

        public static bool IsCrisp(this FuzzyType fuzzy)
        {
            return fuzzy.FuzzinessLevel == (int)TypeFuzzinessLevel.Crisp;
        }
    }
}