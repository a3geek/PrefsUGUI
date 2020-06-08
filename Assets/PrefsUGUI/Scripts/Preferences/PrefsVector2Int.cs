using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector2Int : Prefs.PrefsGuiBase<Vector2Int, PrefsGuiVector2Int>
    {
        public PrefsVector2Int(
            string key, Vector2Int defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Vector2Int, PrefsGuiVector2Int>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector2Int gui)
        {
            gui.Initialize(this.guiLabel, this.Get(), this.GetDefaultValue);
        }
    }
}
