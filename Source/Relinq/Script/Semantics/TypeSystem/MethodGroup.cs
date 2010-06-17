using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.Lookup;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeSystem
{
    public class MethodGroup : RelinqScriptType
    {
        public IEnumerable<MethodInfo> Alts { get; set; }
        private String Name { get; set; }

        private MethodGroup(IEnumerable<MethodInfo> alts)
        {
            alts = alts ?? new MethodInfo[0];
            Alts = new ReadOnlyCollection<MethodInfo>(new List<MethodInfo>(
                alts.Where(alt1 => !alts.Any(alt2 => alt1 != alt2 && alt1.IsHiddenBy(alt2)))));
        }

        public MethodGroup(IEnumerable<MethodInfo> alts, String name)
            : this(alts)
        {
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("[MG {0}: {1}]", Name ?? "?", Alts.Select(alt => alt.ToComprehensiveString()).StringJoin());
        }

        private Func<IEnumerable<MethodInfo>, IEnumerable<MethodInfo>> _orderer = mis => mis
            .OrderBy(mi => mi.Module.Assembly.FullName)
            .ThenBy(mi => mi.Module.MetadataToken)
            .ThenBy(mi => mi.MetadataToken);

        public override bool Equals(object obj)
        {
            var other = obj as MethodGroup;
            return other != null && _orderer(Alts).SequenceEqual(_orderer(other.Alts));
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            _orderer(Alts).ForEach(alt => num = (-1521134295 * num) + alt.GetHashCode());
            return num;
        }
    }
}