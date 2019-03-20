using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector2 : Prefs.PrefsExtends<Vector2, PrefsGuiVector2>
    {
        public PrefsVector2(string key, Vector2 defaultValue = default(Vector2), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }
        
        protected override void OnCreatedGuiInternal(PrefsGuiVector2 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
