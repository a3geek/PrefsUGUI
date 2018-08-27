using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector2Int : Prefs.PrefsParam<Vector2Int, PrefsGuiVector2>
    {
        public PrefsVector2Int(string key, Vector2Int defaultValue = default(Vector2Int), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        protected override void OnCreatedGui(PrefsGuiVector2 gui)
        {
            gui.Initialize(this.guiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
