using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Script.Semantics.Literals.AuxiliaryTypes
{
    public class GroupingImpl<K, T> : Grouping, IGrouping<K, T>
    {
        [JsonProperty]
        public K Key { get; set; }

        [JsonProperty]
        public IEnumerable<T> Collection { get; set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }
    }
}