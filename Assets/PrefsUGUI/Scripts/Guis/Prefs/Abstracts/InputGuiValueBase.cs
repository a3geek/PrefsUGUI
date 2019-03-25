using System;

namespace PrefsUGUI.Guis.Prefs
{
    using Prefs = PrefsUGUI.Prefs;

    public abstract class InputGuiValueBase<ValType> : InputGuiBase
    {
        protected ValType value = default(ValType);
        protected Func<ValType> defaultGetter = delegate { return default(ValType); };


        public virtual ValType GetValue()
            => this.value;

        public virtual void SetValue(ValType value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public virtual void SetGuiListeners(Prefs.PrefsValueBase<ValType> prefs, bool withoutInitialize = true)
        {
            this.SetListener(prefs, withoutInitialize);
            this.OnValueChanged += () => prefs.Set(this.GetValue());
        }

        public override object GetValueObject()
            => this.GetValue();

        protected virtual void SetValueInternal(ValType value)
            => this.value = value;
    }
}
