using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;
    using XmlStorage;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsExtends<ValType, GuiType> : PrefsGuiConnector<ValType, GuiType> where GuiType : InputGuiValueBase<ValType>
        {
            public PrefsExtends(string key, ValType defaultValue = default(ValType), GuiHierarchy hierarchy = null, string guiLabel = "")
                : base(key, defaultValue, hierarchy, guiLabel)
            {
                ;
            }

            public static implicit operator ValType(PrefsExtends<ValType, GuiType> prefs) => prefs.Get();
        }
    }
}
