using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Prefs = PrefsUGUI.Prefs;
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    [DisallowMultipleComponent]
    public abstract class PrefsGuiBase : MonoBehaviour
    {
        public event Action OnValueChanged = delegate { };

        [SerializeField]
        protected Text label = null;
        [SerializeField]
        protected LayoutElement layout = null;
        [SerializeField]
        protected RectTransform elements = null;


        public virtual void SetBottomMargin(float value)
            => this.layout.minHeight += value;

        public virtual float GetBottomMargin()
            => this.layout.minHeight - (this.elements.sizeDelta.y + this.GetTopMargin());

        public virtual void SetTopMargin(float value)
        {
            var pos = this.elements.anchoredPosition;
            pos.y = -value;

            this.elements.anchoredPosition = pos;
            this.layout.minHeight += value;
        }

        public virtual float GetTopMargin()
            => -this.elements.anchoredPosition.y;

        protected virtual void FireOnValueChanged()
            => this.OnValueChanged();

        public virtual void SetLabel(string label)
            => this.label.text = label;

        public virtual string GetLabel()
            => this.label.text;

        protected virtual void SetListener(PrefsBase prefs, bool withoutInitialize = true)
            => this.OnValueChanged = withoutInitialize == true ? this.OnValueChanged : delegate { };

        public abstract object GetValueObject();

        protected virtual void Reset()
        {
            this.layout = GetComponentInChildren<LayoutElement>();
            this.elements = GetComponentInChildren<CanvasRenderer>()?.GetComponent<RectTransform>();
            this.label = GetComponentInChildren<Text>();
        }
    }
}
