using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector4 : Prefs.PrefsGuiBase<Vector4, PrefsGuiVector4>
    {
        public PrefsVector4(
            string key, Vector4 defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Vector4, PrefsGuiVector4>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector4 gui)
            => gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
    }
}
