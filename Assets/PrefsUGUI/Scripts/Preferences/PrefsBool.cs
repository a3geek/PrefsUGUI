using System;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsBool : Prefs.PrefsGuiBase<bool, PrefsGuiBool>
    {
        public PrefsBool(
            string key, bool defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<bool, PrefsGuiBool>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiBool gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
