using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Utilities;

    public abstract class PrefsGuiVectorSliderBase<T1> : PrefsGuiVectorBase<T1> where T1 : struct
    {
        protected abstract bool IsDecimalNumber { get; }
        protected Slider[] sliders => new Slider[] { this.sliderX, this.sliderY, this.sliderZ, this.sliderW };

        [SerializeField]
        protected Slider sliderX = null;
        [SerializeField]
        protected Slider sliderY = null;
        [SerializeField]
        protected Slider sliderZ = null;
        [SerializeField]
        protected Slider sliderW = null;

        protected bool inited = false;


        protected override void Awake()
        {
            base.Awake();

            var events = this.GetSliderEvents();
            for(var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnChangedSlider);
            }
        }

        public override void Initialize(string label, T1 initialValue, Func<T1> defaultGetter)
        {
            this.SetValueInternal(initialValue);

            var val = this.GetElementAsFloat(0);
            this.Initialize(label, initialValue, 0f, val + val, defaultGetter);
        }

        public virtual void Initialize(string label, T1 initialValue, float minValue, float maxValue, Func<T1> defaultGetter)
        {
            base.Initialize(label, initialValue, defaultGetter);
            this.Initialize(minValue, maxValue);

            this.inited = true;
        }

        protected virtual void Initialize(float min, float max)
        {
            var sliders = this.sliders;
            for(var i = 0; i < this.ElementCount; i++)
            {
                sliders[i].wholeNumbers = !this.IsDecimalNumber;
                sliders[i].minValue = (min > max ? max : min);
                sliders[i].maxValue = (min < max ? max : min);
            }
        }

        protected override void SetFields()
        {
            base.SetFields();

            var sliders = this.sliders;
            for(var i = 0; i < sliders.Length; i++)
            {
                sliders[i].value = this.GetElementAsFloat(i);
            }
        }

        protected virtual void OnChangedSlider(float v)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValue(this.GetValueFromSlider());
            this.FireOnValueChanged();
        }

        protected virtual UnityEvent<float>[] GetSliderEvents()
        {
            var sliders = this.sliders;
            var events = new UnityEvent<float>[this.ElementCount];

            for(var i = 0; i < this.ElementCount; i++)
            {
                events[i] = sliders[i].onValueChanged;
            }

            return events;
        }

        protected abstract T1 GetValueFromSlider();
        protected abstract float GetElementAsFloat(int index);

        protected override void Reset()
        {
            base.Reset();

            var sliders = GetComponentsInChildren<Slider>();
            this.sliderX = sliders.Length >= 1 ? sliders[0] : this.sliderX;
            this.sliderY = sliders.Length >= 2 ? sliders[1] : this.sliderY;
            this.sliderZ = sliders.Length >= 3 ? sliders[2] : this.sliderZ;
            this.sliderW = sliders.Length >= 4 ? sliders[3] : this.sliderW;
        }
    }
}
