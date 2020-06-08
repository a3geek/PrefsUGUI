using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiVector3Int : VectorGuiBase<Vector3Int, PrefsGuiVector3Int>
    {
        public override PrefsGuiVector3Int Component => this;

        protected override int ElementCount => 3;
        protected override InputField.ContentType ContentType => InputField.ContentType.IntegerNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector3IntFromFields());
    }
}
