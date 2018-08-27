using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

        public override void SetLabel(string label)
        {
            this.label.text = label;
        }

        public override string GetLabel()
        {
            return this.label.text;
        }

        public override void SetValue(object value)
        {
            if(value is UnityAction == false)
            {
                return;
            }

            this.SetValue((UnityAction)value);
        }

        public override object GetValueObject()
        {
            return this.callback;
        }

        protected virtual void Reset()
        {
            this.button = GetComponentInChildren<Button>();
            this.label = GetComponentInChildren<Text>();
        }
    }
}
