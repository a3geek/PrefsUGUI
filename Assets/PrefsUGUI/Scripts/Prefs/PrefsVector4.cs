using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector4 : Prefs.PrefsExtends<Vector4, PrefsGuiVector4>
    {
        public PrefsVector4(string key, Vector4 defaultValue = default(Vector4), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        protected override void OnCreatedGuiInternal(PrefsGuiVector4 gui)
            => gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
    }
}
