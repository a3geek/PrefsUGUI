using System;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsFloat : Prefs.PrefsGuiBase<float, PrefsGuiNumericDecimal>
    {
        public PrefsFloat(
            string key, float defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<float, PrefsGuiNumericDecimal>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericDecimal gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
