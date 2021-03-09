using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using CustomExtensions.Csharp;
    using Managers;

    [Serializable]
    public abstract class PrefsBase : IDisposable
    {
        public virtual event Action OnValueChanged = delegate { };
        public virtual event Action OnEditedInGui = delegate { };
        public virtual event Action OnDisposed = delegate { };

        public virtual Guid PrefsId { get; } = Guid.Empty;
        public virtual string SaveKey => (this.GuiHierarchy?.FullHierarchy ?? "") + this.key;
        public virtual string Key => this.key;
        public virtual string GuiLabel => this.guiLabel;
        public virtual bool Unsave { get; set; } = false;
        public virtual GuiHierarchy GuiHierarchy { get; protected set; } = null;
        public abstract int GuiSortOrder { get; protected set; }

        [SerializeField]
        protected string key = "";
        [SerializeField]
        protected string guiLabel = "";

        protected bool disposed = false;


        public PrefsBase(string key, GuiHierarchy hierarchy = null, string guiLabel = null)
        {
            this.key = key;
            this.GuiHierarchy = hierarchy;
            this.guiLabel = guiLabel ?? key.ToLabelable();
            this.PrefsId = Guid.NewGuid();

            this.Regist();
        }

        public abstract void ResetDefaultValue();
        public abstract void Reload();
        public abstract void Reload(bool withEvent);

        protected virtual void Regist()
        {
            void ValueSetter()
            {
                Debug.Log(this.SaveKey);
                if (this.Unsave == false)
                {
                    this.ValueSetToStorage();
                }
            };
            PrefsManager.StorageValueSetters.Add(this.SaveKey, ValueSetter);

            this.OnRegisted();
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

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed == true)
            {
                return;
            }

            PrefsManager.RemovePrefs(this.PrefsId);
            this.OnDisposed();

            this.OnValueChanged = null;
            this.OnDisposed = null;
            this.GuiHierarchy = null;

            this.disposed = true;
        }
        #endregion
    }
}
