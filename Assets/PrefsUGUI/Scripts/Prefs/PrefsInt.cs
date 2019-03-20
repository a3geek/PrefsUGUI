using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsInt : Prefs.PrefsExtends<int, PrefsGuiNumericInteger>
    {
        public PrefsInt(string key, int defaultValue = default(int), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericInteger gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
