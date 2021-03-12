using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiString : TextInputGuiBase<string, PrefsGuiString>
    {
        public override PrefsGuiString Component => this;

        [SerializeField]
        protected InputField field = null;


        protected override void Reset()
        {
            base.Reset();
            this.field = this.GetComponentInChildren<InputField>();
        }

        public virtual void Initialize(string label, string initialValue)
        {
            this.SetLabel(label);
            this.SetValue(initialValue);
        }

        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();
            this.field.text = this.value;
        }

        protected override void SetValueInternal(string value)
            => this.value = value;

        protected override UnityEvent<string>[] GetInputEvents()
            => new UnityEvent<string>[] { this.field.onEndEdit };
    }
}
