using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public abstract class NumericGuiBase<ValType, GuiType> : TextInputGuiBase<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        protected abstract bool IsDecimalNumber { get; }

        [SerializeField]
        protected InputField field = null;


        protected override void Reset()
        {
            base.Reset();
            this.field = this.GetComponentInChildren<InputField>();
        }

        public virtual void Initialize(string label, ValType initialValue, Func<ValType> defaultGetter)
        {
            this.SetLabel(label);
            this.field.contentType
                = this.IsDecimalNumber == true ? InputField.ContentType.DecimalNumber : InputField.ContentType.IntegerNumber;

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.field.text = this.GetValue().ToString();
        }

        protected override UnityEvent<string>[] GetInputEvents()
            => new UnityEvent<string>[] { this.field.onEndEdit };
    }
}
