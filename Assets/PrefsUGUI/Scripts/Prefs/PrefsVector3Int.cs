using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector3Int : Prefs.PrefsExtends<Vector3Int, PrefsGuiVector3Int>
    {
        public PrefsVector3Int(string key, Vector3Int defaultValue = default(Vector3Int), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiVector3Int gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
