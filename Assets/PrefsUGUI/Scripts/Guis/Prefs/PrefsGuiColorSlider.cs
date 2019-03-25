using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiColorSlider : PrefsGuiVectorSliderBase<Color>
    {
        protected override int ElementCount => 4;
        protected override bool IsDecimalNumber => true;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;

        [SerializeField]
        protected RawImage preview = null;


        public override void Initialize(string label, Color initialValue, Func<Color> defaultGetter)
        {
            this.SetValueInternal(initialValue);
            this.Initialize(label, initialValue, 0f, 1f, defaultGetter);
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.preview.color = this.GetValue();
        }

        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override float GetElementAsFloat(int index)
            => this.value[index];

        protected override Color GetValueFromSlider()
            => new Color(this.sliderX.value, this.sliderY.value, this.sliderZ.value, this.sliderW.value);

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector4FromField());

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void Reset()
        {
            base.Reset();
            this.preview = GetComponentInChildren<RawImage>();
        }
    }
}
