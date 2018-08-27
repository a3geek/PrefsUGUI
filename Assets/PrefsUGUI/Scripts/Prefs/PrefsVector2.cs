using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector2 : Prefs.PrefsParam<Vector2, PrefsGuiVector2>
    {
        public PrefsVector2(string key, Vector2 defaultValue = default(Vector2), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }


        protected override void OnCreatedGui(PrefsGuiVector2 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
