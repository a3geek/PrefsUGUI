using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Utilities;

    public abstract class PrefsGuiVectorBase<T1> : TextInputGuiBase<T1> where T1 : struct
    {
        public const string ZeroString = "0";

        protected virtual InputField[] fields => new InputField[] { this.fieldX, this.fieldY, this.fieldZ, this.fieldW };
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


        public virtual void Initialize(string label, T1 initialValue, Func<T1> defaultGetter)
        {
            this.SetLabel(label);

            var fields = this.fields;
            for(var i = 0; i < this.ElementCount; i++)
            {
                fields[i].contentType = this.ContentType;
            }

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override UnityEvent<string>[] GetInputEvents()
        {
            var fields = this.fields;
            var events = new UnityEvent<string>[this.ElementCount];

            for(var i = 0; i < this.ElementCount; i++)
            {
                events[i] = fields[i].onEndEdit;
            }

            return events;
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

        protected virtual Vector3Int GetVector3IntFromField(Vector3Int defaultValue = default(Vector3Int))
        {
            return new Vector3Int(
                this.GetText(fieldX).ToInt(defaultValue.x), this.GetText(fieldY).ToInt(defaultValue.y),
                this.GetText(fieldZ).ToInt(defaultValue.z)
            );
        }

        protected virtual Vector4 GetVector4FromField(Vector4 defaultValue = default(Vector4))
        {
            return new Vector4(
                this.GetText(fieldX).ToFloat(defaultValue.x), this.GetText(fieldY).ToFloat(defaultValue.y),
                this.GetText(fieldZ).ToFloat(defaultValue.z), this.GetText(fieldW).ToFloat(defaultValue.w)
            );
        }

        protected virtual string GetText(InputField field)
        {
            if(field != null && string.IsNullOrEmpty(field.text) == true)
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
