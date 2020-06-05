using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsColorSlider : Prefs.PrefsGuiBase<Color, PrefsGuiColorSlider>
    {
        public PrefsColorSlider(
            string key, Color defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Color, PrefsGuiColorSlider>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiColorSlider gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
