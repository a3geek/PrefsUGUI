using System;
using System.Collections.Generic;

namespace PrefsUGUI.Commons
{
    public class MultikeyDictionary<T1, T2, TValue> where T1 : IComparable
    {
        public IReadOnlyDictionary<T1, TValue> Dictionary => this.dictionary;

        private Dictionary<T1, TValue> dictionary = new Dictionary<T1, TValue>();
        private Dictionary<T2, T1> secondKey = new Dictionary<T2, T1>();


        public bool TryGetValue(T1 key, out TValue value)
            => this.dictionary.TryGetValue(key, out value);

        public bool TryGetValue(T2 key, out TValue value)
        {
            if(this.secondKey.TryGetValue(key, out var key1) == true)
            {
                return this.dictionary.TryGetValue(key1, out value);
            }

            value = default;
            return false;
        }

        public bool TryGetKeyPair(T2 key2, out T1 key1)
            => this.secondKey.TryGetValue(key2, out key1);

        public bool TryGetKeyPair(T1 key1, out T2 key2)
        {
            foreach(var pair in this.secondKey)
            {
                if(pair.Value.CompareTo(key1) == 0)
                {
                    key2 = pair.Key;
                    return true;
                }
            }

            key2 = default;
            return false;
        }

        public TValue GetOrAdd(T1 key1, T2 key2, Func<TValue> creator)
        {
            if(this.dictionary.TryGetValue(key1, out var value) == true)
            {
                return value;
            }

            value = creator();
            this.dictionary[key1] = value;
            this.secondKey[key2] = key1;

            return value;
        }

        public bool Remove(T1 key1)
            => this.dictionary.Remove(key1);

        public bool Remove(T2 key2)
            => this.TryGetKeyPair(key2, out var key1) == true && this.dictionary.Remove(key1);
    }
}
