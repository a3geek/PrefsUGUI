using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsFloat : Prefs.PrefsExtends<float, PrefsGuiNumericDecimal>
    {
        public PrefsFloat(string key, float defaultValue = default(float), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericDecimal gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
