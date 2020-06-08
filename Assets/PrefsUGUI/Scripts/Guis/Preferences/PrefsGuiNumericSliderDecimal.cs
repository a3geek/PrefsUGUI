using System;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiNumericSliderDecimal : NumericSliderGuiBase<float, PrefsGuiNumericSliderDecimal>
    {
        public override PrefsGuiNumericSliderDecimal Component => this;

        protected override bool IsDecimalNumber => true;


        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            if (float.TryParse(value, out var v) == true)
            {
                this.SetValueInternal(v);
            }
        }

        protected override float GetValueAsFloat()
            => this.GetValue();

        protected override void SetValueAsFloat(float v)
            => this.SetValue(v);
    }
}
