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
        
        [SerializeField]
        protected Text label = null;
        [SerializeField]
        protected Text defaultButtonText = null;
        [SerializeField]
        private Color defaultColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        [SerializeField]
        private Color undefaultColor = Color.red;

        
        public virtual void OnDefaultButton()
        {
            this.OnPressedDefaultButton();
        }

        public virtual void OnInputValue(string v)
        {
            this.SetValueInternal(v);
            this.OnValueChanged();
        }
        
        public override void SetLabel(string label)
        {
            this.label.text = label;
        }

        public override string GetLabel()
        {
            return this.label.text;
        }
        
        protected virtual void FireOnValueChanged()
        {
            this.OnValueChanged();
        }

        protected virtual void SetFields()
        {
            this.defaultButtonText.color = this.IsDefaultValue() == true ? this.defaultColor : this.undefaultColor;
        }

        protected abstract bool IsDefaultValue();
        protected abstract void SetValueInternal(string value);

        protected virtual void Reset()
        {
            this.label = GetComponentInChildren<Text>();

            var defaultButton = GetComponentInChildren<Button>();
            this.defaultButtonText = defaultButton != null ? defaultButton.GetComponentInChildren<Text>() : null;
        }
    }
}
