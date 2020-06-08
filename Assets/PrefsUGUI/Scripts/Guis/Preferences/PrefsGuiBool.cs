using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiBool : PrefsInputGuiBase<bool, PrefsGuiBool>
    {
        public override PrefsGuiBool Component => this;

        [SerializeField]
        protected Toggle toggle = null;


        protected override void Awake()
        {
            base.Awake();
            this.toggle.onValueChanged.AddListener(this.OnToggleChanged);
        }

        protected override void Reset()
        {
            base.Reset();
            this.toggle = this.GetComponentInChildren<Toggle>();
        }

        public virtual void Initialize(string label, bool initialValue, Func<bool> defaultGetter)
        {
            this.defaultGetter = defaultGetter;

            this.SetLabel(label);
            this.SetValue(initialValue);
        }

        protected virtual void OnToggleChanged(bool value)
        {
            this.SetValueInternal(value);
            this.FireOnValueChanged();
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.toggle.isOn = this.GetValue();
        }

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();
    }
}
