using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsColor : Prefs.PrefsGuiBase<Color, PrefsGuiColor>
    {
        public PrefsColor(
            string key, Color defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Color, PrefsGuiColor>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiColor gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
