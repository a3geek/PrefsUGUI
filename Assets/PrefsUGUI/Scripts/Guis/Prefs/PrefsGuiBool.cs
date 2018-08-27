using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiBool : InputGuiBase
    {
        [SerializeField]
        protected Toggle toggle = null;

        protected bool value = true;
        protected Func<bool> defaultGetter = null;


        public void OnToggleChanged(bool value)
        {
            this.SetValueInternal(value);
            this.FireOnValueChanged();
        }

        public bool GetValue()
        {
            return this.value;
        }

        public void SetValue(bool value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public void Initialize(string label, bool initialValue, Func<bool> defaultGetter)
        {
            this.defaultGetter = defaultGetter;

            this.SetLabel(label);
            this.SetValue(initialValue);
        }
        
        public override void SetValue(object value)
        {
            if(value is bool == false)
            {
                return;
            }

            this.SetValue((bool)value);
        }

        public override object GetValueObject()
        {
            return this.GetValue();
        }

        protected override bool IsDefaultValue()
        {
            return this.GetValue() == this.defaultGetter();
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.toggle.isOn = this.value;
        }

        protected virtual void SetValueInternal(bool value)
        {
            this.value = value;
        }

        protected override void SetValueInternal(string value)
        {
            bool.TryParse(value, out this.value);
        }

        protected override void Reset()
        {
            base.Reset();
            this.toggle = GetComponentInChildren<Toggle>();
        }
    }
}
