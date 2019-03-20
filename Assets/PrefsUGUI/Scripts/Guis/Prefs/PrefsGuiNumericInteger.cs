using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiNumericInteger : PrefsGuiNumericBase<int>
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
    }
}
