using System;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public abstract class TextInputGuiBase<ValType, GuiType> : PrefsInputGuiBase<ValType, GuiType>
        where GuiType : PrefsInputGuiBase<ValType, GuiType>
    {
        protected override void Awake()
        {
            base.Awake();

            var events = this.GetInputEvents();
            for (var i = 0; i < events.Length; i++)
            {
                events[i].AddListener(this.OnInputValue);
            }
        }

        protected abstract UnityEvent<string>[] GetInputEvents();

        protected virtual void OnInputValue(string v)
        {
            this.SetValueInternal(v);
            this.SetFields(true);
        }

        protected virtual void OnInputValue(ValType v)
        {
            this.SetValueInternal(v);
            this.SetFields(true);
        }

        protected abstract void SetValueInternal(string value);
    }
}
