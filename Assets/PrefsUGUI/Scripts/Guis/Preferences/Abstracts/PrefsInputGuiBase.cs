﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    public abstract class PrefsInputGuiBase<ValType, GuiType> : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        //public event Action OnDefaultButtonClicked = delegate { };

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
        //protected Func<ValType> defaultGetter = null;


        //protected virtual void Awake()
        //{
        //    //void onDefaultButtonClicked() => this.OnDefaultButtonClicked.Invoke();
        //    //this.defaultButton.onClick.AddListener(onDefaultButtonClicked);
        //}

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

        public virtual void SetGuiListeners(PrefsValueBase<ValType> prefs, IPrefsGuiEvents<ValType, GuiType> prefsEvents)
        {
            //void onValueChanged() => prefs.Set(this.GetValue());
            //void onDefaultButtonClicked() => prefs.ResetDefaultValue();

            //this.OnValueChanged += onValueChanged;
            //this.OnDefaultButtonClicked += onDefaultButtonClicked;

            this.prefsEvents = prefsEvents;
            this.defaultButton.onClick.AddListener(prefsEvents.OnClickedDefaultButton);
        }

        public override void Dispose()
        {
            base.Dispose();
            //this.OnDefaultButtonClicked = null;
            this.defaultButton.onClick.RemoveAllListeners();
            //this.defaultGetter = null;
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
                //this.FireOnValueChanged();
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
