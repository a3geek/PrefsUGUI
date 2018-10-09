using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsColor : Prefs.PrefsParam<Color, PrefsGuiColor>
    {
        public PrefsColor(string key, Color defaultValue = default(Color), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiColor gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
