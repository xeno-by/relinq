using System;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeSystem
{
    public abstract class RelinqScriptType
    {
        public static implicit operator RelinqScriptType(Type t)
        {
            if (t.IsAuxiliaryCompilerType())
            {
                // if we pass here an UnknownConstant or any other type
                // that doesn't have a parameterless ctor -> the stuff will crash
                return (RelinqScriptType)Activator.CreateInstance(t);
            }
            else
            {
                return new ClrType(t);
            }
        }

        public static bool operator ==(RelinqScriptType t1, RelinqScriptType t2)
        {
            return Equals(t1, t2);
        }

        public static bool operator !=(RelinqScriptType t1, RelinqScriptType t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode() 
        {
            return 0;
        }
    }
}
