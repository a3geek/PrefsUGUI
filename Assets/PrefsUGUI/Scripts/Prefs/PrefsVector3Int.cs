using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector3Int : Prefs.PrefsParam<Vector3Int, PrefsGuiVector3>
    {
        public PrefsVector3Int(string key, Vector3Int defaultValue = default(Vector3Int), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        protected override void OnCreatedGui(PrefsGuiVector3 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
