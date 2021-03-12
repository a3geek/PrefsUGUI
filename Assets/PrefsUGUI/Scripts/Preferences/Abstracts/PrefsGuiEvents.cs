using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using Guis.Preferences;

    public abstract partial class PrefsGuiBase<ValType, GuiType>
    {
        protected class PrefsGuiEvents : IPrefsGuiEvents<ValType>
        {
            private PrefsGuiBase<ValType, GuiType> parent = null;


            public PrefsGuiEvents(PrefsGuiBase<ValType, GuiType> parent)
            {
                this.parent = parent;
            }

            public void OnEditedInGui(ValType value)
                => this.parent.OnEditedInGuiInternal(value);

            public void OnClickedDefaultButton()
                => this.parent.ResetDefaultValue();

            public ValType GetDefaultValue()
                => this.parent.GetDefaultValue();
        }
    }
}
