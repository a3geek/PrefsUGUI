using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Utilities;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsBase : IDisposable
        {
            public virtual event Action OnValueChanged = delegate { };

            public virtual string SaveKey => (this.GuiHierarchy == null ? "" : this.GuiHierarchy.Hierarchy) + this.key;
            public virtual string Key => this.key;
            public virtual GuiHierarchy GuiHierarchy => this.hierarchy;
            public virtual string GuiLabel => this.guiLabel;

            public virtual bool Unsave { get; set; }

            public abstract Type ValueType { get; }
            public abstract object DefaultValueAsObject { get; }
            public abstract object ValueAsObject { get; }

            [SerializeField]
            protected string key = "";
            [SerializeField]
            protected string guiLabel = "";

            protected GuiHierarchy hierarchy = null;


            public PrefsBase(string key, GuiHierarchy hierarchy = null, string guiLabel = "")
            {
                this.key = key;
                this.hierarchy = hierarchy;
                this.guiLabel = string.IsNullOrEmpty(guiLabel) == true ? key.ToLabelable() : guiLabel;

                this.Unsave = false;
                this.Regist();
            }

            ~PrefsBase()
            {
                this.Dispose();
            }

            public void Dispose()
            {
                RemovePrefs(this);
            }

            public abstract void ResetDefaultValue();
            public abstract void Reload(bool withEvent = true);


            protected virtual void Regist()
            {
                PrefsInstances.Add(this);
                this.AfterRegist();
            }

            protected virtual void FireOnValueChanged()
            {
                this.OnValueChanged();
            }

            protected abstract void AfterRegist();
        }
    }
}
