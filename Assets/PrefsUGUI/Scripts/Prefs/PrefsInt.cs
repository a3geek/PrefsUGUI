using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsInt : Prefs.PrefsGuiBase<int, PrefsGuiNumericInteger>
    {
        public PrefsInt(
            string key, int defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<int, PrefsGuiNumericInteger>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericInteger gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
