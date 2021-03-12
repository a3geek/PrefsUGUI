using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiVector2Int : VectorGuiBase<Vector2Int, PrefsGuiVector2Int>
    {
        public override PrefsGuiVector2Int Component => this;

        protected override int ElementCount => 2;
        protected override InputField.ContentType ContentType => InputField.ContentType.IntegerNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();

        protected override void SetValueInternal(string value)
        {
            var v3 = this.GetVector3IntFromFields();
            this.SetValueInternal(new Vector2Int(v3.x, v3.y));
        }

        protected override Vector2Int GetDeltaAddedValue(Vector4 v4)
            => this.value + new Vector2Int(Mathf.RoundToInt(v4.x), Mathf.RoundToInt(v4.y));
    }
}
