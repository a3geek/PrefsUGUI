using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsBool : Prefs.PrefsExtends<bool, PrefsGuiBool>
    {
        public PrefsBool(string key, bool defaultValue = default(bool), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiBool gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
