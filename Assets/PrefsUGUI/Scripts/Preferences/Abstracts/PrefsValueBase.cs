using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using XmlStorage;

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

        public virtual ValType Get()
        {
            if(this.got == false)
            {
                this.Reload(false);
            }

            return this.value;
        }

        public virtual ValType GetDefaultValue()
            => this.DefaultValue;

        public override void ResetDefaultValue()
            => this.Set(this.DefaultValue);

        public virtual void Set(ValType value, bool withEvent = true)
            => this.SetValueInternal(value, withEvent);

        public override void Reload()
            => this.Reload(true);

        public override void Reload(bool withEvent)
            => this.SetValueInternal(Storage.Get(this.SaveKey, this.DefaultValue, Prefs.AggregationName), withEvent);

        protected virtual void SetValueInternal(ValType value, bool withEvent = true)
        {
            this.got = true;
            this.value = value;

            if(withEvent == true)
            {
                this.FireOnValueChanged();
            }
        }

        protected override void ValueSetToStorage()
            => Storage.Set(typeof(ValType), this.SaveKey, this.Get(), Prefs.AggregationName);
    }
}
