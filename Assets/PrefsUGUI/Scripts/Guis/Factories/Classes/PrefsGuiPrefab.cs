using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsGuiPrefab
    {
        public string Key => this.key;
        public PrefsGuiBase Prefab => this.prefab;
        public PrefsGuiType GuiType => this.guiType;

        [SerializeField]
        private string key = "";
        [SerializeField]
        private PrefsGuiBase prefab = null;
        [SerializeField]
        private PrefsGuiType guiType = PrefsGuiType.None;


        public PrefsGuiPrefab(PrefsGuiType guiType)
        {
            this.guiType = guiType;
            this.key = guiType.ToString();
        }

        public void OnValidate()
        {
            this.key = this.guiType.ToString();
        }
    }
}
