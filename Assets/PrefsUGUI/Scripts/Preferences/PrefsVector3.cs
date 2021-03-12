using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector3 : PrefsGuiBase<Vector3, PrefsGuiVector3>
    {
        public PrefsVector3(
            string key, Vector3 defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Vector3, PrefsGuiVector3>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector3 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
