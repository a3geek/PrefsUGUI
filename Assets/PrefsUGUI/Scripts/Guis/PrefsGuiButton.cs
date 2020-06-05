using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis
{
    using Prefs;

    [Serializable]
    public class PrefsGuiButton : PrefsGuiBase, IPrefsGuiConnector<UnityAction, PrefsGuiButton>
    {
        public PrefsGuiButton Component => this;

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

            this.button.onClick.AddListener(this.FireOnValueChanged);
            this.SetValue(callback);
        }

        public virtual void SetValue(UnityAction callback)
        {
            if(this.callback != null)
            {
                this.button.onClick.RemoveListener(this.callback);
            }

            this.callback = callback;
            this.button.onClick.AddListener(this.callback);
        }

        public virtual UnityAction GetValue()
            => this.callback;

        public virtual void SetGuiListeners(PrefsUGUI.Prefs.PrefsValueBase<UnityAction> prefs)
        {
        }
    }
}
