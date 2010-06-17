using System;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeSystem
{
    public class ClrType : RelinqScriptType
    {
        public Type Type { get; set; }

        public ClrType(Type t)
        {
            if (t.IsAuxiliaryCompilerType())
            {
                throw new ArgumentException("Auxiliary compiler type is not a valid type info");
            }
            else
            {
                Type = t;
            }
        }

        public override string ToString()
        {
            return "<" + Type.ToComprehensiveString() + ">";
        }

        public override bool Equals(object obj)
        {
            var other = obj as ClrType;
            return other != null && Type == other.Type;
        }

        public override int GetHashCode()
        {
            return Type == null ? 0 : Type.GetHashCode();
        }
    }
}