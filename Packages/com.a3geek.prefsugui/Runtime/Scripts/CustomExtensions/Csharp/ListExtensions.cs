using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.CustomExtensions.CSharp
{
    public interface IListCreator<out T>
    {
        public T Create(int index);
    }

    public interface IListDestroyer<in T>
    {
        public void Destroy(int index, T item);
    }

    public static class ListExtensions
    {
        public static void SetCount<T>(
            this List<T> items, int count, IListCreator<T> creator, IListDestroyer<T> destroyer
        )
        {
            if (items.Count == count)
            {
                return;
            }

            for (var i = count; i < items.Count; i++)
            {
                destroyer.Destroy(i, items[i]);
            }
            items.RemoveRange(Mathf.Min(count, items.Count), Mathf.Max(items.Count - count, 0));

            for (var i = items.Count; i < count; i++)
            {
                items.Add(creator.Create(i));
            }
        }
    }
}
