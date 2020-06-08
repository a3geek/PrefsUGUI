using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiVector4 : VectorGuiBase<Vector4, PrefsGuiVector4>
    {
        public override PrefsGuiVector4 Component => this;

        protected override int ElementCount => 4;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector4FromFields());
    }
}
