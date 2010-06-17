using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using Relinq.Tuples;

namespace Relinq.V1.Xml.Metadata
{
    public class XmlMetadataInfo
    {
        public String Name { get; set; }
        public MemberTypes MemberType { get; set; }

        public String AssemblyToken { get; set; }
        public int ModuleToken { get; set; }
        public int MetadataToken { get; set; }
        public List<XmlMetadataInfo> TypeGenerics { get; set; }
        public List<XmlMetadataInfo> MethodGenerics { get; set; }

        private MemberInfo ToMetadata()
        {
            var ass = Assembly.Load(AssemblyToken);
            var mod = ass.GetModules().Where(m => m.MetadataToken == ModuleToken).Single();

            var typeGenerics = ToMany<Type>(TypeGenerics);
            var methodGenerics = ToMany<Type>(MethodGenerics);

            MemberInfo mi;
            switch (MemberType)
            {
                case MemberTypes.Constructor:
                case MemberTypes.Method:
                case MemberTypes.Property:
                case MemberTypes.TypeInfo:
                    mi = mod.ResolveMember(MetadataToken);
                    break;

                default:
                    throw new NotSupportedException(String.Format(
                                                        "MemberType '{0}' is not supported",
                                                        MemberType));
            }

            if (mi is Type)
            {
                var t = (Type)mi;
                if (t.IsGenericTypeDefinition)
                {
                    mi = t.MakeGenericType(typeGenerics);
                }
            }

            if (mi is MethodInfo)
            {
                if (mi.DeclaringType.IsGenericTypeDefinition)
                {
                    var gt = mi.DeclaringType.MakeGenericType(typeGenerics);
                    mi = gt.GetMethods().Where(gtm => gtm.MetadataToken == mi.MetadataToken).Single();
                }

                var m = (MethodInfo)mi;
                if (m.IsGenericMethodDefinition)
                {
                    mi = m.MakeGenericMethod(methodGenerics);
                }
            }

            if (mi is ConstructorInfo)
            {
                if (mi.DeclaringType.IsGenericTypeDefinition)
                {
                    var gt = mi.DeclaringType.MakeGenericType(typeGenerics);
                    mi = gt.GetConstructors().Where(gtm => gtm.MetadataToken == mi.MetadataToken).Single();
                }
            }

            return mi;
        }

        #region Public API

        public static XmlMetadataInfo FromMetadata(MemberInfo mi)
        {
            if (mi == null)
                return null;

            mi = PreprocessIfAnonymous(mi);

            var xmi = new XmlMetadataInfo
                          {
                              AssemblyToken = mi.Module.Assembly.FullName,
                              ModuleToken = mi.Module.MetadataToken,
                              MetadataToken = mi is PropertyInfo ? ((PropertyInfo)mi).GetGetMethod().MetadataToken : mi.MetadataToken,
                              Name = mi is Type ? ((Type)mi).AssemblyQualifiedName : mi.Name,
                              MemberType = mi.MemberType,
                          };

            if (mi is Type)
            {
                var t = (Type)mi;
                xmi.TypeGenerics = FromMany(t.GetGenericArguments());
            }

            if (mi is MethodInfo)
            {
                var m = (MethodInfo)mi;
                xmi.TypeGenerics = FromMany(m.DeclaringType.GetGenericArguments());
                xmi.MethodGenerics = FromMany(m.GetGenericArguments());
            }

            if (mi is PropertyInfo)
            {
                var m = ((PropertyInfo)mi).GetGetMethod();
                xmi.TypeGenerics = FromMany(m.DeclaringType.GetGenericArguments());
                xmi.MethodGenerics = FromMany(m.GetGenericArguments());
            }

            if (mi is ConstructorInfo)
            {
                var m = (ConstructorInfo)mi;
                xmi.TypeGenerics = FromMany(m.DeclaringType.GetGenericArguments());
            }

            if (xmi.MethodGenerics != null && xmi.MethodGenerics.Count == 0) xmi.MethodGenerics = null;
            if (xmi.TypeGenerics != null && xmi.TypeGenerics.Count == 0) xmi.TypeGenerics = null;

            return xmi;
        }

        public static List<XmlMetadataInfo> FromMany(IEnumerable<MemberInfo> mis)
        {
            return new List<XmlMetadataInfo>(FromManyImpl(mis));
        }

        private static IEnumerable<XmlMetadataInfo> FromManyImpl(IEnumerable<MemberInfo> mis)
        {
            foreach (var ex in mis)
            {
                yield return FromMetadata(ex);
            }
        }

        public static T ToMetadata<T>(XmlMetadataInfo xmi)
            where T : MemberInfo
        {
            if (xmi == null)
                return null;

            return (T)xmi.ToMetadata();
        }

        public static T[] ToMany<T>(IEnumerable<XmlMetadataInfo> mis)
            where T : MemberInfo
        {
            if (mis == null)
                return null;

            return new List<T>(ToManyImpl<T>(mis)).ToArray();
        }

        public static IEnumerable<T> ToManyImpl<T>(IEnumerable<XmlMetadataInfo> mis)
            where T : MemberInfo
        {
            foreach (var mi in mis)
            {
                yield return ToMetadata<T>(mi);
            }
        }

        #endregion

        #region AnonymousTypes <-> Tuples

        private static MemberInfo PreprocessIfAnonymous(MemberInfo mi)
        {
            if (mi is Type)
            {
                if (IsAnonymousType((Type)mi))
                {
                    return AnonymousToTuple((Type)mi);
                }
                else
                {
                    return mi;
                }
            }

            if (IsAnonymousType(mi.DeclaringType))
            {
                var tupleType = AnonymousToTuple(mi.DeclaringType);

                switch(mi.MemberType)
                {
                    case MemberTypes.Constructor:
                        return tupleType.GetConstructors().Single();

                    case MemberTypes.Method:
                        if (mi.Name.StartsWith("get_"))
                        {
                            var fieldName = mi.Name.Substring(4);

                            var fieldIndex = -1;
                            for (var i = 0; i < mi.DeclaringType.GetConstructors().Single().GetParameters().Length; ++i)
                            {
                                var pi = mi.DeclaringType.GetConstructors().Single().GetParameters()[i];
                                if (pi.Name == fieldName)
                                {
                                    fieldIndex = i;
                                }
                            }

                            if (fieldIndex == -1)
                            {
                                throw new NotSupportedException(String.Format(
                                                                    "Cannot resolve getter '{0}' of an anonymous type '{1}'",
                                                                    mi.Name,
                                                                    mi.DeclaringType.FullName));
                            }
                            else
                            {
                                return tupleType.GetMethod("get_Field" + fieldIndex);
                            }
                        }
                        else
                        {
                            return tupleType.GetMethod(mi.Name);
                        }

                    default:
                        throw new NotSupportedException(String.Format(
                                                            "Member '{0}' of an anonymous type is not supported",
                                                            mi.MemberType));
                }
            }

            return mi;
        }

        private static bool IsAnonymousType(Type t)
        {
            return t.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
                   t.Name.Contains("AnonymousType");
        }

        private static Type AnonymousToTuple(Type t)
        {
            Type genericTuple;

            var typeArgs = t.GetGenericArguments();
            switch(typeArgs.Length)
            {
                case 1:
                    genericTuple = typeof(Tuple1<>);
                    break;

                case 2:
                    genericTuple = typeof(Tuple2<,>);
                    break;

                default:
                    throw new NotSupportedException(String.Format(
                                                        "Anonymous types with '{0}' arguments are not supported",
                                                        typeArgs.Length));
            }

            var tupleArgs = new List<Type>();
            foreach (var typeArg in typeArgs)
            {
                if (!IsAnonymousType(typeArg))
                {
                    tupleArgs.Add(typeArg);
                }
                else
                {
                    tupleArgs.Add(AnonymousToTuple(typeArg));
                }
            }

            return genericTuple.MakeGenericType(tupleArgs.ToArray());
        }

        #endregion
    }
}