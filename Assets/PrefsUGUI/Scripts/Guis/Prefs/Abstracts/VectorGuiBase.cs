using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Utilities;

    [Serializable]
    public abstract class VectorGuiBase<T1> : TextInputGuiBase<T1> where T1 : struct
    {
        private const string ZeroString = "0";

        protected abstract int ElementCount { get; }
        protected abstract InputField.ContentType ContentType { get; }
        protected virtual InputField[] fields { get; set; } = new InputField[0];

        [SerializeField]
        protected InputField fieldX = null;
        [SerializeField]
        protected InputField fieldY = null;
        [SerializeField]
        protected InputField fieldZ = null;
        [SerializeField]
        protected InputField fieldW = null;


        protected override void Awake()
        {
            base.Awake();
            this.fields = new InputField[] { this.fieldX, this.fieldY, this.fieldZ, this.fieldW };
        }

        protected override void Reset()
        {
            base.Reset();

            var fields = this.GetComponentsInChildren<InputField>();
            this.fieldX = fields.Length >= 1 ? fields[0] : this.fieldX;
            this.fieldY = fields.Length >= 2 ? fields[1] : this.fieldY;
            this.fieldZ = fields.Length >= 3 ? fields[2] : this.fieldZ;
            this.fieldW = fields.Length >= 4 ? fields[3] : this.fieldW;
        }

        public virtual void Initialize(string label, T1 initialValue, Func<T1> defaultGetter)
        {
            this.SetLabel(label);

            for (var i = 0; i < this.ElementCount; i++)
            {
                this.fields[i].contentType = this.ContentType;
            }

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override UnityEvent<string>[] GetInputEvents()
        {
            var events = new UnityEvent<string>[this.ElementCount];
            for (var i = 0; i < this.ElementCount; i++)
            {
                events[i] = this.fields[i].onEndEdit;
            }

            return events;
        }

        protected override void SetFields()
        {
            base.SetFields();

            for (var i = 0; i < this.ElementCount; i++)
            {
                this.fields[i].text = this.GetElement(i);
            }
        }

        protected virtual Vector3Int GetVector3IntFromFields(Vector3Int defaultValue = default)
        {
            return new Vector3Int(
                this.GetText(this.fieldX).ToInt(defaultValue.x), this.GetText(this.fieldY).ToInt(defaultValue.y),
                this.GetText(this.fieldZ).ToInt(defaultValue.z)
            );
        }

        protected virtual Vector4 GetVector4FromFields(Vector4 defaultValue = default)
        {
            return new Vector4(
                this.GetText(this.fieldX).ToFloat(defaultValue.x), this.GetText(this.fieldY).ToFloat(defaultValue.y),
                this.GetText(this.fieldZ).ToFloat(defaultValue.z), this.GetText(this.fieldW).ToFloat(defaultValue.w)
            );
        }

        protected virtual string GetText(InputField field)
        {
            if (field != null && string.IsNullOrEmpty(field.text) == true)
            {
                field.text = ZeroString;
            }

            return field == null ? ZeroString : field.text;
        }

        protected abstract string GetElement(int index);
    }
}
