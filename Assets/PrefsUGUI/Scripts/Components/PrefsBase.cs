using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Factories;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsBase
        {
            public virtual event Action OnValueChanged = delegate { };

            public string SaveKey
            {
                get
                {
                    var sep = HierarchySeparator;
                    var hierarchy = this.GuiHierarchy == "" ? "" : this.GuiHierarchy.TrimEnd(sep) + sep;

                    return hierarchy + this.Key;
                }
            }
            public string Key
            {
                get { return this.key; }
            }
            public string GuiHierarchy
            {
                get { return this.guiHierarchy; }
            }
            public string GuiLabel
            {
                get { return this.guiLabel; }
            }

            public abstract object ValueAsObject { get; set; }
            public abstract object DefaultValueAsObject { get; }
            public abstract Type ValueType { get; }

            [SerializeField]
            protected string key = "";
            [SerializeField]
            protected string guiHierarchy = "";
            [SerializeField]
            protected string guiLabel = "";


            public PrefsBase(string key, string guiHierarchy = "", string guiLabel = "")
            {
                this.key = key;
                this.guiHierarchy = guiHierarchy;
                this.guiLabel = string.IsNullOrEmpty(guiLabel) == true ? key : guiLabel;

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
