using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiNumericDecimal : NumericGuiBase<float, PrefsGuiNumericDecimal>
    {
        public override PrefsGuiNumericDecimal Component => this;

        protected override bool IsDecimalNumber => true;


        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();

        protected override void SetValueInternal(string value)
        {
            if (float.TryParse(value, out var v) == true)
            {
                this.SetValueInternal(v);
            }
        }

        protected override float GetDeltaAddedValue(float delta)
            => this.value + delta;
    }
}
