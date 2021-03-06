﻿using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector3Int : Prefs.PrefsExtends<Vector3Int, PrefsGuiVector3Int>
    {
        public PrefsVector3Int(string key, Vector3Int defaultValue = default(Vector3Int), GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBaseConnector<Vector3Int, PrefsGuiVector3Int>> onCreatedGui = null)
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui) { }

        protected override void OnCreatedGuiInternal(PrefsGuiVector3Int gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
