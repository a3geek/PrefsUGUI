using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PrefsUGUI.Commons
{
    public class OrderableConcurrentCache<Key, Value>
    {
        private ConcurrentDictionary<Key, Value> caches = new ConcurrentDictionary<Key, Value>();
        private ConcurrentQueue<Key> orders = new ConcurrentQueue<Key>();


        public void Add(Key key, Value value)
        {
            this.caches[key] = value;
            this.orders.Enqueue(key);
        }

        public void TakeEach(Action<Value> action)
        {
            while(this.orders.TryDequeue(out var key) == true)
            {
                if(this.caches.TryRemove(key, out var value) == true)
                {
                    action?.Invoke(value);
                }
            }

            this.caches.Clear();
        }

        public static void AllTakeEach(Action<Value> action, params OrderableConcurrentCache<Key, Value>[] caches)
        {
            for(var i = 0; i < caches.Length; i++)
            {
                caches[i]?.TakeEach(action);
            }
        }
    }
}
