using UnityEngine;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiNumericDecimal : PrefsGuiNumericBase<float>
    {
        protected override bool IsDecimalNumber => true;


        protected override bool IsDefaultValue() => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v = 0f;
            if(float.TryParse(value, out v) == true)
            {
                this.SetValueInternal(v);
            }
        }
    }
}
