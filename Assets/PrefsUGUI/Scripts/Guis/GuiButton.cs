using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis
{
    [AddComponentMenu("")]
    public class GuiButton : GuiBase
    {
        [SerializeField]
        protected Button button = null;
        [SerializeField]
        protected Text label = null;

        protected UnityAction callback = null;


        public void Initialize(string label, UnityAction callback)
        {
            this.SetLabel(label);
            this.SetValue(callback);
        }

        public void SetValue(UnityAction callback)
        {
            if(this.callback != null)
            {
                this.button.onClick.RemoveListener(this.callback);
            }

            this.callback = callback;
            this.button.onClick.AddListener(this.callback);
        }

        public override void SetLabel(string label) => this.label.text = label;

        public override string GetLabel() => this.label.text;

        public override void SetValue(object value)
        {
            if(value is UnityAction == false)
            {
                return;
            }

            this.SetValue((UnityAction)value);
        }

        public override object GetValueObject() => this.callback;

        protected virtual void Reset()
        {
            this.button = GetComponentInChildren<Button>();
            this.label = GetComponentInChildren<Text>();
        }
    }
}
