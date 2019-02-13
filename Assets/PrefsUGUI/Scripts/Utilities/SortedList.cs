using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PrefsUGUI.Utilities
{
    [Serializable]
    public sealed class SortedList<T> : IEnumerable<T>
    {
        public T this[int index]
        {
            get { return this.items[index]; }
        }

        public int Count => this.items.Count;
        public ReadOnlyCollection<T> Items => this.items.AsReadOnly();
        public ReadOnlyCollection<int> Orders => this.orders.AsReadOnly();

        private List<T> items = new List<T>();
        private List<int> orders = new List<int>();
        private Func<T, T, int> sorter = null;


        public SortedList(Func<T, T, int> equalSorter = null)
        {
            this.sorter = equalSorter ?? ((e1, e2) => 0);
        }

        public int Add(T item, int order)
        {
            for(var i = 0; i < this.orders.Count; i++)
            {
                if(this.orders[i] == order)
                {
                    var e = this.items[i];
                    var sorted = this.sorter(e, item);

                    this.items[i] = sorted <= 0 ? e : item;
                    item = sorted <= 0 ? item : e;
                }
                else if(this.orders[i] > order)
                {
                    this.items.Insert(i, item);
                    this.orders.Insert(i, order);

                    return i;
                }
            }

            this.items.Add(item);
            this.orders.Add(order);

            return this.items.Count - 1;
        }

        public bool Remove(T item)
        {
            var index = this.items.IndexOf(item);
            if(index < 0)
            {
                return false;
            }

            this.items.RemoveAt(index);
            this.orders.RemoveAt(index);

            return true;
        }

        public void Clear()
        {
            this.items.Clear();
            this.orders.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(var i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static explicit operator List<T>(SortedList<T> sortedList)
        {
            return new List<T>(sortedList.items);
        }
    }
}
