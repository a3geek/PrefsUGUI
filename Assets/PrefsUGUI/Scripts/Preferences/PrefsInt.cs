using System;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsInt : PrefsGuiBase<int, PrefsGuiNumericInteger>
    {
        public PrefsInt(
            string key, int defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<int, PrefsGuiNumericInteger>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericInteger gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
