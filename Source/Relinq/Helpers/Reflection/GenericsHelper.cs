using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Helpers.StructuralTrees;

namespace Relinq.Helpers.Reflection
{
    public static class GenericsHelper
    {
        public static Type[] XGetGenericArguments(this Type t)
        {
            if (t == null) return null;
            return t.IsGenericType ? t.GetGenericArguments() : new Type[0];
        }

        public static Type[] XGetGenericArguments(this MethodInfo mi)
        {
            if (mi == null) return null;
            return mi.IsGenericMethod ? mi.GetGenericArguments() : new Type[0];
        }

        public static Type XGetGenericDefinition(this Type t)
        {
            if (t == null) return null;
            return t.IsGenericType ? t.GetGenericTypeDefinition() : t;
        }

        public static MethodInfo XGetGenericDefinition(this MethodInfo mi)
        {
            if (mi == null) return null;
            return mi.IsGenericMethod ? mi.GetGenericMethodDefinition() : mi;
        }

        public static Type XMakeGenericType(this Type t, params Type[] targs)
        {
            if (t == null) return null;
            if (!t.IsGenericType || t.IsGenericParameter)
            {
                if (targs.Length != 0)
                {
                    throw new NotSupportedException(targs.Length.ToString());
                }
                else
                {
                    return t;
                }
            }
            else
            {
                return t.GetGenericTypeDefinition().MakeGenericType(targs);
            }
        }

        public static MethodInfo XMakeGenericMethod(this MethodInfo mi, params Type[] margs)
        {
            return mi.XMakeGenericMethod(mi.DeclaringType.XGetGenericArguments(), margs);
        }

        public static MethodInfo XMakeGenericMethod(this MethodInfo mi, Type[] targs, Type[] margs)
        {
            if (mi == null) return null;
            var pattern = (MethodInfo)mi.Module.ResolveMethod(mi.MetadataToken);

            var typeImpl = pattern.DeclaringType;
            if (!targs.IsNullOrEmpty()) typeImpl = typeImpl.MakeGenericType(targs);

            var methodImpl = typeImpl.GetMethods(BF.All).Single(mi2 => mi2.MetadataToken == mi.MetadataToken);
            if (!margs.IsNullOrEmpty()) methodImpl = methodImpl.MakeGenericMethod(margs);

            return methodImpl;
        }

        public static bool CanBeInferredFrom(this Type t1, Type t2)
        {
            return t1.InferFrom(t2) != null;
        }

        public static Dictionary<String, Type> InferFrom(this Type t1, Type t2)
        {
            var map1 = t1.GetStructuralTree();
            var map2 = t2.GetStructuralTree();

            var inferences = new Dictionary<String, Type>();
            var result = map1.Keys.All(key =>
            {
                var map1i = map1.First(kvp => kvp.Key == key);
                var map2i = map2.First(kvp => kvp.Key == key);

                if (!map1i.Value.SameBasisType(map2i.Value))
                {
                    if (!map1i.Value.IsGenericParameter)
                    {
                        return false;
                    }
                    else
                    {
                        var source = t2.SelectStructuralUnit(map2i.Key);
                        var t1inferred = t1.InferStructuralUnit(map1i.Key, source);

                        if (t1inferred != null)
                        {
                            if (!inferences.ContainsKey(map2i.Key))
                            {
                                inferences.Add(map2i.Key, source);
                                return true;
                            }
                            else
                            {
                                return inferences[map2i.Key] == source;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            });

            return result ? inferences : null;
        }
    }
}
