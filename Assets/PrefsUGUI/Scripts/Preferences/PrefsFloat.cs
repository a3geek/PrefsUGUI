using System;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsFloat : PrefsGuiBase<float, PrefsGuiNumericDecimal>
    {
        public PrefsFloat(
            string key, float defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<float, PrefsGuiNumericDecimal>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericDecimal gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
