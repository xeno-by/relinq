using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Relinq.Helpers.Debug
{
#if DEBUG
    public static class DebugHelper
    {
        private static Dictionary<Type, int> _indices = new Dictionary<Type, int>();
        private static Dictionary<Type, Dictionary<int, IDebuggableObject>> _cache =
            new Dictionary<Type, Dictionary<int, IDebuggableObject>>();

        public static int RegisterForDebug(this IDebuggableObject @object)
        {
            if (!_cache.ContainsKey(@object.GetType()))
            {
                _indices.Add(@object.GetType(), 0);
                _cache.Add(@object.GetType(), new Dictionary<int, IDebuggableObject>());
            }

            var nextIndex = _indices[@object.GetType()]++;
            _cache[@object.GetType()][nextIndex] = @object;
            return nextIndex;
        }

        public static void UnregisterFromDebug(this Object @object)
        {
            if (_cache.ContainsKey(@object.GetType()))
            {
                foreach (var kvp in _cache[@object.GetType()].ToArray())
                {
                    if (kvp.Value == @object)
                    {
                        _cache[@object.GetType()].Remove(kvp.Key);
                    }
                }
            }
        }

        public static IEnumerable<IDebuggableObject> GetDebuggableChildren(this IDebuggableObject @object)
        {
            var impl = GetDebuggableChildrenImpl(@object).ToArray();
            return impl
                .OrderBy(obj => obj.GetType().Name)
                .ThenBy(obj => obj.Id);
        }

        private static IEnumerable<IDebuggableObject> GetDebuggableChildrenImpl(this IDebuggableObject @object)
        {
            foreach (var extent in _cache)
            {
                foreach (var extentItem in extent.Value.ToArray())
                {
                    if (extentItem.Value != null &&
                        extentItem.Value.DebuggableParents.Any(parent => parent == @object))
                    {
                        yield return extentItem.Value;
                    }
                }
            }
        }

        public static void DumpExtent(Type type)
        {
            if (Directory.Exists(@"d:\dumps\"))
            {
                using (var sw = new StreamWriter(@"d:\dumps\extent-" + type.Name + "-" + Guid.NewGuid() + ".dump"))
                {
                    sw.WriteLine("Extent: '{0}'", type.FullName ?? type.Name);

                    if (_cache.ContainsKey(type))
                    {
                        foreach (var extentItem in _cache[type].ToArray())
                        {
                            sw.WriteLine("\tItem: Id = '{0}', Content = '{1}'", extentItem.Key, extentItem.Value);
                        }

                        sw.WriteLine();
                    }
                    else
                    {
                        sw.WriteLine("NOTHING FOUND IN CACHE");
                    }
                }
            }
        }

        public static void DumpAll()
        {
            if (Directory.Exists(@"d:\dumps\"))
            {
                using (var sw = new StreamWriter(@"d:\dumps\all-" + Guid.NewGuid() + ".dump"))
                {
                    foreach (var extent in _cache)
                    {
                        sw.WriteLine("Extent: '{0}'", extent.Key.FullName ?? extent.Key.Name);

                        foreach (var extentItem in extent.Value.ToArray())
                        {
                            sw.WriteLine("\tItem: Id = '{0}', Content = '{1}'", extentItem.Key, extentItem.Value);
                        }

                        sw.WriteLine();
                    }
                }
            }
        }
    }
#else
    public static class DebugHelper
    {
        private static Dictionary<Type, int> _indices = new Dictionary<Type, int>();

        public static int RegisterForDebug(this Object @object)
        {
            if (!_indices.ContainsKey(@object.GetType()))
            {
                _indices.Add(@object.GetType(), 0);
            }

            return _indices[@object.GetType()]++;
        }
    }
#endif
}
