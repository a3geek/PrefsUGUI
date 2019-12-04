using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsExtends<ValType, GuiType> : PrefsGuiConnector<ValType, GuiType> where GuiType : InputGuiValueBase<ValType>
        {
            public PrefsExtends(string key, ValType defaultValue = default(ValType),
                GuiHierarchy hierarchy = null, string guiLabel = null, Action<PrefsGuiBaseConnector<ValType, GuiType>> onCreatedGui = null)
                : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
            {
                ;
            }

            public static implicit operator ValType(PrefsExtends<ValType, GuiType> prefs) => prefs.Get();
        }
    }
}
