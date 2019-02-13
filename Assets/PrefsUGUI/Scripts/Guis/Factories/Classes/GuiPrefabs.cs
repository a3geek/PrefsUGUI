using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Prefs;

    [Serializable]
    public class GuiPrefab
    {
        public Type Type => this.type;
        public string Key => this.key;
        public InputGuiBase Prefab => this.prefab;

        [SerializeField]
        private string key = "";
        [SerializeField]
        private InputGuiBase prefab = null;
        [SerializeField, HideInInspector]
        private Type type = null;


        public GuiPrefab(Type type)
        {
            this.type = type;
            this.key = type.Name;
        }
    }

    [Serializable]
    public class GuiPrefabs
    {
        public GuiButton Button => this.buttonPrefab;

        [SerializeField]
        private GuiButton buttonPrefab = null;
        [SerializeField]
        private GuiPrefab[] guiPrefabs = new GuiPrefab[]
        {
            new GuiPrefab(typeof(PrefsGuiBool)), new GuiPrefab(typeof(PrefsGuiColor)), new GuiPrefab(typeof(PrefsGuiColorSlider)),
            new GuiPrefab(typeof(PrefsGuiEnum)), new GuiPrefab(typeof(PrefsGuiNumeric)), new GuiPrefab(typeof(PrefsGuiNumericSlider)),
            new GuiPrefab(typeof(PrefsGuiString)), new GuiPrefab(typeof(PrefsGuiVector2)),
            new GuiPrefab(typeof(PrefsGuiVector3)), new GuiPrefab(typeof(PrefsGuiVector4)),
        };


        public PrefabType GetGuiPrefab<PrefabType>() where PrefabType : InputGuiBase
        {
            for(var i = 0; i < this.guiPrefabs.Length; i++)
            {
                if(this.guiPrefabs[i].Type == typeof(PrefabType))
                {
                    return (PrefabType)this.guiPrefabs[i].Prefab;
                }
            }

            return null;
        }
    }
}
