using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiNumericSlider : PrefsGuiNumeric
    {
        public float MinValue => this.slider.minValue;
        public float MaxValue => this.slider.maxValue;

        [SerializeField]
        protected Slider slider = null;

        protected bool inited = false;


        protected override void Awake()
        {
            base.Awake();
            this.slider.onValueChanged.AddListener(this.OnChangedSlider);
        }

        public override void Initialize(string label, float initialValue, Func<float> fdefaultGetter)
            => this.Initialize(label, initialValue, -2f * initialValue, 2f * initialValue, fdefaultGetter);

        public override void Initialize(string label, int initialValue, Func<int> idefaultGetter)
            => this.Initialize(label, initialValue, -2 * initialValue, 2 * initialValue, idefaultGetter);

        public void Initialize(string label, float initialValue, float minValue, float maxValue, Func<float> fdefaultGetter)
        {
            this.InitializeSlider(minValue, maxValue, true);
            base.Initialize(label, initialValue, fdefaultGetter);

            this.inited = true;
        }

        public void Initialize(string label, int initialValue, int minValue, int maxValue, Func<int> idefaultGetter)
        {
            this.InitializeSlider(minValue, maxValue, false);
            base.Initialize(label, initialValue, idefaultGetter);

            this.inited = true;
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.slider.value = this.IsDecimalNumber == true ? this.fvalue : this.ivalue;
        }

        protected virtual void InitializeSlider(float min, float max, bool isDecimal)
        {
            this.slider.wholeNumbers = !isDecimal;
            this.slider.minValue = (min > max ? max : min);
            this.slider.maxValue = (min < max ? max : min);
        }

        protected virtual void OnChangedSlider(float v)
        {
            if(this.inited == false)
            {
                return;
            }

            if(this.isDecimalNumber == true)
            {
                this.SetValue(v);
            }
            else
            {
                this.SetValue(Mathf.RoundToInt(v));
            }

            this.FireOnValueChanged();
        }

        protected override void Reset()
        {
            base.Reset();
            this.slider = GetComponentInChildren<Slider>();
        }
    }
}
