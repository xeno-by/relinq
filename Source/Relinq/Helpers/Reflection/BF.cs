using System.Reflection;

namespace Relinq.Helpers.Reflection
{
    public static class BF
    {
        public const BindingFlags All = Public | Private;
        public const BindingFlags Instance = PublicInstance | PrivateInstance;
        public const BindingFlags Static = PublicStatic | PrivateStatic;

        public const BindingFlags Public = PublicInstance | PublicStatic;
        public const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;
        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;

        public const BindingFlags Private = PrivateInstance | PrivateStatic;
        public const BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;
    }
}