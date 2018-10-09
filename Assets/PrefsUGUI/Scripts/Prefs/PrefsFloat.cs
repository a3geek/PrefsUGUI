using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsFloat : Prefs.PrefsParam<float, PrefsGuiNumeric>
    {
        public PrefsFloat(string key, float defaultValue = default(float), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiNumeric gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
