using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;
    using XmlStorage;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsParam<ValType, GuiType> : PrefsBase where GuiType : InputGuiBase
        {
            public ValType Value
            {
                get { return this.Get(); }
            }
            public ValType DefaultValue
            {
                get { return this.defaultValue; }
            }

            public override object ValueAsObject
            {
                get { return this.Get(); }
                set
                {
                    if(value is ValType == false)
                    {
                        return;
                    }

                    this.Set((ValType)value);
                }
            }
            public override object DefaultValueAsObject
            {
                get { return this.defaultValue; }
            }
            public override Type ValueType
            {
                get { return typeof(ValType); }
            }

            [SerializeField]
            protected ValType defaultValue = default(ValType);

            protected bool got = false;
            protected ValType value = default(ValType);


            public PrefsParam(string key, ValType defaultValue = default(ValType), string guiHierarchy = "", string guiLabel = "")
                : base(key, guiHierarchy, guiLabel)
            {
                this.value = defaultValue;
                this.defaultValue = defaultValue;
            }

            ~PrefsParam()
            {
                RemovePrefs(this);
            }
            
            public ValType Get()
            {
                if(this.got == false)
                {
                    this.Reload(false);
                }

                return this.value;
            }

            public void Set(ValType value)
            {
                if(this.got == false)
                {
                    this.Get();
                }

                this.SetValueInternal(value);
            }

            public override void ResetDefaultValue()
            {
                this.Set(this.DefaultValue);
            }

            public override void Reload(bool withEvent = true)
            {
                this.SetValueInternal(Storage.Get(this.SaveKey, this.defaultValue, AggregationName), withEvent);
            }

            protected override void Regist()
            {
                base.Regist();
                AddPrefs<GuiType>(this, gui =>
                {
                    this.OnCreatedGui(gui);
                });
            }
            
            protected virtual void SetValueInternal(ValType value, bool withEvent = true)
            {
                this.got = true;
                this.value = value;
                
                if(withEvent == true)
                {
                    this.FireOnValueChanged();
                }
            }

            protected abstract void OnCreatedGui(GuiType gui);
        }
    }
}
