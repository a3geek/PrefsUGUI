using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    public abstract class PrefsGuiBase : GuiBase
    {
        public event Action OnValueChanged = delegate { };

        [SerializeField]
        protected Text label = null;
        [SerializeField]
        protected LayoutElement layout = null;
        [SerializeField]
        protected RectTransform elements = null;


        public override void SetLabel(string label) => this.label.text = label;

        public override string GetLabel() => this.label.text;

        public virtual void SetBottomMargin(float value)
            => this.layout.minHeight = this.elements.sizeDelta.y + Mathf.Max(0f, value);

        public virtual float GetBottomMargin() => this.layout.minHeight - this.elements.sizeDelta.y;

        public virtual void SetGuiListeners(PrefsBase prefs, bool withoutInitialize = false)
        {
            this.OnValueChanged = withoutInitialize == false ? this.OnValueChanged : delegate { };
            this.OnValueChanged += () => prefs.ValueAsObject = this.GetValueObject();
        }

        protected virtual void FireOnValueChanged() => this.OnValueChanged();

        protected virtual void Reset()
        {
            this.elements = GetComponentInChildren<RectTransform>();
            this.label = GetComponentInChildren<Text>();
        }
    }
}
