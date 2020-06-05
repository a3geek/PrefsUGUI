using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsColor : Prefs.PrefsGuiBase<Color, PrefsGuiColor>
    {
        public PrefsColor(
            string key, Color defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Color, PrefsGuiColor>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiColor gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
