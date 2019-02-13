using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis
{
    [DisallowMultipleComponent]
    public abstract class GuiBase : MonoBehaviour
    {
        public abstract void SetLabel(string label);
        public abstract void SetValue(object value);
        public abstract string GetLabel();
        public abstract object GetValueObject();
    }

    public abstract class InputGuiBase : GuiBase
    {
        public event Action OnPressedDefaultButton = delegate { };
        public event Action OnValueChanged = delegate { };

        public virtual float BottomMargin
        {
            get { return this.layout.minHeight - this.elements.sizeDelta.y; }
            set { this.layout.minHeight = this.elements.sizeDelta.y + Mathf.Max(0f, value); }
        }

        [SerializeField]
        protected LayoutElement layout = null;
        [SerializeField]
        protected RectTransform elements = null;
        [SerializeField]
        protected Text label = null;
        [SerializeField]
        protected Button defaultButton = null;
        [SerializeField]
        protected Text defaultButtonText = null;
        [SerializeField]
        private Color defaultColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        [SerializeField]
        private Color undefaultColor = Color.red;


        protected virtual void Awake()
        {
            this.defaultButton.onClick.AddListener(this.OnDefaultButton);

            var events = this.GetInputEvents();
            for(var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnInputValue);
            }
        }
        
        public override void SetLabel(string label)
        {
            this.label.text = label;
        }

        public override string GetLabel()
        {
            return this.label.text;
        }

        protected virtual void OnDefaultButton()
        {
            this.OnPressedDefaultButton();
        }

        protected virtual void OnInputValue(string v)
        {
            this.SetValueInternal(v);
            this.FireOnValueChanged();
        }

        protected virtual void FireOnValueChanged()
        {
            this.OnValueChanged();
        }

        protected virtual void SetFields()
        {
            this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.undefaultColor;
        }

        protected abstract UnityEvent<string>[] GetInputEvents();
        protected abstract bool IsDefaultValue();
        protected abstract void SetValueInternal(string value);

        protected virtual void Reset()
        {
            this.elements = GetComponentInChildren<RectTransform>();
            this.label = GetComponentInChildren<Text>();

            this.defaultButton = GetComponentInChildren<Button>();
            this.defaultButtonText = this.defaultButton?.GetComponentInChildren<Text>();
        }
    }
}
