using System;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks
{
    public static class TypeLinkHelper
    {
        public static TypeLink IsIdenticalTo(this FuzzyType first, FuzzyType second)
        {
            return new IdentityLink(first, second);
        }

        public static TypeLink IsFieldOf(this FuzzyType first, FuzzyType second, String fieldName)
        {
            return first.IsIdenticalTo(second.GetFieldOrProperty(fieldName));
        }

        public static TypeLink IsArgOf(this FuzzyType first, FuzzyLambda second, String argName)
        {
            return first.IsIdenticalTo(second.Args[argName]);
        }

        public static TypeLink IsRetValOf(this FuzzyType first, FuzzyLambda second)
        {
            return first.IsIdenticalTo(second.ReturnValue);
        }

        public static TypeLink IsTargetOf(this FuzzyType first, FuzzyMethodBinding second)
        {
            return first.IsIdenticalTo(second.Target);
        }

        public static TypeLink IsArgOf(this FuzzyType first, FuzzyMethodBinding second, int argIndex)
        {
            return first.IsIdenticalTo(second.Args[argIndex]);
        }

        public static TypeLink IsRetValOf(this FuzzyType first, FuzzyMethodBinding second)
        {
            return first.IsIdenticalTo(second.ReturnValue);
        }

        public static TypeLink BindToOneOfAlternatives(this FuzzyMethodBinding binding, FuzzyType bindingTypePoint, FuzzyType alternativeTypePoint)
        {
            return new MethodBindingToOneOfAlternativesLink(binding, bindingTypePoint, alternativeTypePoint);
        }
    }
}