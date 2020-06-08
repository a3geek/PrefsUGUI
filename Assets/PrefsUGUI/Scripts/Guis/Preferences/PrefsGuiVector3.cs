using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiVector3 : VectorGuiBase<Vector3, PrefsGuiVector3>
    {
        public override PrefsGuiVector3 Component => this;

        protected override int ElementCount => 3;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v4 = this.GetVector4FromFields();
            this.SetValueInternal(new Vector3(v4.x, v4.y, v4.z));
        }
    }
}
