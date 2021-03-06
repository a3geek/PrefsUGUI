﻿using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector3 : PrefsGuiVectorBase<Vector3>
    {
        protected override int ElementCount => 3;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;


        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override bool IsDefaultValue()
            => this.GetValue() == this.defaultGetter();

        protected override void SetValueInternal(string value)
        {
            var v4 = this.GetVector4FromField();
            this.SetValueInternal(new Vector3(v4.x, v4.y, v4.z));
        }
    }
}
