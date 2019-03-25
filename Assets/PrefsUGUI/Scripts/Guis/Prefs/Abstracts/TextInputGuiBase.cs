using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    public abstract class TextInputGuiBase<ValType> : InputGuiValueBase<ValType>
    {
        protected override void Awake()
        {
            base.Awake();

            var events = this.GetInputEvents();
            for(var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnInputValue);
            }
        }

        protected abstract UnityEvent<string>[] GetInputEvents();

        protected virtual void OnInputValue(string v)
        {
            this.SetValueInternal(v);
            this.FireOnValueChanged();
        }

        protected abstract void SetValueInternal(string value);
    }
}
