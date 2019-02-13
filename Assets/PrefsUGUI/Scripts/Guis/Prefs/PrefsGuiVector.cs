using System;

namespace PrefsUGUI.Guis.Prefs
{
    public abstract class PrefsGuiVector<T1> : PrefsGuiVectorBase<T1> where T1 : struct
    {
        protected Func<T1> defaultGetter = null;


        public virtual T1 GetValue() => this.value;

        public virtual void SetValue(T1 value)
        {
            this.value = value;
            this.SetFields();
        }

        public override void SetValue(object value)
        {
            if(value is T1 == false)
            {
                return;
            }

            this.SetValue((T1)value);
        }

        public virtual void Initialize(string label, T1 initialValue, Func<T1> defaultGetter)
        {
            this.Initialize(label);

            this.defaultGetter = defaultGetter;
            this.SetValue(initialValue);
        }

        protected override bool IsDefaultValue() => this.GetValue().Equals(this.defaultGetter());

        public override object GetValueObject() => this.GetValue();
    }
}
