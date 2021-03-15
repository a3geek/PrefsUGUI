using System;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsBool : PrefsGuiBase<bool, PrefsGuiBool>
    {
        public PrefsBool(
            string key, bool defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<bool, PrefsGuiBool>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiBool gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
