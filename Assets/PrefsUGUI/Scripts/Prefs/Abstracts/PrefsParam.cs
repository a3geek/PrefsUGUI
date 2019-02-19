using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;
    using XmlStorage;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsParam<ValType, GuiType> : PrefsGuiConnector<GuiType> where GuiType : PrefsGuiBase
        {
            public virtual ValType Value => this.Get();
            public virtual ValType DefaultValue => this.defaultValue;

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


            public PrefsParam(string key, ValType defaultValue = default(ValType), GuiHierarchy hierarchy = null, string guiLabel = "")
                : base(key, hierarchy, guiLabel)
            {
                this.value = defaultValue;
                this.defaultValue = defaultValue;
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

            protected virtual void SetValueInternal(ValType value, bool withEvent = true)
            {
                this.got = true;
                this.value = value;

                if(withEvent == true)
                {
                    this.FireOnValueChanged();
                }
            }

            public static implicit operator ValType(PrefsParam<ValType, GuiType> prefs)
            {
                return prefs.Get();
            }
        }
    }
}
