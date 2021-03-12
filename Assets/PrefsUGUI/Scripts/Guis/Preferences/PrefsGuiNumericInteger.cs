using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiNumericInteger : NumericGuiBase<int, PrefsGuiNumericInteger>
    {
        public override PrefsGuiNumericInteger Component => this;

        protected override bool IsDecimalNumber => false;


        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();

        protected override void SetValueInternal(string value)
        {
            if (int.TryParse(value, out var v) == true)
            {
                this.SetValueInternal(v);
            }
        }

        protected override int GetDeltaAddedValue(float delta)
            => this.value + Mathf.RoundToInt(delta);
    }
}
