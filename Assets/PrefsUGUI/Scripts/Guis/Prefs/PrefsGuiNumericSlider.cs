using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiNumericSlider : PrefsGuiNumeric
    {
        public float MinValue
        {
            get { return this.slider.minValue; }
        }
        public float MaxValue
        {
            get { return this.slider.maxValue; }
        }

        [SerializeField]
        protected Slider slider = null;


        public void OnChangedSlider(float v)
        {
            if(this.isDecimalNumber == true)
            {
                this.SetValue(v);
            }
            else
            {
                this.SetValue((int)v);
            }

            this.FireOnValueChanged();
        }

        public override void Initialize(string label, float initialValue, Func<float> fdefaultGetter)
        {
            this.Initialize(label, initialValue, -2f * initialValue, 2f * initialValue, fdefaultGetter);
        }

        public override void Initialize(string label, int initialValue, Func<int> idefaultGetter)
        {
            this.Initialize(label, initialValue, -2 * initialValue, 2 * initialValue, idefaultGetter);
        }
        
        public void Initialize(string label, float initialValue, float minValue, float maxValue, Func<float> fdefaultGetter)
        {
            this.InitializeSlider(minValue, maxValue, true);
            base.Initialize(label, initialValue, fdefaultGetter);
        }

        public void Initialize(string label, int initialValue, int minValue, int maxValue, Func<int> idefaultGetter)
        {
            this.InitializeSlider(minValue, maxValue, false);
            base.Initialize(label, initialValue, idefaultGetter);
        }

        protected virtual void InitializeSlider(float min, float max, bool isDecimal)
        {
            this.slider.wholeNumbers = !isDecimal;
            this.slider.minValue = min;
            this.slider.maxValue = max;
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.slider.value = this.IsDecimalNumber == true ? this.fvalue : this.ivalue;
        }

        protected override void Reset()
        {
            base.Reset();
            this.slider = GetComponentInChildren<Slider>();
        }
    }
}
