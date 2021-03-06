﻿using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsRect : Prefs.PrefsExtends<Rect, PrefsGuiRect>
    {
        public PrefsRect(string key, Rect defaultValue = default(Rect), GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBaseConnector<Rect, PrefsGuiRect>> onCreatedGui = null)
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui) { }

        protected override void OnCreatedGuiInternal(PrefsGuiRect gui)
            => gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
    }
}
