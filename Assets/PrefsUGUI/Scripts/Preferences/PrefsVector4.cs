﻿using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector4 : PrefsGuiBase<Vector4, PrefsGuiVector4>
    {
        public PrefsVector4(
            string key, Vector4 defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Vector4, PrefsGuiVector4>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector4 gui)
            => gui.Initialize(this.GuiLabel, this.Get());
    }
}
