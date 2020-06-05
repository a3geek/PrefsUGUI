using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Prefs = PrefsUGUI.Prefs;

    [Serializable]
    public abstract class PrefsInputGuiBase<ValType> : PrefsGuiBase, IPrefsGuiConnector<ValType, PrefsInputGuiBase<ValType>>
    {
        public event Action OnDefaultButtonClicked = delegate { };

        public PrefsInputGuiBase<ValType> Component => this;

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

        public virtual void SetGuiListeners(Prefs.PrefsValueBase<ValType> prefs)
        {
            void onValueChanged() => prefs.Set(this.GetValue());
            void onDefaultButtonClicked() => prefs.ResetDefaultValue();

            this.OnValueChanged += onValueChanged;
            this.OnDefaultButtonClicked += onDefaultButtonClicked;
        }

        protected virtual void SetValueInternal(ValType value)
            => this.value = value;

        protected virtual void SetFields()
            => this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.nondefaultColor;

        protected abstract bool IsDefaultValue();
    }
}
