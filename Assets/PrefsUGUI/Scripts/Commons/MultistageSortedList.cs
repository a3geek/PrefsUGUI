using System;
using System.Collections;
using System.Collections.Generic;

namespace PrefsUGUI.Commons
{
    public sealed class MultistageSortedList<T> : SortedList<T>, IEnumerable<T> where T : class
    {
        private static readonly Func<T, T, int> DefaultSorter = (e1, e2) => 0;

        private Func<T, T, int> sorter = null;


        public MultistageSortedList() : this(DefaultSorter)
        {
        }

        public MultistageSortedList(Func<T, T, int> equalSorter)
        {
            this.sorter = equalSorter ?? DefaultSorter;
        }

        public override int Add(T item, int order)
        {
            for (var i = 0; i < this.list.Count; i++)
            {
                if (this.list[i].order == order)
                {
                    var e = this.list[i].item;
                    var sorted = this.sorter(e, item);

                    this.list[i] = (sorted <= 0 ? e : item, order);
                    item = sorted <= 0 ? item : e;
                }

                if (this.list[i].order > order)
                {
                    this.list.Insert(i, (item, order));
                    return i;
                }
            }

            this.list.Add((item, order));
            return this.list.Count - 1;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public static explicit operator List<T>(MultistageSortedList<T> sortedList)
            => new List<T>(sortedList.Items);
    }
}
