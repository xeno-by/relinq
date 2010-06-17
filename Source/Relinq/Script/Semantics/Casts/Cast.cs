using System;
using System.Reflection;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.Lookup;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.Casts
{
    public class Cast
    {
        public RelinqScriptType Source { get; private set; }
        public RelinqScriptType Destination { get; private set; }
        public MethodInfo UserDefined { get; private set; }

        public static Cast Lookup(RelinqScriptType source, RelinqScriptType destination)
        {
            if (source.HasPredefinedImplicitCastTo(destination))
            {
                return new Cast(source, destination);
            }
            else if (source.HasUserDefinedImplicitCastTo(destination))
            {
                var lookup = source.LookupUserDefinedImplicitCasts(destination);
                var sig = lookup.Resolve(((ClrType)source).Type, ((ClrType)destination).Type);
                return new Cast(source, destination, sig);
            }
            else
            {
                return null;
            }
        }

        private Cast(RelinqScriptType source, RelinqScriptType destination)
        {
            Source = source;
            Destination = destination;
        }

        public Cast(RelinqScriptType source, RelinqScriptType destination, MethodInfo userDefined)
            : this(source, destination)
        {
            UserDefined = userDefined;
        }
        
        public override string ToString()
        {
            var cast = String.Format("{0} -> {1}", Source, Destination);
            return cast + (UserDefined == null ? String.Empty : " (" + UserDefined.ToComprehensiveString() + ")");
        }

        public override bool Equals(object obj)
        {
            var other = obj as Cast;
            return other != null && Source == other.Source && Destination == other.Destination &&
                UserDefined == other.UserDefined;
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            num = (-1521134295 * num) + (Source == null ? 0 : Source.GetHashCode());
            num = (-1521134295 * num) + (Destination == null ? 0 : Destination.GetHashCode());
            num = (-1521134295 * num) + (UserDefined == null ? 0 : UserDefined.GetHashCode());
            return num;
        }

        public static bool operator ==(Cast c1, Cast c2)
        {
            return Equals(c1, c2);
        }

        public static bool operator !=(Cast c1, Cast c2)
        {
            return !(c1 == c2);
        }

        // http://code.google.com/p/relinq/wiki/BetterImplicitConversion
        public static bool operator >(Cast c1, Cast c2)
        {
            if (c1 == null || c2 == null) return c1 != null;
            if (c1.Destination is This || c2.Destination is This) return false;

            if (c1.Source == c2.Source && c1.Destination != c2.Destination)
            {
                if (c1.Source == c1.Destination) return true;
                if (c2.Source == c2.Destination) return false;

                if (c1.Destination.HasImplicitCastTo(c2.Destination) &&
                    !c2.Destination.HasImplicitCastTo(c1.Destination)) return true;
                if (c2.Destination.HasImplicitCastTo(c1.Destination) &&
                    !c1.Destination.HasImplicitCastTo(c2.Destination)) return false;

                if (c1.Source is Lambda)
                {
                    Func<RelinqScriptType, Type> retval = rst =>
                        rst is ClrType ? ((ClrType)rst).Type.GetFunctionSignature().ReturnType : (
                        rst is Lambda ? ((Lambda)rst).Type.GetFunctionSignature().ReturnType : null);

                    return Lookup(retval(c1.Source), retval(c1.Destination)) >
                        Lookup(retval(c2.Source), retval(c2.Destination));
                }
            }

            return false;
        }

        public static bool operator <(Cast c1, Cast c2)
        {
            return c2 > c1;
        }
    }
}
