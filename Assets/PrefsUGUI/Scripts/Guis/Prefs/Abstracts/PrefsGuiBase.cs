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
        protected LayoutElement layout = null;
        [SerializeField]
        protected RectTransform elements = null;


        public virtual void SetBottomMargin(float value)
            => this.layout.minHeight = this.elements.sizeDelta.y + Mathf.Max(0f, value);

        public virtual float GetBottomMargin()
            => this.layout.minHeight - this.elements.sizeDelta.y;

        protected virtual void FireOnValueChanged()
            => this.OnValueChanged();

        protected virtual void SetListener(PrefsBase prefs, bool withoutInitialize = true)
            => this.OnValueChanged = withoutInitialize == true ? this.OnValueChanged : delegate { };

        public abstract object GetValueObject();

        protected virtual void Reset()
        {
            this.layout = GetComponentInChildren<LayoutElement>();
            this.elements = GetComponentInChildren<RectTransform>();
        }
    }
}
