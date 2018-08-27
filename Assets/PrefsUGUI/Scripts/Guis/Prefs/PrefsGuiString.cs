using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiString : InputGuiBase
    {
        [SerializeField]
        protected InputField field = null;

        protected string value = "";
        protected Func<string> defaultGetter = null;


        public string GetValue()
        {
            return this.value;
        }

        public void SetValue(string value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }
        
        public override void SetValue(object value)
        {
            if(value is string == false)
            {
                return;
            }

            this.SetValue((string)value);
        }

        public void Initialize(string label, string initialValue, Func<string> defaultGetter)
        {
            this.SetLabel(label);
            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
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
            this.field.text = this.value;
        }

        protected override void SetValueInternal(string value)
        {
            this.value = value;
        }

        protected override void Reset()
        {
            base.Reset();
            this.field = GetComponentInChildren<InputField>();
        }
    }
}
