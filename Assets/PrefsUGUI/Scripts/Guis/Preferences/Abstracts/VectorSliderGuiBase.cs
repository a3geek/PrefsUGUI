using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public abstract class VectorSliderGuiBase<ValType, GuiType> : VectorGuiBase<ValType, GuiType> where ValType : struct
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        protected abstract bool IsDecimalNumber { get; }
        protected virtual Slider[] sliders { get; set; } = new Slider[0];

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
            for (var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnSliderChanged);
            }
        }

        protected override void Reset()
        {
            base.Reset();

            var sliders = this.GetComponentsInChildren<Slider>();
            this.sliderX = sliders.Length >= 1 ? sliders[0] : this.sliderX;
            this.sliderY = sliders.Length >= 2 ? sliders[1] : this.sliderY;
            this.sliderZ = sliders.Length >= 3 ? sliders[2] : this.sliderZ;
            this.sliderW = sliders.Length >= 4 ? sliders[3] : this.sliderW;
        }

        public override void Initialize(string label, ValType initialValue, Func<ValType> defaultGetter)
        {
            var val = this.GetElementAsFloat(0);
            this.Initialize(label, initialValue, 0f, val + val, defaultGetter);
        }

        public virtual void Initialize(string label, ValType initialValue, float minValue, float maxValue, Func<ValType> defaultGetter)
        {
            this.sliders = new Slider[] { this.sliderX, this.sliderY, this.sliderZ, this.sliderW };

            this.InitializeSlider(minValue, maxValue);
            base.Initialize(label, initialValue, defaultGetter);

            this.inited = true;
        }

        protected virtual void InitializeSlider(float min, float max)
        {
            var sliders = this.sliders;
            for (var i = 0; i < this.ElementCount; i++)
            {
                sliders[i].wholeNumbers = !this.IsDecimalNumber;
                sliders[i].minValue = (min > max ? max : min);
                sliders[i].maxValue = (min < max ? max : min);
            }
        }

        protected override void SetFields()
        {
            this.inited = false;
            base.SetFields();

            for (var i = 0; i < this.sliders.Length; i++)
            {
                this.sliders[i].value = this.GetElementAsFloat(i);
            }

            this.inited = true;
        }

        protected virtual void OnSliderChanged(float v)
        {
            if (this.inited == false)
            {
                return;
            }

            this.SetValue(this.GetValueFromSlider());
            this.FireOnValueChanged();
        }

        protected virtual UnityEvent<float>[] GetSliderEvents()
        {
            var events = new UnityEvent<float>[this.ElementCount];
            for (var i = 0; i < this.ElementCount; i++)
            {
                events[i] = this.sliders[i].onValueChanged;
            }

            return events;
        }

        protected abstract ValType GetValueFromSlider();
        protected abstract float GetElementAsFloat(int index);
    }
}
