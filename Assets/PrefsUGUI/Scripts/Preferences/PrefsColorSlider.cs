using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsColorSlider : PrefsGuiBase<Color, PrefsGuiColorSlider>
    {
        public PrefsColorSlider(
            string key, Color defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Color, PrefsGuiColorSlider>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiColorSlider gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
