using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiBool : InputGuiValueBase<bool>
    {
        [SerializeField]
        protected Toggle toggle = null;


        protected override void Awake()
        {
            base.Awake();
            this.toggle.onValueChanged.AddListener(this.OnToggleChanged);
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

        protected override bool IsDefaultValue() => this.GetValue() == this.defaultGetter();

        protected override void Reset()
        {
            base.Reset();
            this.toggle = GetComponentInChildren<Toggle>();
        }
    }
}
