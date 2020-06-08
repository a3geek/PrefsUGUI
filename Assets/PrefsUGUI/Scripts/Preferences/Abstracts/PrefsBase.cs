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

            public virtual string SaveKey => (this.GuiHierarchy?.FullHierarchy ?? "") + this.key;
            public virtual string Key => this.key;
            public virtual string GuiLabel => this.guiLabel;
            public virtual bool Unsave { get; set; } = false;
            public virtual GuiHierarchy GuiHierarchy { get; protected set; } = null;

            [SerializeField]
            protected string key = "";
            [SerializeField]
            protected string guiLabel = "";


            public PrefsBase(string key, GuiHierarchy hierarchy = null, string guiLabel = null)
            {
                this.key = key;
                this.GuiHierarchy = hierarchy;
                this.guiLabel = guiLabel ?? key.ToLabelable();

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
                void ValueSetter()
                {
                    if(this.Unsave == false)
                    {
                        this.ValueSetToStorage();
                    }
                };
                ValueSetters.Add(ValueSetter);

                this.OnRegisted();
            }

            protected virtual void FireOnValueChanged()
                => this.OnValueChanged();

            protected abstract void ValueSetToStorage();
            protected abstract void OnRegisted();
        }
    }
}
