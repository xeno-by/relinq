using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Script.Semantics.Casts;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.StructuralTrees;

namespace Relinq.Script.Semantics.TypeInference
{
    public class ResolvedInvocation
    {
        public ResolvedSignature Resolved { get; private set; }
        public MethodInfo Signature { get { return Resolved.Signature; } }
        public TypeInferenceCache Inferences { get { return Resolved.Inferences; } }
        public IEnumerable<Cast> Casts { get; private set; }

        public ResolvedInvocation(ResolvedSignature resolved, IEnumerable<Cast> casts)
        {
            Resolved = resolved;

            Casts = casts ?? new Cast[0];
            if (!Signature.XArgsCount().Contains(Casts.Count()))
            {
                throw new NotSupportedException(String.Format(
                    "Signature '{0}' and casts [{1}] are incompatible.", 
                    Signature, Casts.StringJoin()));
            }
        }

        public override string ToString()
        {
            return String.Format("'{0}' @ [{1}]", Signature, Casts.StringJoin());
        }

        public override bool Equals(object obj)
        {
            var other = obj as ResolvedInvocation;
            return other != null && Resolved == other.Resolved && Casts.SequenceEqual(other.Casts);
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            num = (-1521134295 * num) + (Resolved == null ? 0 : Resolved.GetHashCode());
            Casts.ForEach(cast => num = (-1521134295 * num) + (cast == null ? 0 : cast.GetHashCode()));
            return num;
        }

        // http://code.google.com/p/relinq/wiki/BetterMethodAlternative
        public static bool operator >(ResolvedInvocation i1, ResolvedInvocation i2)
        {
            if (i1.Resolved.Ctx.Root != i2.Resolved.Ctx.Root) return false;

            Func<IEnumerable<Cast>, IEnumerable<Cast>, bool> betterCastSet = (c1s, c2s) =>
                c1s.AllMatch(c2s, (c1, c2) => !(c1 < c2)) &&
                c1s.AnyMatch(c2s, (c1, c2) => c1 > c2);

            if (betterCastSet(i1.Casts, i2.Casts)) return true;
            if (betterCastSet(i2.Casts, i1.Casts)) return false;

            var argc = i1.Signature.XArgsCount().Intersect(i2.Signature.XArgsCount()).Min();
            var sigeq = i1.Signature.ToXArgs(argc).AllMatch(
                i2.Signature.ToXArgs(argc), (i1t, i2t) => i1t.SameType(i2t));
            if (!sigeq) return false;

            Func<MethodInfo, int> mrank = mi =>
                ((!mi.IsPredefined() ? 1 : 0) << 6) +
                ((!mi.IsExtension() ? 1 : 0) << 5) +
                ((!mi.IsGenericMethod ? 1 : 0) << 4) +
                Math.Min(mi.GetParameters().Length, 15);
 
            if (mrank(i1.Signature) > mrank(i2.Signature)) return true;
            if (mrank(i1.Signature) < mrank(i2.Signature)) return false;

            Func<ResolvedInvocation, Dictionary<String, String>> mapping = ri => ri.Signature.GetGenericArgsMapping();
            var maps = new[] {mapping(i1), mapping(i2)};

            var spec = i1.Signature.GetParameters().Zip(i2.Signature.GetParameters(), (p1, p2, i) =>
            {
                Func<String, int, int> prank = (path, j) => 
                {
                    var key = "a" + i + path;
                    var hasKey = maps[j].ContainsKey(key);
                    return 
                        ((!hasKey ? 1 : 0) << 2) +
                        ((hasKey && maps[j][key].StartsWith("t") ? 1 : 0) << 1) +
                        ((hasKey && maps[j][key].StartsWith("m") ? 1 : 0) << 0);
                };

                Func<ParameterInfo, TypeStructuralTree> s = pi =>
                    pi.ParameterType.GetStructuralTree();
                var better =
                    s(p1).AllMatch(s(p2), (kvp1, kvp2) => prank(kvp1.Key, 0) >= prank(kvp2.Key, 1)) &&
                    s(p1).AnyMatch(s(p2), (kvp1, kvp2) => prank(kvp1.Key, 0) > prank(kvp2.Key, 1));
                var worse =
                    s(p1).AllMatch(s(p2), (kvp1, kvp2) => prank(kvp1.Key, 0) <= prank(kvp2.Key, 1)) &&
                    s(p1).AnyMatch(s(p2), (kvp1, kvp2) => prank(kvp1.Key, 0) < prank(kvp2.Key, 1));

                return better || worse ? (bool?)better : null;
            });

            return spec.All(speci => speci != false) && spec.Any(speci => speci == true);
        }

        public static bool operator <(ResolvedInvocation i1, ResolvedInvocation i2)
        {
            return i2 > i1;
        }
    }
}