using System;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiNumericDecimal : NumericGuiBase<float, PrefsGuiNumericDecimal>
    {
        public override PrefsGuiNumericDecimal Component => this;

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
    }
}
