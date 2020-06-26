using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    public abstract class PrefsInputGuiBase<ValType, GuiType> : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        public event Action OnDefaultButtonClicked = delegate { };

        public abstract GuiType Component { get; }

        [SerializeField]
        protected Button defaultButton = null;
        [SerializeField]
        protected Text defaultButtonText = null;
        [SerializeField]
        private Color defaultColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        [SerializeField]
        private Color nondefaultColor = Color.red;

        protected ValType value = default;
        protected Func<ValType> defaultGetter = null;


        protected virtual void Awake()
        {
            void onDefaultButtonClicked() => this.OnDefaultButtonClicked.Invoke();
            this.defaultButton.onClick.AddListener(onDefaultButtonClicked);
        }

        protected override void Reset()
        {
            base.Reset();
            this.defaultButton = this.GetComponentInChildren<Button>();
            this.defaultButtonText = this.defaultButton == null ? null : this.defaultButton.GetComponentInChildren<Text>();
        }

        public virtual ValType GetValue()
            => this.value;

        public virtual void SetValue(ValType value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public virtual void SetGuiListeners(PrefsValueBase<ValType> prefs)
        {
            void onValueChanged() => prefs.Set(this.GetValue());
            void onDefaultButtonClicked() => prefs.ResetDefaultValue();

            this.OnValueChanged += onValueChanged;
            this.OnDefaultButtonClicked += onDefaultButtonClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.OnDefaultButtonClicked = null;
            this.defaultGetter = null;
        }

        protected virtual void SetValueInternal(ValType value)
            => this.value = value;

        protected virtual void SetFields()
            => this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.nondefaultColor;

        protected abstract bool IsDefaultValue();
    }
}
