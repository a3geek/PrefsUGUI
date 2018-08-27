using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    using Utilities;

    public abstract class PrefsGuiVectorBase<T1> : InputGuiBase where T1 : struct
    {
        public const string ZeroString = "0";

        protected virtual InputField[] fields
        {
            get { return new InputField[] { this.fieldX, this.fieldY, this.fieldZ, this.fieldW }; }
        }
        protected abstract int ElementCount { get; }
        protected abstract InputField.ContentType ContentType { get; }

        [SerializeField]
        protected InputField fieldX = null;
        [SerializeField]
        protected InputField fieldY = null;
        [SerializeField]
        protected InputField fieldZ = null;
        [SerializeField]
        protected InputField fieldW = null;

        protected T1 value = default(T1);

        
        protected virtual void Initialize(string label)
        {
            var fields = this.fields;
            for(var i = 0; i < this.ElementCount; i++)
            {
                fields[i].contentType = this.ContentType;
            }
            
            this.SetLabel(label);
        }
        
        protected override void SetFields()
        {
            base.SetFields();

            var fields = this.fields;
            for(var i = 0; i < this.ElementCount; i++)
            {
                fields[i].text = this.GetElement(i);
            }
        }
        
        protected virtual Vector3Int GetVector3Int(Vector3Int defaultValue = default(Vector3Int))
        {
            return new Vector3Int(
                this.GetText(fieldX).ToInt(defaultValue.x), this.GetText(fieldY).ToInt(defaultValue.y),
                this.GetText(fieldZ).ToInt(defaultValue.z)
            );
        }

        protected virtual Vector4 GetVector4(Vector4 defaultValue = default(Vector4))
        {
            return new Vector4(
                this.GetText(fieldX).ToFloat(defaultValue.x), this.GetText(fieldY).ToFloat(defaultValue.y),
                this.GetText(fieldZ).ToFloat(defaultValue.z), this.GetText(fieldW).ToFloat(defaultValue.w)
            );
        }

        protected virtual string GetText(InputField field)
        {
            if(field != null && field.text == "")
            {
                field.text = ZeroString;
            }

            return field == null ? ZeroString : field.text;
        }

        protected abstract string GetElement(int index);

        protected override void Reset()
        {
            base.Reset();

            var fields = GetComponentsInChildren<InputField>();
            this.fieldX = fields.Length >= 1 ? fields[0] : this.fieldX;
            this.fieldY = fields.Length >= 2 ? fields[1] : this.fieldY;
            this.fieldZ = fields.Length >= 3 ? fields[2] : this.fieldZ;
            this.fieldW = fields.Length >= 4 ? fields[3] : this.fieldW;
        }
    }
}
