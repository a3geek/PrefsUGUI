﻿using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsVector3Int : PrefsGuiBase<Vector3Int, PrefsGuiVector3Int>
    {
        public PrefsVector3Int(
            string key, Vector3Int defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<Vector3Int, PrefsGuiVector3Int>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        protected override void OnCreatedGuiInternal(PrefsGuiVector3Int gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
        }
    }
}
