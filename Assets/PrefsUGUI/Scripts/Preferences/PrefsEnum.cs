﻿using System;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsEnum<T> : PrefsGuiBase<int, PrefsGuiEnum> where T : Enum
    {
        public PrefsEnum(
            string key, T defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<int, PrefsGuiEnum>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, Convert.ToInt32(defaultValue), hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiEnum gui)
            => gui.Initialize<T>(this.GuiLabel, this.Get());
    }
}
