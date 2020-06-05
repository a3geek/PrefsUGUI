using System;
using UnityEngine;

namespace PrefsUGUI
{
    using XmlStorage;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsValueBase<ValType> : PrefsBase
        {
            public virtual ValType Value
            {
                get => this.Get();
                set => this.Set(value);
            }
            public virtual ValType DefaultValue => this.defaultValue;

            [SerializeField]
            protected ValType defaultValue = default;

            protected bool got = false;
            protected ValType value = default;


            public PrefsValueBase(string key, ValType defaultValue = default, GuiHierarchy hierarchy = null, string guiLabel = null)
                : base(key, hierarchy, guiLabel)
            {
                this.value = defaultValue;
                this.defaultValue = defaultValue;
            }

            public ValType Get()
            {
                if (this.got == false)
                {
                    this.Reload(false);
                }

                return this.value;
            }

            public ValType GetDefaultValue()
                => this.DefaultValue;

            public override void ResetDefaultValue()
                => this.Set(this.DefaultValue);

            public void Set(ValType value, bool withEvent = true)
                => this.SetValueInternal(value, withEvent);

            public override void Reload(bool withEvent = true)
                => this.SetValueInternal(Storage.Get(this.SaveKey, this.DefaultValue, AggregationName), withEvent);

            protected virtual void SetValueInternal(ValType value, bool withEvent = true)
            {
                this.got = true;
                this.value = value;

                if (withEvent == true)
                {
                    this.FireOnValueChanged();
                }
            }

            protected override void ValueSetToStorage()
                => Storage.Set(typeof(ValType), this.SaveKey, this.Get(), AggregationName);
        }
    }
}
