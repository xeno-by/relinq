using System;
using System.Reflection;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeInference
{
    public class ResolvedSignature
    {
        public MethodInfo Signature { get; private set; }
        public TypeInferenceContext Ctx { get; private set; }
        public TypeInferenceCache Inferences { get { return Ctx.Inferences; } }

        public ResolvedSignature(TypeInferenceContext ctx, MethodInfo signature)
        {
            Ctx = ctx;

            Signature = signature;
            if (Signature.IsOpenGeneric())
            {
                throw new NotSupportedException(String.Format(
                    "Cannot create a resolved signature from an open generic method '{0}'.", signature));
            }
        }

        public override string ToString()
        {
            return String.Format("'{0}'", Signature);
        }

        public override bool Equals(object obj)
        {
            var other = obj as ResolvedSignature;
            return other != null && Ctx.Root == other.Ctx.Root && Signature == other.Signature;
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            num = (-1521134295 * num) + (Ctx.Root == null ? 0 : Ctx.Root.GetHashCode());
            return (-1521134295 * num) + (Signature == null ? 0 : Signature.GetHashCode());
        }
    }
}