using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiNumericSliderInteger : NumericSliderGuiBase<int, PrefsGuiNumericSliderInteger>
    {
        public override PrefsGuiNumericSliderInteger Component => this;

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

        protected override float GetValueAsFloat()
            => this.GetValue();

        protected override void SetValueInternalAsFloat(float v)
            => this.SetValueInternal(Mathf.RoundToInt(v));

        protected override int GetDeltaAddedValue(float delta)
            => this.value + Mathf.RoundToInt(delta);
    }
}
