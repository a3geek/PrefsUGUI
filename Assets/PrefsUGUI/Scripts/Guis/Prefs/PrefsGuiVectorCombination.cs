using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    using Type = InputField.ContentType;

    public abstract class PrefsGuiVectorCombination<Vec, VecInt> : PrefsGuiVectorBase<Vec> where Vec : struct where VecInt : struct
    {
        public virtual bool IsDecimalNumber => this.isDecimalNumber;
        protected override Type ContentType => this.IsDecimalNumber == true ? Type.DecimalNumber : Type.IntegerNumber;

        [SerializeField]
        protected bool isDecimalNumber = true;

        protected VecInt valueInt = default(VecInt);
        protected Func<Vec> vecDefaultGetter = null;
        protected Func<VecInt> vecIntDefaultGetter = null;


        public virtual Vec GetFloatValue() => this.value;

        public virtual VecInt GetIntValue() => this.valueInt;

        public virtual void SetValue(Vec value)
        {
            if(this.IsDecimalNumber == false)
            {
                return;
            }

            this.SetValueInternal(value);
            this.SetFields();
        }

        public virtual void SetValue(VecInt value)
        {
            if(this.IsDecimalNumber == true)
            {
                return;
            }

            this.SetValueInternal(value);
            this.SetFields();
        }

        public void Initialize(string label, Vec initialValue, Func<Vec> defaultGetter)
        {
            this.isDecimalNumber = true;
            this.vecDefaultGetter = defaultGetter;

            this.Initialize(label);
            this.SetValue(initialValue);
        }

        public void Initialize(string label, VecInt initialValue, Func<VecInt> defaultGetter)
        {
            this.isDecimalNumber = false;
            this.vecIntDefaultGetter = defaultGetter;

            this.Initialize(label);
            this.SetValue(initialValue);
        }

        public override void SetValue(object value)
        {
            if(value is Vec == true && this.IsDecimalNumber == true)
            {
                this.SetValue((Vec)value);
            }
            else if(value is VecInt == true && this.IsDecimalNumber == false)
            {
                this.SetValue((VecInt)value);
            }
        }

        public override object GetValueObject()
            => this.IsDecimalNumber == true ? (object)this.GetFloatValue() : this.GetIntValue();

        protected virtual void SetValueInternal(Vec value) => this.value = value;

        protected virtual void SetValueInternal(VecInt value) => this.valueInt = value;

        protected override bool IsDefaultValue()
            => this.IsDecimalNumber == true ?
                this.GetFloatValue().Equals(this.vecDefaultGetter()) :
                this.GetIntValue().Equals(this.vecIntDefaultGetter());

        protected override void SetValueInternal(string value)
        {
            if(this.IsDecimalNumber == true)
            {
                var vec4 = this.GetVector4(this.VecToVector4());
                this.value = this.Vector4ToVec(vec4);
            }
            else
            {
                var vec3int = this.GetVector3Int(this.VecIntToVecotr3Int());
                this.valueInt = this.Vector3IntToVecInt(vec3int);
            }
        }

        protected abstract Vec Vector4ToVec(Vector4 vec4);
        protected abstract VecInt Vector3IntToVecInt(Vector3Int vec3);
        protected abstract Vector4 VecToVector4();
        protected abstract Vector3Int VecIntToVecotr3Int();
    }
}
