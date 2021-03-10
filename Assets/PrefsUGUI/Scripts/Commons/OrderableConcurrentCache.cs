using System;
using System.Collections.Concurrent;

namespace PrefsUGUI.Commons
{
    public class OrderableConcurrentCache<Key, Value>
    {
        public interface ITaker
        {
            void Take(Value value);
        }

        private ConcurrentDictionary<Key, Value> caches = new ConcurrentDictionary<Key, Value>();
        private ConcurrentQueue<Key> orders = new ConcurrentQueue<Key>();


        public void Add(Key key, Value value)
        {
            this.caches[key] = value;
            this.orders.Enqueue(key);
        }

        public void Remove(Key key)
        {
            this.caches.TryRemove(key, out var _);
        }

        public void TakeEach(ITaker taker)
        {
            while(this.orders.TryDequeue(out var key) == true)
            {
                if(this.caches.TryRemove(key, out var value) == true)
                {
                    taker.Take(value);
                }
            }

            this.caches.Clear();
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

        public static void AllTakeEach(ITaker taker, params OrderableConcurrentCache<Key, Value>[] caches)
        {
            for(var i = 0; i < caches.Length; i++)
            {
                caches[i]?.TakeEach(taker);
            }
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
