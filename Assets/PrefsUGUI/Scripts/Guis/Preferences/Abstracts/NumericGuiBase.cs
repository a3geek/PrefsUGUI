using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Commons;

    [Serializable]
    public abstract class NumericGuiBase<ValType, GuiType> : TextInputGuiBase<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        protected abstract bool IsDecimalNumber { get; }

        [SerializeField]
        protected NumericUpDownInput updown = new NumericUpDownInput();
        [SerializeField]
        protected InputField field = null;


        protected override void Reset()
        {
            base.Reset();

            if (this.IsDecimalNumber == false)
            {
                this.updown.UpDownStep = 1f;
                this.updown.StepDelay = 0.1f;
            }

            this.field = this.GetComponentInChildren<InputField>();
        }

        protected virtual void Update()
        {
            if(this.updown.Update(this.field, out var delta) == true)
            {
                this.OnInputValue(this.GetDeltaAddedValue(delta));
            }
        }

        public virtual void Initialize(string label, ValType initialValue, Func<ValType> defaultGetter)
        {
            this.SetLabel(label);
            this.field.contentType
                = this.IsDecimalNumber == true ? InputField.ContentType.DecimalNumber : InputField.ContentType.IntegerNumber;

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();
            this.field.text = this.GetValue().ToString();
        }

        protected override UnityEvent<string>[] GetInputEvents()
            => new UnityEvent<string>[] { this.field.onEndEdit };

        protected abstract ValType GetDeltaAddedValue(float delta);
    }
}
