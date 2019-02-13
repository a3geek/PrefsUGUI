using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiString : InputGuiBase
    {
        [SerializeField]
        protected InputField field = null;

        protected string value = "";
        protected Func<string> defaultGetter = null;


        public string GetValue() => this.value;

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

        public override object GetValueObject() => this.GetValue();

        protected override UnityEvent<string>[] GetInputEvents() => new UnityEvent<string>[] { this.field.onEndEdit };

        protected override bool IsDefaultValue() => this.GetValue() == this.defaultGetter();

        protected override void SetFields()
        {
            base.SetFields();
            this.field.text = this.value;
        }

        protected override void SetValueInternal(string value) => this.value = value;

        protected override void Reset()
        {
            base.Reset();
            this.field = GetComponentInChildren<InputField>();
        }
    }
}
