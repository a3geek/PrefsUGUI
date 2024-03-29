﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Factories.Classes;
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiButton : PrefsGuiBase, IPrefsGuiConnector<UnityAction, PrefsGuiButton>
    {
        public virtual event Action OnClicked = delegate { };

        public virtual PrefsGuiButton Component => this;

        [SerializeField]
        protected Button button = null;

        protected UnityAction callback = null;


        protected override void Reset()
        {
            base.Reset();
            this.button = this.GetComponentInChildren<Button>();
        }

        public virtual void Initialize(string label, UnityAction callback)
        {
            this.SetLabel(label);

            this.button.onClick.AddListener(this.FireOnClicked);
            this.SetValue(callback);
        }

        public virtual void SetValue(UnityAction callback)
        {
            if(callback == null)
            {
                return;
            }
            if(this.callback != null)
            {
                this.button.onClick.RemoveListener(this.callback);
            }

            this.callback = callback;
            this.button.onClick.AddListener(this.callback);
        }

        public virtual void ManualClick(bool withCallback = false)
        {
            this.FireOnClicked();
            if(withCallback == true)
            {
                this.callback?.Invoke();
            }
        }

        public virtual UnityAction GetValue()
            => this.callback;

        public virtual void SetGuiListeners(PrefsValueBase<UnityAction> prefs, IPrefsGuiEvents<UnityAction, PrefsGuiButton> prefsEventer, AbstractGuiHierarchy hierarchy)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            this.button.onClick.RemoveAllListeners();
            this.callback = null;
        }

        protected virtual void FireOnClicked()
            => this.OnClicked();
    }
}
