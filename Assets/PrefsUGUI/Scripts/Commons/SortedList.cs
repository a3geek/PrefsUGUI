using System;
using System.Collections;
using System.Collections.Generic;

namespace PrefsUGUI.Commons
{
    public class SortedList<T> : IEnumerable<T> where T : class
    {
        public virtual T this[int index]
        {
            get => this.list[index].item;
        }

        public virtual int Count => this.list.Count;
        public virtual IReadOnlyList<T> Items => this.list.ConvertAll(t => t.item);
        public virtual IReadOnlyList<int> Orders => this.list.ConvertAll(t => t.order);

        protected List<(T item, int order)> list = new List<(T item, int order)>();


        public virtual int Add(T item, int order)
        {
            for(var i = 0; i < this.list.Count; i++)
            {
                if(this.list[i].order > order)
                {
                    this.list.Insert(i, (item, order));
                    return i;
                }
            }

            this.list.Add((item, order));
            return this.list.Count - 1;
        }

        public virtual bool Remove(T item)
        {
            for (var i = 0; i < this.list.Count; i++)
            {
                if (this.list[i].item == item)
                {
                    this.list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public virtual void Clear()
            => this.list.Clear();

        public virtual IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Count; i++)
            {
                yield return this.list[i].item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public static explicit operator List<T>(SortedList<T> sortedList)
            => new List<T>(sortedList.Items);
    }
}
