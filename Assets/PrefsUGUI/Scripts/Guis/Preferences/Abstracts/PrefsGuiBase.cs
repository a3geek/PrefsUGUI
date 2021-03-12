using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public abstract class PrefsGuiBase : MonoBehaviour, IPrefsGuiBase, IDisposable
    {
        //public event Action OnEditedInGui = delegate { };

        [SerializeField]
        protected Text label = null;
        [SerializeField]
        protected LayoutElement layout = null;
        [SerializeField]
        protected RectTransform elements = null;

        protected bool isDisposed = false;


        protected virtual void Reset()
        {
            this.label = this.GetComponentInChildren<Text>();
            this.layout = this.GetComponentInChildren<LayoutElement>();

            var canvas = this.GetComponentInChildren<CanvasRenderer>();
            this.elements = canvas == null ? null : canvas.GetComponent<RectTransform>();
        }

        protected virtual void OnDestroy()
        {
            if (this.isDisposed == false)
            {
                this.Dispose();
            }
        }

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

        public virtual void SetLabel(string label)
            => this.label.text = label;

        public virtual string GetLabel()
            => this.label.text;

        public virtual void SetVisible(bool visible)
            => this.gameObject.SetActive(visible);

        public virtual bool GetVisible()
            => this.gameObject.activeSelf;

        public virtual void Dispose()
        {
            this.isDisposed = true;
            //this.OnValueChanged = null;
        }

        //protected virtual void FireOnValueChanged()
        //    => this.OnValueChanged();
    }
}
