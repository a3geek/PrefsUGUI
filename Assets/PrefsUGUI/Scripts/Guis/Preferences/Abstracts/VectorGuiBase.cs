﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Commons;
    using CustomExtensions.Csharp;

    [Serializable]
    public abstract class VectorGuiBase<ValType, GuiType> : TextInputGuiBase<ValType, GuiType> where ValType : struct
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        private const string ZeroString = "0";

        protected abstract int ElementCount { get; }
        protected abstract InputField.ContentType ContentType { get; }
        protected virtual InputField[] Fields { get; set; } = Array.Empty<InputField>();

        [SerializeField]
        protected NumericUpDownInput updown = new NumericUpDownInput();
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
            if(this.Fields.Length == 0)
            {
                this.InitializeFields();
            }

            base.Awake();
        }

        protected virtual void Update()
        {
            for (var i = 0; i < this.ElementCount; i++)
            {
                if(this.updown.Update(this.Fields[i], out var delta) == false)
                {
                    continue;
                }

                var vec = Vector4.zero;
                vec[i] = delta;
                this.OnInputValue(this.GetDeltaAddedValue(vec));
            }
        }

        protected override void Reset()
        {
            base.Reset();

            if(this.ContentType == InputField.ContentType.IntegerNumber)
            {
                this.updown.UpDownStep = 1f;
                this.updown.StepDelay = 0.1f;
            }

            var fields = this.GetComponentsInChildren<InputField>();
            this.fieldX = fields.Length >= 1 ? fields[0] : this.fieldX;
            this.fieldY = fields.Length >= 2 ? fields[1] : this.fieldY;
            this.fieldZ = fields.Length >= 3 ? fields[2] : this.fieldZ;
            this.fieldW = fields.Length >= 4 ? fields[3] : this.fieldW;
        }

        public virtual void Initialize(string label, ValType initialValue)
        {
            this.SetLabel(label);

            if(this.Fields.Length == 0)
            {
                this.InitializeFields();
            }

            this.SetValue(initialValue);
        }

        protected override UnityEvent<string>[] GetInputEvents()
        {
            var events = new UnityEvent<string>[this.ElementCount];
            for (var i = 0; i < this.ElementCount; i++)
            {
                events[i] = this.Fields[i].onEndEdit;
            }

            return events;
        }

        protected override void SetFieldsInternal()
        {
            base.SetFieldsInternal();

            for (var i = 0; i < this.ElementCount; i++)
            {
                this.Fields[i].text = this.GetElement(i);
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

        protected virtual void InitializeFields()
        {
            this.Fields = new[] { this.fieldX, this.fieldY, this.fieldZ, this.fieldW };
            for(var i = 0; i < this.ElementCount; i++)
            {
                this.Fields[i].contentType = this.ContentType;
            }
        }

        protected abstract string GetElement(int index);
        protected abstract ValType GetDeltaAddedValue(Vector4 v4);
    }
}
