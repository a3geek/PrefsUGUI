using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Factories.Classes;
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    public abstract class PrefsInputGuiBase<ValType, GuiType> : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        public abstract GuiType Component { get; }

        [SerializeField]
        protected Button defaultButton = null;
        [SerializeField]
        protected Text defaultButtonText = null;
        [SerializeField]
        protected Color defaultColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        [SerializeField]
        protected Color nondefaultColor = Color.red;

        protected bool isUpdatingFields = false;
        protected ValType value = default;
        protected IPrefsGuiEvents<ValType, GuiType> prefsEvents = null;
        protected AbstractGuiHierarchy hierarchy = null;
        

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
            this.SetFields(false);
        }

        public virtual void SetGuiListeners(PrefsValueBase<ValType> prefs, IPrefsGuiEvents<ValType, GuiType> prefsEvents, AbstractGuiHierarchy hierarchy)
        {
            this.prefsEvents = prefsEvents;
            this.hierarchy = hierarchy;

            this.hierarchy.OnDiscard += this.prefsEvents.OnClickedDiscardButton;
            this.defaultButton.onClick.AddListener(prefsEvents.OnClickedDefaultButton);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.hierarchy.OnDiscard -= this.prefsEvents.OnClickedDiscardButton;
            this.defaultButton.onClick.RemoveAllListeners();
        }

        protected virtual void SetFields(bool withEvent = true)
        {
            if(this.isUpdatingFields == true)
            {
                return;
            }

            this.isUpdatingFields = true;

            this.SetFieldsInternal();
            if(withEvent == true)
            {
                this.prefsEvents.OnEditedInGui(this.GetValue());
            }

            this.isUpdatingFields = false;
        }

        protected virtual void SetValueInternal(ValType value)
            => this.value = value;

        protected virtual void SetFieldsInternal()
            => this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.nondefaultColor;

        protected abstract bool IsDefaultValue();
    }
}
