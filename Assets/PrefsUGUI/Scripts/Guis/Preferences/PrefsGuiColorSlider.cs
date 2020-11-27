using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiColorSlider : VectorSliderGuiBase<Color, PrefsGuiColorSlider>
    {
        public override PrefsGuiColorSlider Component => this;

        protected override int ElementCount => 4;
        protected override bool IsDecimalNumber => true;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;

        [SerializeField]
        protected RawImage preview = null;


        protected override void Reset()
        {
            base.Reset();
            this.preview = this.GetComponentInChildren<RawImage>();
        }

        public override void Initialize(string label, Color initialValue, Func<Color> defaultGetter)
        {
            this.SetValueInternal(initialValue);
            this.Initialize(label, initialValue, 0f, 1f, defaultGetter);
        }

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();
            this.preview.color = this.GetValue();
        }

        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override float GetElementAsFloat(int index)
            => this.value[index];

        protected override Color GetValueFromSlider()
            => new Color(this.sliderX.value, this.sliderY.value, this.sliderZ.value, this.sliderW.value);

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector4FromFields());

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override Color GetDeltaAddedValue(Vector4 v4)
            => this.value + (Color)v4;
    }
}
