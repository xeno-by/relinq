using System;

namespace Relinq.Helpers.Assurance
{
    public static class AssuranceTools
    {
        private static void Assure(Func<bool> matter, Func<Exception> penalty)
        {
            if (!matter()) throw penalty();
        }

        public static T SurelyNotNull<T>(this T obj)
            where T : class
        {
            Assure(() => obj != null, () => new AssuranceFailedException("This should be not null by design"));
            return obj;
        }

        public static T SurelyNull<T>(this T obj)
            where T : class
        {
            Assure(() => obj == null, () => new AssuranceFailedException("This should be null by design"));
            return obj;
        }

        public static bool SurelyFalse(this bool obj)
        {
            Assure(() => !obj, () => new AssuranceFailedException("This should be false by design"));
            return obj;
        }

        public static bool SurelyTrue(this bool obj)
        {
            Assure(() => obj, () => new AssuranceFailedException("This should be true by design"));
            return obj;
        }
    }
}