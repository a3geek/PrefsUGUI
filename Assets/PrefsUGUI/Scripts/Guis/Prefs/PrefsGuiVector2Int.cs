using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector2Int : PrefsGuiVectorBase<Vector2Int>
    {
        protected override int ElementCount => 2;
        protected override InputField.ContentType ContentType => InputField.ContentType.IntegerNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v3 = this.GetVector3IntFromField();
            this.SetValueInternal(new Vector2Int(v3.x, v3.y));
        }
    }
}
