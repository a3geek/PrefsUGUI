using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsColorSlider : Prefs.PrefsExtends<Color, PrefsGuiColorSlider>
    {
        public PrefsColorSlider(string key, Color defaultValue = default(Color), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiColorSlider gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
