using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiBool : PrefsInputGuiBase<bool, PrefsGuiBool>
    {
        public override PrefsGuiBool Component => this;

        [SerializeField]
        protected Toggle toggle = null;


        protected virtual void Awake()
        {
            this.toggle.onValueChanged.AddListener(this.OnToggleChanged);
        }

        protected override void Reset()
        {
            base.Reset();
            this.toggle = this.GetComponentInChildren<Toggle>();
        }

        public virtual void Initialize(string label, bool initialValue)
        {
            this.SetLabel(label);
            this.SetValue(initialValue);
        }

        protected virtual void OnToggleChanged(bool value)
        {
            this.SetValueInternal(value);
            this.SetFields(true);
        }

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();
            this.toggle.isOn = this.GetValue();
        }

        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();
    }
}
