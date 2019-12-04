using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsFloat : Prefs.PrefsExtends<float, PrefsGuiNumericDecimal>
    {
        public PrefsFloat(string key, float defaultValue = default(float), GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBaseConnector<float, PrefsGuiNumericDecimal>> onCreatedGui = null)
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui) { }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericDecimal gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
