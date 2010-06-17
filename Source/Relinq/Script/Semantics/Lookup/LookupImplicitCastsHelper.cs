using System;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.Casts;
using Relinq.Script.Semantics.Lifting;
using Relinq.Script.Semantics.Literals;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;

namespace Relinq.Script.Semantics.Lookup
{
    // http://code.google.com/p/relinq/wiki/ImplicitConversions
    public static class LookupImplicitCastsHelper
    {
        public static bool HasImplicitCastTo(this Type relinqSrc, RelinqScriptType relinqDest)
        {
            return ((RelinqScriptType)relinqSrc).HasImplicitCastTo(relinqDest);
        }

        public static bool HasImplicitCastTo(this RelinqScriptType relinqSrc, RelinqScriptType relinqDest)
        {
            return relinqSrc.HasPredefinedImplicitCastTo(relinqDest) || 
                relinqSrc.HasUserDefinedImplicitCastTo(relinqDest);
        }
        
        public static bool HasPredefinedImplicitCastTo(this Type relinqSrc, RelinqScriptType relinqDest)
        {
            return ((RelinqScriptType)relinqSrc).HasPredefinedImplicitCastTo(relinqDest);
        }

        public static bool HasPredefinedImplicitCastTo(this RelinqScriptType relinqSrc, RelinqScriptType relinqDest)
        {
            // "This" is an auxiliary type that is introduced to let me
            // treat both regular and extension methods uniformly.
            // It can appear only during matching of CLR method signature with 
            // a call-type RelinqScriptExpression.
            if (relinqSrc is This || relinqDest is This)
            {
                return true;
            }

            if (relinqSrc is ClrType && relinqDest is ClrType)
            {
                var src = ((ClrType)relinqSrc).Type;
                var dest = ((ClrType)relinqDest).Type;

                if (src.IsOpenGeneric() || dest.IsOpenGeneric())
                {
                    throw new NotImplementedException(String.Format(
                        "Don't know how to analyze the cast: '{0} -> {1}'",
                        relinqSrc, relinqDest));
                }
                else
                {
                    if (src.IsClrNumeric() && dest.IsClrNumeric())
                    {
                        if (dest == typeof(char) || src == typeof(decimal))
                        {
                            return false;
                        }
                        else if (src == typeof(char))
                        {
                            return typeof(ushort).HasPredefinedImplicitCastTo(dest);
                        }
                        else
                        {
                            Func<Type, String, double> constFieldToDouble = (t, f) =>
                                Convert.ToDouble(t.GetField(f).GetValue(null));
                            Func<Type, double> minValue = t => constFieldToDouble(t, "MinValue");
                            Func<Type, double> maxValue = t => constFieldToDouble(t, "MaxValue");

                            return minValue(dest) <= minValue(src) && maxValue(src) <= maxValue(dest);
                        }
                    }
                    else if (src.IsNullable() || dest.IsNullable())
                    {
                        if (dest.IsNullable())
                        {
                            return src.UndecorateNullable().HasPredefinedImplicitCastTo(
                                dest.UndecorateNullable());
                        }
                        else
                        {
                            return src.UndecorateNullable().LookupInheritanceChain().Contains(dest);
                        }
                    }

                    // todo. rewrite using info about the source of the integer
                    // this will fix two bugs:
                    // 1) this conversion is now active even if the source is not a literal
                    // 2) this stuff won't work if int can't be cast to the underlying type
                    //    since by default most of numeric literals get the int type.
                    else if (src.IsInteger() && dest.IsEnum)
                    {
                        return src.HasImplicitCastTo(Enum.GetUnderlyingType(dest));
                    }
                    // we have to use this to support expressions like ".Prop == TypeCode.Int32"
                    else if (dest.IsInteger() && src.IsEnum)
                    {
                        return dest.HasImplicitCastTo(Enum.GetUnderlyingType(src));
                    }


                    return dest.IsAssignableFrom(src);
                }
            }
            else
            {
                if (relinqDest is ClrType)
                {
                    var dest = ((ClrType)relinqDest).Type;
                    if (relinqSrc is Null && 
                        (dest.IsReferenceType() || dest.IsNullable()))
                    {
                        return true;
                    }
                    else if (relinqSrc is Lambda && dest.IsFunctionType())
                    {
                        // todo. what the hack?!
                        return true; // since TypeInferenceMethods will only call Cast.Lookup for apriori resolved lambdas
                    }
                    else if (relinqSrc is UnknownConstant)
                    {
                        if (dest == typeof(char))
                        {
                            var @const = relinqSrc as UnknownStringizedObject;
                            return @const != null && @const.Constant.Data.Length == 1;
                        }
                        else
                        {
                            // todo. think about something better than this hack lol
                            try
                            {
                                var data = ((UnknownConstant)relinqSrc).Constant.Data;
                                JsonDeserializer.Deserialize(data, dest);
                            }
                            catch(Exception)
                            {
                                return false;
                            }

                            return true;
                        }
                    }
                    else if (relinqSrc is MethodGroup)
                    {
                        // todo. requires TypeInferenceContext
                        throw new NotImplementedException(String.Format(
                            "Don't know how to analyze the cast: '{0} -> {1}'",
                            relinqSrc, relinqDest));
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool HasUserDefinedImplicitCastTo(this RelinqScriptType relinqSrc, RelinqScriptType relinqDest)
        {
            var src = relinqSrc as ClrType;
            var dest = relinqDest as ClrType;

            return (src == null || dest == null) ? false :
                src.Type.HasUserDefinedImplicitCastTo(dest.Type);
        }

        public static bool HasUserDefinedImplicitCastTo(this Type src, Type dest)
        {
            var lookup = src.LookupUserDefinedImplicitCasts(dest);
            return lookup == null ? false : lookup.Resolve(src, dest) != null;
        }

        public static MethodGroup LookupUserDefinedImplicitCasts(this RelinqScriptType relinqSrc, RelinqScriptType relinqDest)
        {
            var src = relinqSrc as ClrType;
            var dest = relinqDest as ClrType;

            return (src == null || dest == null) ? null :
                src.Type.LookupUserDefinedImplicitCasts(dest.Type);
        }

        public static MethodGroup LookupUserDefinedImplicitCasts(this Type src, Type dest)
        {
            Func<Type, Type, bool> encompasses = (t1, t2) => t2.HasPredefinedImplicitCastTo(t1);
            Func<Type, Type, bool> isEncompassedBy = (t1, t2) => t1.HasPredefinedImplicitCastTo(t2);

            var alternatives = new[] {src, dest}.Select(type =>
                type.UndecorateNullable().LookupThisAndBaseTypes().Select(bt => bt
                    .GetMethods(BF.PublicStatic)
                    .Where(mi => mi.DeclaringType == bt)
                    .Where(mi => mi.IsUserDefinedImplicitCast())
                    .Where(mi => isEncompassedBy(mi.CastDestination(), dest.UndecorateNullable()))
                    .Where(mi => encompasses(mi.CastSource(), src.UndecorateNullable()))))
                .Flatten().Flatten();
            alternatives = alternatives.Concat(alternatives.LiftSignatures());

            return alternatives.IsNullOrEmpty() ? null : new MethodGroup(alternatives, "<cast>");
        }
    }
}
