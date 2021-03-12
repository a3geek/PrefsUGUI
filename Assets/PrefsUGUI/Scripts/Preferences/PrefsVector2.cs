using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector2 : PrefsGuiBase<Vector2, PrefsGuiVector2>
    {
        public PrefsVector2(
            string key, Vector2 defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Vector2, PrefsGuiVector2>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector2 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
