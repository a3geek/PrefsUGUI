﻿using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiNumericSliderDecimal : NumericSliderGuiBase<float, PrefsGuiNumericSliderDecimal>
    {
        public override PrefsGuiNumericSliderDecimal Component => this;

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

        protected override float GetValueAsFloat()
            => this.GetValue();

        protected override void SetValueInternalAsFloat(float v)
            => this.SetValueInternal(v);

        protected override float GetDeltaAddedValue(float delta)
            => this.value + delta;
    }
}
