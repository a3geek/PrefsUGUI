using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsGuiConnector<ValType, GuiType> : PrefsGuiBaseConnector<ValType, GuiType> where GuiType : InputGuiValueBase<ValType>
        {
            public PrefsGuiConnector(string key, ValType defaultValue = default(ValType), GuiHierarchy hierarchy = null, string guiLabel = "")
                : base(key, defaultValue, hierarchy, guiLabel)
            {
            }

            protected override void AfterRegist()
                => AddPrefs<ValType, GuiType>(this, gui => this.ExecuteOnCreatedGui(gui));
        }
    }
}
