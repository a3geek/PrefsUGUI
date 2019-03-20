using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;
    using XmlStorage;

    public static partial class Prefs
    {
        public abstract class PrefsValueBase<ValType> : PrefsBase
        {
            public virtual ValType Value
            {
                get { return this.Get(); }
                set { this.Set(value); }
            }
            public virtual ValType DefaultValue => this.defaultValue;

            public override object ValueAsObject => this.Get();
            public override object DefaultValueAsObject => this.defaultValue;
            public override Type ValueType => typeof(ValType);

            [SerializeField]
            protected ValType defaultValue = default(ValType);

            protected bool got = false;
            protected ValType value = default(ValType);


            public PrefsValueBase(string key, ValType defaultValue = default(ValType), GuiHierarchy hierarchy = null, string guiLabel = "")
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

            public void Set(ValType value, bool withEvent = true)
                => this.SetValueInternal(value, withEvent);

            public override void ResetDefaultValue()
                => this.Set(this.DefaultValue);

            public override void Reload(bool withEvent = true)
                => this.SetValueInternal(Storage.Get(this.SaveKey, this.defaultValue, AggregationName), withEvent);

            protected virtual void SetValueInternal(ValType value, bool withEvent = true)
            {
                this.got = true;
                this.value = value;

                if(withEvent == true)
                {
                    this.FireOnValueChanged();
                }
            }
        }
    }
}
