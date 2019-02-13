using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Utilities;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsBase
        {
            public virtual event Action OnValueChanged = delegate { };

            public virtual string SaveKey => (this.GuiHierarchy == null ? "" : this.GuiHierarchy.Hierarchy) + this.key;
            public virtual string Key => this.key;
            public virtual GuiHierarchy GuiHierarchy => this.hierarchy;
            public virtual string GuiLabel => this.guiLabel;
            
            public abstract Type ValueType { get; }
            public abstract object DefaultValueAsObject { get; }
            public abstract object ValueAsObject { get; set; }

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

                this.Regist();
            }

            public abstract void ResetDefaultValue();
            public abstract void Reload(bool withEvent = true);
            
            protected virtual void Regist()
            {
                Data[this.SaveKey] = this;
            }

            protected virtual void FireOnValueChanged()
            {
                this.OnValueChanged();
            }
        }
    }
}
