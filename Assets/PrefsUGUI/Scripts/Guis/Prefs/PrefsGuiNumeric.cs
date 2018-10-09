using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiNumeric : InputGuiBase
    {
        public virtual bool IsDecimalNumber
        {
            get { return this.isDecimalNumber; }
        }

        [SerializeField]
        protected bool isDecimalNumber = true;
        [SerializeField]
        protected InputField field = null;

        protected float fvalue = 0f;
        protected int ivalue = 0;
        protected Func<float> fdefaultGetter = null;
        protected Func<int> idefaultGetter = null;


        public virtual float GetFloatValue()
        {
            return this.fvalue;
        }

        public virtual int GetIntValue()
        {
            return this.ivalue;
        }

        public virtual void SetValue(int value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public virtual void SetValue(float value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public virtual void Initialize(string label, float initialValue, Func<float> fdefaultGetter)
        {
            this.Initialize(label, true);
            this.fdefaultGetter = fdefaultGetter;
            this.SetValue(initialValue);
        }

        public virtual void Initialize(string label, int initialValue, Func<int> idefaultGetter)
        {
            this.Initialize(label, false);
            this.idefaultGetter = idefaultGetter;
            this.SetValue(initialValue);
        }

        public override void SetValue(object value)
        {
            var isDecimal = this.IsDecimalNumber;
            var isFloat = value is float;
            var isInt = value is int;

            if(isDecimal == true && isFloat == true)
            {
                this.SetValue((float)value);
            }
            else if(isDecimal == false && isInt == true)
            {
                this.SetValue((int)value);
            }
        }

        public override object GetValueObject()
        {
            return this.IsDecimalNumber == true ? (object)this.GetFloatValue() : this.GetIntValue();
        }
        
        protected override bool IsDefaultValue()
        {
            return this.IsDecimalNumber == true ?
                this.GetFloatValue() == this.fdefaultGetter() :
                this.GetIntValue() == this.idefaultGetter();
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.field.text = this.IsDecimalNumber == true ?
                this.fvalue.ToString() : this.ivalue.ToString();
        }

        protected override UnityEvent<string>[] GetInputEvents()
        {
            return new UnityEvent<string>[] { this.field.onEndEdit };
        }

        protected virtual void Initialize(string label, bool isDecimalNumber)
        {
            this.SetLabel(label);

            this.isDecimalNumber = isDecimalNumber;
            this.field.contentType = isDecimalNumber == true ?
                InputField.ContentType.DecimalNumber : InputField.ContentType.IntegerNumber;
        }

        protected virtual void SetValueInternal(float value)
        {
            this.fvalue = value;
        }

        protected virtual void SetValueInternal(int value)
        {
            this.ivalue = value;
        }

        protected override void SetValueInternal(string value)
        {
            if(this.IsDecimalNumber == true)
            {
                float.TryParse(value, out this.fvalue);
            }
            else
            {
                int.TryParse(value, out this.ivalue);
            }
        }

        protected override void Reset()
        {
            base.Reset();
            this.field = GetComponentInChildren<InputField>();
        }
    }
}
