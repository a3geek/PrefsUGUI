using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiLabel : PrefsGuiBase
    {
        [SerializeField]
        protected Text text = null;

        public virtual void Initialize(string label, string text)
        {
            this.SetLabel(label);
            this.text.text = text;
        }

        public void SetValue(string text)
            => this.text.text = text;

        public string GetValue(string text)
            => this.text.text = text;

        public override object GetValueObject()
            => this.text.text;

        protected override void Reset()
        {
            base.Reset();
            this.text = this.elements?.GetComponentInChildren<Image>()?.GetComponentInChildren<Text>();
        }
    }
}
