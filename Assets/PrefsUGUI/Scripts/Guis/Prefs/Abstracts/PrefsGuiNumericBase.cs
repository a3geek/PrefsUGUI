using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public abstract class PrefsGuiNumericBase<ValType> : TextInputGuiBase<ValType>
    {
        protected abstract bool IsDecimalNumber { get; }

        [SerializeField]
        protected InputField field = null;


        public virtual void Initialize(string label, ValType initialValue, Func<ValType> defaultGetter)
        {
            this.SetLabel(label);
            this.field.contentType = this.IsDecimalNumber == true ?
                InputField.ContentType.DecimalNumber : InputField.ContentType.IntegerNumber;

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.field.text = this.GetValue().ToString();
        }

        protected override UnityEvent<string>[] GetInputEvents() => new UnityEvent<string>[] { this.field.onEndEdit };

        protected override void Reset()
        {
            base.Reset();
            this.field = GetComponentInChildren<InputField>();
        }
    }
}
