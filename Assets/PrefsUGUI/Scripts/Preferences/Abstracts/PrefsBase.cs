using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using CustomExtensions.Csharp;
    using Managers;
    using Managers.Classes;

    [Serializable]
    public abstract partial class PrefsBase : IDisposable
    {
        public virtual event Action OnValueChanged = delegate { };
        public virtual event Action OnEditedInGui = delegate { };
        public virtual event Action OnDisposed = delegate { };

        public virtual Guid PrefsId { get; } = Guid.Empty;
        public virtual string SaveKey => (this.GuiHierarchy?.SaveKeyPath ?? "") + this.key;
        public virtual string Key => this.key;
        public virtual bool Unsave { get; set; } = false;
        public virtual bool UnEditSync { get; set; } = false;
        public virtual Hierarchy GuiHierarchy { get; protected set; } = null;
        public abstract string GuiLabel { get; set; }
        public abstract bool IsCreatedGui { get; }
        public abstract int GuiSortOrder { get; protected set; }

        [SerializeField]
        protected string key = "";

        protected bool disposed = false;
        protected IPrefsStorageSetter storageSetter = null;


        public PrefsBase(string key, Hierarchy hierarchy = null)
        {
            this.key = key;
            this.GuiHierarchy = hierarchy;
            this.PrefsId = Guid.NewGuid();
            this.storageSetter = new StorageSetter(this);

            this.Regist();
        }

        public abstract void ResetDefaultValue();
        public abstract void Reload();

        protected virtual void Regist()
        {
            PrefsManager.AddStorageSetter(this.SaveKey, this.storageSetter);
            this.OnRegisted();
        }

        protected virtual void SetStorageValue()
        {
            if(this.Unsave == false && this.IsCreatedGui == true)
            {
                this.ValueSetToStorage();
            }
        }

        protected virtual void FireOnEditedInGui()
        {
            this.OnEditedInGui();
            PrefsManager.PrefsEditedEventer.OnAnyPrefsEditedInGui(this);
        }

        protected virtual void FireOnValueChanged()
            => this.OnValueChanged();

        protected abstract void ValueSetToStorage();
        protected abstract void OnRegisted();

        #region IDisposable Support
        ~PrefsBase()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(this.disposed == true)
            {
                return;
            }

            this.DisposeInternal(disposing);
        }

        protected virtual void DisposeInternal(bool disposing)
        {
            this.OnDisposed();
            PrefsManager.RemovePrefs(this.PrefsId);

            this.OnValueChanged = null;
            this.OnDisposed = null;
            this.GuiHierarchy = null;

            this.disposed = true;
        }
        #endregion
    }
}
