using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiRect : PrefsGuiVectorBase<Rect>
    {
        protected override int ElementCount => 4;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;


        protected override string GetElement(int index)
        {
            switch(index)
            {
                case 0: return this.value.x.ToString();
                case 1: return this.value.y.ToString();
                case 2: return this.value.width.ToString();
                default: return this.value.height.ToString();
            }
        }

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v4 = this.GetVector4FromField();
            this.SetValueInternal(new Rect(v4.x, v4.y, v4.z, v4.w));
        }
    }
}
