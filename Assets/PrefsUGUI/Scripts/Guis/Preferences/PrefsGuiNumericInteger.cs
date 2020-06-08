using System;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiNumericInteger : NumericGuiBase<int, PrefsGuiNumericInteger>
    {
        public override PrefsGuiNumericInteger Component => this;

        protected override bool IsDecimalNumber => false;


        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            if (int.TryParse(value, out var v) == true)
            {
                this.SetValueInternal(v);
            }
        }
    }
}
