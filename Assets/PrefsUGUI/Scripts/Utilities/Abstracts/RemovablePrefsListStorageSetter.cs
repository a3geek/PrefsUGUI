using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace PrefsUGUI.Utilities
{
    using CustomExtensions.Csharp;
    using Managers.Classes;

    public interface IReadOnlyRemovableGuiList<T> where T : IDisposable
    {
        string SaveKey { get; }
        int Count { get; }
        List<IItemBag<T>> ItemBags { get; }
        IReadOnlyPrefs<UnityAction> AddButton { get; }
    }

    public interface IItemBag<T>
    {
        int Index { get; }
        T Item { get; }
    }

    public partial class RemovablePrefsList<T>
    {
        private class ItemBag : IItemBag<T>
        {
            public int Index { get; set; }
            public T Item { get; set; }

            public ItemBag(int index, T item)
            {
                this.Index = index;
                this.Item = item;
            }
        }

        private class ItemBagListController : IListCreator<IItemBag<T>>, IListDestoryer<IItemBag<T>>
        {
            private RemovablePrefsList<T> parent = null;


            public ItemBagListController(RemovablePrefsList<T> parent)
            {
                this.parent = parent;
            }

            public IItemBag<T> Create(int index)
            {
                var idx = this.parent.Count <= 0 ? 0 : this.parent.ItemBags.Max(itemBag => itemBag.Index) + 1;
                return new ItemBag(idx, this.parent.creator(idx));
            }

            public void Destroy(int index, IItemBag<T> item)
                => this.parent.RemoveItemBag(item.Index);
        }

        private class RemovablePrefsListStorageSetter : IPrefsStorageSetter
        {
            private RemovablePrefsList<T> parent = null;


            public RemovablePrefsListStorageSetter(RemovablePrefsList<T> parent)
            {
                this.parent = parent;
            }

            public void SetStorageValue()
                => this.parent.SetStorageValue();
        }
    }
}
