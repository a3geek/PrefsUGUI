using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using XmlStorage;
    using Guis.Preferences;

    [Serializable]
    public abstract partial class PrefsValueBase<ValType> : PrefsBase
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


        public PrefsValueBase(string key, ValType defaultValue = default, Hierarchy hierarchy = null)
            : base(key, hierarchy)
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

        public virtual void Set(ValType value)
            => this.SetValueInternal(value, true);

        public override void Reload()
            => this.Reload(true);

        protected virtual void Reload(bool withEvent)
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

        protected virtual void OnEditedInGuiInternal(ValType value)
        {
            this.SetValueInternal(value);
            this.FireOnEditedInGui();
        }

        protected virtual void OnClickedDefaultButton()
        {
            this.ResetDefaultValue();
            this.FireOnEditedInGui();
        }

        protected virtual void OnClickedDiscardButton()
        {
            this.Reload();
            this.FireOnEditedInGui();
        }

        protected override void ValueSetToStorage()
            => Storage.Set(typeof(ValType), this.SaveKey, this.Get(), Prefs.AggregationName);
    }
}
