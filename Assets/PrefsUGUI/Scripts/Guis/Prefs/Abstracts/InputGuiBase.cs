using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    public abstract class InputGuiBase : PrefsGuiBase
    {
        public event Action OnPressedDefaultButton = delegate { };

        [SerializeField]
        protected Button defaultButton = null;
        [SerializeField]
        protected Text defaultButtonText = null;
        [SerializeField]
        private Color defaultColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        [SerializeField]
        private Color undefaultColor = Color.red;


        protected virtual void Awake()
        {
            this.defaultButton.onClick.AddListener(this.OnDefaultButton);

            var events = this.GetInputEvents();
            for(var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnInputValue);
            }
        }

        public override void SetGuiListeners(PrefsBase prefs, bool withoutInitialize = false)
        {
            base.SetGuiListeners(prefs, withoutInitialize);

            this.OnPressedDefaultButton = withoutInitialize == false ? this.OnPressedDefaultButton : delegate { };
            this.OnPressedDefaultButton += () => prefs.ResetDefaultValue();
        }

        protected virtual void OnDefaultButton() => this.OnPressedDefaultButton();

        protected virtual void OnInputValue(string v)
        {
            this.SetValueInternal(v);
            this.FireOnValueChanged();
        }

        protected virtual void SetFields()
            => this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.undefaultColor;

        protected abstract UnityEvent<string>[] GetInputEvents();
        protected abstract bool IsDefaultValue();
        protected abstract void SetValueInternal(string value);

        protected override void Reset()
        {
            base.Reset();
            this.defaultButton = GetComponentInChildren<Button>();
            this.defaultButtonText = this.defaultButton?.GetComponentInChildren<Text>();
        }
    }
}
