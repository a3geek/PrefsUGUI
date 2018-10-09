using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsBool : Prefs.PrefsParam<bool, PrefsGuiBool>
    {
        public PrefsBool(string key, bool defaultValue = default(bool), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiBool gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
