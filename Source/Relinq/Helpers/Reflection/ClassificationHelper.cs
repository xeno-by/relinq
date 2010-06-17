using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.Lifting;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.UserDefinedCasts;
using Relinq.Script.Semantics.UserDefinedOperators;
using Relinq.Script.Syntax.Operators;
using Relinq.Script.Semantics.Operators;

namespace Relinq.Helpers.Reflection
{
    public static class ClassificationHelper
    {
        public static bool IsAuxiliaryCompilerType(this Type t)
        {
            return t.Assembly == typeof(RelinqScriptType).Assembly &&
                t.Namespace == typeof(RelinqScriptType).Namespace;
        }

        public static bool IsEnumerableType(this Type t)
        {
            return t.GetEnumerableElement() != null;
        }

        public static bool IsListType(this Type t)
        {
            return t.GetListElement() != null;
        }

        public static bool IsDictionaryType(this Type t)
        {
            return t.GetDictionaryElement() != null;
        }

        public static bool IsEnumerableOf<T>(this Type t)
        {
            return t.IsEnumerableOf(typeof(T));
        }

        public static bool IsListOf<T>(this Type t)
        {
            return t.IsListOf(typeof(T));
        }

        public static bool IsDictionaryOf<K, V>(this Type t)
        {
            return t.IsDictionaryOf(typeof(K), typeof(V));
        }

        public static bool IsEnumerableOf(this Type t, Type elType)
        {
            return t.IsEnumerableType() && t.GetEnumerableElement() == elType;
        }

        public static bool IsListOf(this Type t, Type elType)
        {
            return t.IsListType() && t.GetListElement() == elType;
        }

        public static bool IsDictionaryOf(this Type t, Type keyType, Type valueType)
        {
            return t.IsDictionaryType() && t.GetDictionaryElement().GetType() == 
                typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);
        }

        public static Type GetEnumerableElement(this Type t)
        {
            var unambiguousEnum =
               t.SameMetadataToken(typeof(IEnumerable<>)) ? t :
               t.GetInterfaces().Where(iface => iface.SameMetadataToken(typeof(IEnumerable<>))).SingleOrDefault();
            return unambiguousEnum == null ? null : unambiguousEnum.XGetGenericArguments().Single();
        }

        public static Type GetListElement(this Type t)
        {
            var enumOfT = t.GetEnumerableElement();
            if (enumOfT == null)
            {
                return null;
            }
            else
            {
                var defaultCtor = t.GetConstructor(new Type[0]);
                var addMethod = t.GetMethod("Add", enumOfT.AsArray());

//                var enumCtor = t.GetConstructor(new Type[] { typeof(IEnumerable<>).MakeGenericType(enumOfT) });
//                var vaCtor =
//                    (from ctor in t.GetConstructors()
//                    let p = ctor.GetParameters()
//                    where p.Length == 1 && p[0].IsDefined(typeof(ParamArrayAttribute), true) && p[0].ParameterType == enumOfT
//                    select ctor).SingleOrDefault();

                var isListTypeOfT = ((addMethod != null && defaultCtor != null) /*|| enumCtor != null || vaCtor != null*/);
                return isListTypeOfT ? enumOfT : null;
            }
        }

        public static KeyValuePair<Type, Type>? GetDictionaryElement(this Type t)
        {
            var unambiguousDic =
                t.SameMetadataToken(typeof(IDictionary<,>)) ? t :
                t.GetInterfaces().Where(iface => iface.SameMetadataToken(typeof(IDictionary<,>))).SingleOrDefault();
            return unambiguousDic == null ? null : (KeyValuePair<Type, Type>?)
                new KeyValuePair<Type, Type>(
                    unambiguousDic.XGetGenericArguments()[0],
                    unambiguousDic.XGetGenericArguments()[1]);
        }

        public static bool IsReferenceType(this Type t)
        {
            return t.IsClass || t.IsInterface;
        }

        public static bool IsNonNullableValueType(this Type t)
        {
            return t.IsValueType && !t.IsNullable() && t != typeof(void);
        }

        public static bool IsNullable(this Type t)
        {
            return t.SameMetadataToken(typeof(Nullable<>));
        }

        public static bool IsNullable(this Object o)
        {
            return o.GetType().IsNullable();
        }

        public static Type UndecorateNullable(this Type t)
        {
            return t.IsNullable() ? Nullable.GetUnderlyingType(t) : t;
        }

        public static Object UndecorateNullable(this Object o)
        {
            return o.GetType().IsNullable() ? o.GetType().GetProperty("Value").GetValue(o, null) : o;
        }

        public static bool IsJSPrimitiveOrNullable(this Type t)
        {
            return t.IsJSNumericOrNullable() || t.IsTOrNullableT<bool>() || t.IsTOrNullableT<char>();
        }

        public static bool IsJSNumericOrNullable(this Type t)
        {
            if (t.IsNullable())
            {
                return IsJSNumericOrNullable(t.UndecorateNullable());
            }
            else
            {
                return t != typeof(Decimal) && t != typeof(char) && t.IsClrNumeric();
            }
        }

        public static bool IsInteger(this Type t)
        {
            return t.IsClrNumeric() && t != typeof(Decimal) && t != typeof(char)
                && t != typeof(float) && t != typeof(double);
        }

        public static bool IsClrNumeric(this Type t)
        {
            // compact, tho rather gimmick solution
            return typeof(Decimal).GetMethods()
                .Where(mi => mi.Name.StartsWith("op_Explicit"))
                .Any(mi => mi.ReturnType == t);
        }

        public static bool IsTOrNullableT<T>(this Type t)
            where T : struct
        {
            if (t.IsNullable())
            {
                return IsTOrNullableT<T>(t.UndecorateNullable());
            }
            else
            {
                return t == typeof(T);
            }
        }

        public static bool IsEnumOrNullable(this Type t)
        {
            if (t.IsNullable())
            {
                return IsEnumOrNullable(t.UndecorateNullable());
            }
            else
            {
                return t.IsEnum;
            }
        }

        public static bool IsDelegate(this Type t)
        {
            if (t.SameMetadataToken(typeof(Delegate))) return false;
            if (t.SameMetadataToken(typeof(MulticastDelegate))) return false;

            for (var current = t; current != null; current = current.BaseType)
                if (current.SameMetadataToken(typeof(Delegate))) return true;

            return false;
        }

        public static bool IsTypedLambdaExpression(this Type t)
        {
            return typeof(Expression<>).SameMetadataToken(t);
        }

        public static bool IsFunctionType(this Type t)
        {
            return t.IsDelegate() || t.IsTypedLambdaExpression();
        }

//        public static bool IsSystemFunc(this Type t)
//        {
//            return t.Assembly == typeof(Func<>).Assembly &&
//                t.Namespace == typeof(Func<>).Namespace &&
//                t.Name.StartsWith("Func`");
//        }

        public static bool IsRemote(this Type t)
        {
            return t.GetInterface("IBean") != null;
        }

        public static bool IsOpenGeneric(this Type t)
        {
            if (t.IsFunctionType())
            {
                var desc = t.GetFunctionDesc();
                return desc.ReturnValue.IsOpenGeneric() ||
                    desc.Args.Any(arg => arg.IsOpenGeneric());
            }
            else if (t.IsArray)
            {
                return t.GetElementType().IsOpenGeneric();
            }
            else
            {
                return t.IsGenericParameter || t.XGetGenericArguments().Any(arg => arg.IsOpenGeneric());
            }
        }

        public static bool IsOpenGeneric(this MethodInfo t)
        {
            return t.ReturnType.IsOpenGeneric() || 
                t.GetParameters().Any(pi => pi.ParameterType.IsOpenGeneric()) ||
                t.ContainsGenericParameters; // example: bool Meth<T>(int x);
        }

        public static bool IsAnonymous(this Type t)
        {
            return
                t.HasAttr<CompilerGeneratedAttribute>() &&
                (Regex.IsMatch(t.Name, @"\<\>.*AnonymousType.*") || // C# anonymous types
                t.Name.StartsWith("RelinqAnonymousType<")); // Relinq anonymous types
        }

        public static bool IsTransparentIdentifier(this String s)
        {
            return Regex.IsMatch(s, @"\<\>.*TransparentIdentifier.*");
        }

        public static bool IsExtension(this MethodInfo mi)
        {
            return mi.HasAttr<ExtensionAttribute>();
        }

        public static bool IsIndexer(this MethodInfo mi)
        {
            var t = mi.DeclaringType;
            if (t.IsArray)
            {
                return mi.Name == "Get";
            }
            else
            {
                if (!t.HasAttr<DefaultMemberAttribute>())
                {
                    return false;
                }
                else
                {
                    var indexerName = t.Attr<DefaultMemberAttribute>().MemberName;
                    return mi.Name == "get_" + indexerName;
                }
            }
        }

        public static bool IsPredefined(this MethodInfo mi)
        {
            return mi.IsPredefinedOperator();
        }

        public static bool IsOperator(this MethodInfo mi)
        {
            return mi.IsPredefinedOperator() || mi.IsUserDefinedOperator();
        }

        public static bool IsPredefinedOperator(this MethodInfo sig)
        {
            var isPredefined = sig.HasAttr<PredefinedOperatorAttribute>() ||
                sig.DeclaringType.HasAttr<PredefinedOperatorAttribute>();
            var isLiftedFromPredefined = sig.IsLifted() && sig.RedirectedTo().IsPredefinedOperator();

            return isPredefined || isLiftedFromPredefined;
        }

        public static bool IsBooleanOperator(this MethodInfo mi)
        {
            return mi.IsStatic && mi.IsSpecialName &&
                UserDefinedOperatorsHelper.IsBooleanOpImpl(mi.Name) &&
                mi.ReturnType == typeof(bool) &&
                mi.GetParameters().Length == 1 &&
                mi.GetParameters()[0].ParameterType.SameType(mi.DeclaringType);
        }

        public static bool IsUserDefinedOperator(this MethodInfo mi)
        {
            var isUserDefined = mi.IsStatic && mi.IsSpecialName && 
                UserDefinedOperatorsHelper.IsOpMethodImpl(mi.Name);
            var isLiftedFromUserDefined = mi.IsLifted() && 
                mi.RedirectedTo().IsUserDefinedOperator();

            return isUserDefined || isLiftedFromUserDefined;
        }

        public static bool IsTrap(this MethodInfo mi)
        {
            return mi.RedirectionRoot().HasAttr<TrapAttribute>();
        }

        public static bool IsLifted(this MethodInfo mi)
        {
            return mi.HasAttr<LiftedAttribute>();
        }
        
        public static IEnumerable<OperatorType> GetOperatorTypes(this MethodInfo mi)
        {
            if (mi.IsLifted())
            {
                return mi.RedirectedTo().GetOperatorTypes();
            }
            else
            {
                if (mi.IsPredefinedOperator())
                {
                    Func<ICustomAttributeProvider, IEnumerable<OperatorType>> opTypes = provider => 
                        provider.Attrs<PredefinedOperatorAttribute>().Select(attr => attr.Type);

                    return opTypes(mi).Concat(opTypes(mi.DeclaringType));
                }
                else if (mi.IsUserDefinedOperator())
                {
                    return UserDefinedOperatorsHelper.GetOperatorTypeByMethodImpl(mi.Name);
                }
                else
                {
                    return new OperatorType[0];
                }
            }
        }

        public static bool IsUserDefinedImplicitCast(this MethodInfo mi)
        {
            var isUserDefined = mi.IsStatic && mi.IsSpecialName && 
                UserDefinedImplicitCastsHelper.IsCastImpl(mi.Name);
            var isLiftedFromUserDefined = mi.IsLifted() && 
                mi.RedirectedTo().IsUserDefinedImplicitCast();

            return isUserDefined || isLiftedFromUserDefined;
        }

        public static bool IsStatic(this MemberInfo mi)
        {
            if (mi is PropertyInfo)
            {
                return ((PropertyInfo)mi).GetGetMethod().IsStatic;
            }
            else if (mi is EventInfo)
            {
                return ((EventInfo)mi).GetAddMethod().IsStatic;
            }
            else
            {
                var pi = mi.GetType().GetProperty("IsStatic", typeof(bool));
                if (pi != null)
                {
                    return (bool)pi.GetValue(mi, null);
                }
                else
                {
                    throw new NotSupportedException(mi.ToString());
                }
            }
        }

        public static bool IsVarargs(this MethodInfo mi)
        {
            var @params = mi.GetParameters();
            return @params.Length != 0 &&
                @params[@params.Length - 1].HasAttr<ParamArrayAttribute>();
        }
    }
}