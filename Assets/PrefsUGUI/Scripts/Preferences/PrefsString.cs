using System;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsString : PrefsGuiBase<string, PrefsGuiString>
    {
        public PrefsString(
            string key, string defaultValue = "", Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<string, PrefsGuiString>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiString gui)
            => gui.Initialize(this.GuiLabel, this.Get());
    }
}
