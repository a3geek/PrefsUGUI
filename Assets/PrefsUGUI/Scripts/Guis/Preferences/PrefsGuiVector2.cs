using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiVector2 : VectorGuiBase<Vector2, PrefsGuiVector2>
    {
        public override PrefsGuiVector2 Component => this;

        protected override int ElementCount => 2;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v4 = this.GetVector4FromFields();
            this.SetValueInternal(new Vector2(v4.x, v4.y));
        }

        protected override Vector2 GetDeltaAddedValue(Vector4 v4)
            => this.value + new Vector2(v4.x, v4.y);
    }
}
