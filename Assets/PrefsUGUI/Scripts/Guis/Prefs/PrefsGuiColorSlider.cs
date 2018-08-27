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


        public void OnChangedSlider(float v)
        {
            this.SetValue(new Color(
                this.sliderX.value, this.sliderY.value,
                this.sliderZ.value, this.sliderW.value
            ));

            this.FireOnValueChanged();
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
