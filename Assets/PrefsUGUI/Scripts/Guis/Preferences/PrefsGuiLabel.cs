using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Factories.Classes;
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiLabel : PrefsGuiBase, IPrefsGuiConnector<string, PrefsGuiLabel>
    {
        public virtual PrefsGuiLabel Component => this;

        [SerializeField]
        protected InputField inputfield = null;


        protected override void Reset()
        {
            base.Reset();

            if(this.elements != null)
            {
                var image = this.elements.GetComponentInChildren<Image>();
                this.inputfield = image == null ? null : image.GetComponentInChildren<InputField>();
            }
        }

        public virtual void Initialize(string label, string text)
        {
            this.SetLabel(label);
            this.inputfield.text = text;
        }

        public virtual string GetValue()
            => this.inputfield.text;

        public virtual void SetValue(string text)
            => this.inputfield.text = text;

        public virtual void SetGuiListeners(PrefsValueBase<string> prefs, IPrefsGuiEvents<string, PrefsGuiLabel> prefsEventer, AbstractGuiHierarchy hierarchy)
        {
        }
    }
}
