using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace PrefsUGUI.Utilities
{
    using CustomExtensions.Csharp;
    using PrefsUGUI;
    using PrefsUGUI.Managers;
    using XmlStorage;

    public partial class RemovablePrefsList<T> : IRemovablePrefsList<T>, IDisposable where T : IDisposable
    {
        private static readonly string SaveKeyPrefix = "GuiIndexs-";

        public string SaveKey => SaveKeyPrefix + this.saveKey;
        public int Count => this.ItemBags.Count;
        public IEnumerable<T> Items => this.ItemBags.Select(bag => bag.Item);
        public List<IItemBag<T>> ItemBags { get; private set; } = new List<IItemBag<T>>();
        public IReadOnlyPrefs<UnityAction> AddButton => this.addButton;

        private int defaultNum = 0;
        private string saveKey = "";
        private Func<int, T> creator = null;
        private PrefsButton addButton = null;
        private Action onCountChanged = null;
        private ItemBagListController listController = null;
        private RemovablePrefsListStorageSetter storageSetter = null;


        public RemovablePrefsList(string saveKey, PrefsButton addButton, Func<int, T> creator, int defaultNum = 0, Action onCountChanged = null)
        {
            this.saveKey = saveKey;
            this.creator = creator;
            this.addButton = addButton;
            this.onCountChanged = onCountChanged;
            this.listController = new ItemBagListController(this);
            this.storageSetter = new RemovablePrefsListStorageSetter(this);

            PrefsManager.AddStorageSetter(this.SaveKey, this.storageSetter);

            this.defaultNum = defaultNum;
            this.Reload();

            this.addButton.OnClicked += this.Add;
        }

        public void SetCount(int count)
            => this.ItemBags.SetCount(count, this.listController, this.listController);

        public void Add()
            => this.SetCount(this.Count + 1);

        public void Remove(int index)
            => this.Remove(index);

        public void Dispose()
        {
            PrefsManager.RemoveStorageSetter(this.SaveKey);

            for(var i = 0; i < this.ItemBags.Count; i++)
            {
                this.ItemBags[i].Item.Dispose();
            }
            this.addButton.Dispose();

            this.ItemBags.Clear();
            this.creator = null;
            this.onCountChanged = null;
        }

        private void RemoveItemBag(int index)
        {
            for(var i  = 0; i < this.ItemBags.Count; i++)
            {
                if(this.ItemBags[i].Index == index)
                {
                    this.ItemBags[i].Item.Dispose();
                    this.ItemBags.RemoveAt(i);
                    break;
                }
            }

            this.onCountChanged?.Invoke();
        }

        private void SetStorageValue()
            => Storage.Set(this.SaveKey, this.ItemBags.Select(tuple => tuple.Index).ToList());

        private void Reload()
            => Storage.Get(this.SaveKey, Enumerable.Range(0, defaultNum).ToList(), Prefs.AggregationName)
                .ForEach(idx => this.ItemBags.Add(new ItemBag(idx, this.creator(idx))));
    }
}
