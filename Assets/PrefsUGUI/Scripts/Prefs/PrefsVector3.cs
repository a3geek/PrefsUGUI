using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsVector3 : Prefs.PrefsExtends<Vector3, PrefsGuiVector3>
    {
        public PrefsVector3(string key, Vector3 defaultValue = default(Vector3), GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBaseConnector<Vector3, PrefsGuiVector3>> onCreatedGui = null)
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui) { }

        protected override void OnCreatedGuiInternal(PrefsGuiVector3 gui)
        {
            gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
        }
    }
}
