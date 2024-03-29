﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiColor : VectorGuiBase<Color, PrefsGuiColor>
    {
        public override PrefsGuiColor Component => this;

        protected override int ElementCount => 4;
        protected override InputField.ContentType ContentType => InputField.ContentType.DecimalNumber;

        [SerializeField]
        protected RawImage preview = null;


        protected override void Reset()
        {
            base.Reset();
            this.preview = this.GetComponentInChildren<RawImage>();
        }

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();
            this.preview.color = this.GetValue();
        }

        protected override string GetElement(int index)
            => this.value[index].ToString();

        protected override void SetValueInternal(string value)
            => this.SetValueInternal(this.GetVector4FromFields());

        protected override bool IsDefaultValue()
            => this.GetValue() == this.prefsEvents.GetDefaultValue();

        protected override Color GetDeltaAddedValue(Vector4 v4)
            => this.value + (Color)v4;
    }
}
