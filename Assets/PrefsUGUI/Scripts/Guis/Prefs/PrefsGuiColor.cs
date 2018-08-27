using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiColor : PrefsGuiVector<Color>
    {
        protected override int ElementCount
        {
            get { return 4; }
        }
        protected override InputField.ContentType ContentType
        {
            get { return InputField.ContentType.DecimalNumber; }
        }

        [SerializeField]
        protected RawImage preview = null;


        protected override void SetFields()
        {
            base.SetFields();
            this.preview.color = this.value;
        }

        protected override string GetElement(int index)
        {
            return this.value[index].ToString();
        }

        protected override void SetValueInternal(string value)
        {
            this.SetValue(this.GetVector4(this.value));
        }
        
        protected override void Reset()
        {
            base.Reset();
            this.preview = GetComponentInChildren<RawImage>();
        }
    }
}
