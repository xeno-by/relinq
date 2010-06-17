using System;
using System.Collections;
using System.Collections.Generic;

namespace Relinq.Helpers.Collections
{
    // todo. once and forever implement a dictionary that can dynamically switch state:
    // todo. regular, add-only, edit-only, readonly
    // todo. after that redo certain classes like StructuralMaps and InferenceCaches
    // todo. i mean: more precisely define their behavior using new capabilities
    public class InitOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _inner;

        public InitOnlyDictionary()
            : this(new Dictionary<TKey, TValue>())
        {
        }

        public InitOnlyDictionary(IDictionary<TKey, TValue> innerDictionary)
        {
            _inner = innerDictionary;
        }

        public bool ContainsKey(TKey key)
        {
            return _inner.ContainsKey(key);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _inner.Add(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotSupportedException(String.Format(
                "Cannot clear the dictionary: dictionary is init-only"));
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _inner.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _inner.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException(String.Format(
                "Cannot remove item '{0}' from the dictionary: dictionary is init-only", item));
        }

        public void Add(TKey key, TValue value)
        {
            _inner.Add(key, value);
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException(String.Format(
                "Cannot remove item with key '{0}' from the dictionary: dictionary is init-only", key));
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _inner.TryGetValue(key, out value);
        }

        public int Count
        {
            get
            {
                return _inner.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _inner[key];
            }
            set
            {
                if (!_inner.ContainsKey(key))
                {
                    ((IDictionary<TKey, TValue>)this).Add(key, value);
                }
                else
                {
                    if (!EqualityComparer<TValue>.Default.Equals(_inner[key], value))
                    {
                        throw new NotSupportedException(String.Format(
                            "Cannot assign a value '{0}' for the key '{1}': " +
                            "dictionary is init-only and it already contains the value '{2}'",
                            value, key, _inner[key]));
                    }
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return _inner.Keys;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return _inner.Values;
            }
        }
    }
}