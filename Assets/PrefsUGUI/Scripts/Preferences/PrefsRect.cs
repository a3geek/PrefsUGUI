using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsRect : PrefsGuiBase<Rect, PrefsGuiRect>
    {
        public PrefsRect(
            string key, Rect defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Rect, PrefsGuiRect>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiRect gui)
            => gui.Initialize(this.GuiLabel, this.Get());
    }
}
