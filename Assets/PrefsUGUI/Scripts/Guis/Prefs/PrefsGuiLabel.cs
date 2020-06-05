using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Prefs = PrefsUGUI.Prefs;

    [Serializable]
    public class PrefsGuiLabel : PrefsGuiBase, IPrefsGuiConnector<string, PrefsGuiLabel>
    {
        public PrefsGuiLabel Component => this;

        [SerializeField]
        protected Text text = null;


        protected override void Reset()
        {
            base.Reset();

            if(this.elements != null)
            {
                var image = this.elements.GetComponentInChildren<Image>();
                this.text = image == null ? null : image.GetComponentInChildren<Text>();
            }
        }

        public virtual void Initialize(string label, string text)
        {
            this.SetLabel(label);
            this.text.text = text;
        }

        public virtual string GetValue()
            => this.text.text;

        public virtual void SetValue(string text)
            => this.text.text = text;

        public virtual void SetGuiListeners(Prefs.PrefsValueBase<string> prefs)
        {
        }
    }
}
