using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    public class PrefsGuiNumericSliderInteger : PrefsGuiNumericSliderBase<int>
    {
        protected override bool IsDecimalNumber => false;


        protected override bool IsDefaultValue() => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v = 0;
            if(int.TryParse(value, out v) == true)
            {
                this.SetValueInternal(v);
            }
        }

        protected override float GetValueAsFloat() => this.GetValue();

        protected override void SetValueAsFloat(float v) => this.SetValue(Mathf.RoundToInt(v));
    }
}
