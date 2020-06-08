using System;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsBool : Prefs.PrefsGuiBase<bool, PrefsGuiBool>
    {
        public PrefsBool(
            string key, bool defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<bool, PrefsGuiBool>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiBool gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
