using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsRect : Prefs.PrefsGuiBase<Rect, PrefsGuiRect>
    {
        public PrefsRect(
            string key, Rect defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<Rect, PrefsGuiRect>> onCreatedGui = null
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiRect gui)
            => gui.Initialize(this.GuiLabel, this.Get(), this.GetDefaultValue);
    }
}
