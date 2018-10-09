using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiColorSlider : PrefsGuiColor
    {
        protected Slider[] sliders
        {
            get { return new Slider[] { this.sliderX, this.sliderY, this.sliderZ, this.sliderW }; }
        }

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
        
        public override void Initialize(string label, Color initialValue, Func<Color> defaultGetter)
        {
            base.Initialize(label, initialValue, defaultGetter);

            this.inited = true;
        }
        
        protected override void Initialize(string label)
        {
            base.Initialize(label);
            
            var sliders = this.sliders;
            for(var i = 0; i < this.ElementCount; i++)
            {
                sliders[i].minValue = 0f;
                sliders[i].maxValue = 1f;
            }
        }

        protected override void SetFields()
        {
            base.SetFields();

            var sliders = this.sliders;
            for(var i = 0; i < this.ElementCount; i++)
            {
                sliders[i].value = this.value[i];
            }
        }

        protected virtual void OnChangedSlider(float v)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValue(new Color(
                this.sliderX.value, this.sliderY.value,
                this.sliderZ.value, this.sliderW.value
            ));

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
