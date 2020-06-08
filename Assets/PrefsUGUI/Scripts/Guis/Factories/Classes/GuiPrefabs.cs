using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    [Serializable]
    public class GuiPrefab
    {
        public Type Type => this.type ?? Type.GetType(this.key);
        public string Key => this.key;
        public PrefsGuiBase Prefab => this.prefab;

        [SerializeField]
        private string key = "";
        [SerializeField]
        private PrefsGuiBase prefab = null;
        [SerializeField]
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
        public PrefsGuiButton Button => this.buttonPrefab;

        [SerializeField]
        private PrefsGuiButton buttonPrefab = null;
        [SerializeField]
        private GuiPrefab[] guiPrefabs = new GuiPrefab[]
        {
            new GuiPrefab(typeof(PrefsGuiBool)), new GuiPrefab(typeof(PrefsGuiColor)),
            new GuiPrefab(typeof(PrefsGuiColorSlider)), new GuiPrefab(typeof(PrefsGuiEnum)),
            new GuiPrefab(typeof(PrefsGuiNumericDecimal)), new GuiPrefab(typeof(PrefsGuiNumericInteger)),
            new GuiPrefab(typeof(PrefsGuiNumericSliderDecimal)), new GuiPrefab(typeof(PrefsGuiNumericSliderInteger)),
            new GuiPrefab(typeof(PrefsGuiString)), new GuiPrefab(typeof(PrefsGuiVector2)),
            new GuiPrefab(typeof(PrefsGuiVector2Int)), new GuiPrefab(typeof(PrefsGuiVector3)),
            new GuiPrefab(typeof(PrefsGuiVector3Int)), new GuiPrefab(typeof(PrefsGuiVector4)),
            new GuiPrefab(typeof(PrefsGuiRect)), new GuiPrefab(typeof(PrefsGuiButton)),
            new GuiPrefab(typeof(PrefsGuiLabel)), new GuiPrefab(typeof(PrefsGuiImageLabel))
        };


        public GuiType GetGuiPrefab<ValType, GuiType>()
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            for(var i = 0; i < this.guiPrefabs.Length; i++)
            {
                if(this.guiPrefabs[i].Type == typeof(GuiType))
                {
                    return (GuiType)this.guiPrefabs[i].Prefab;
                }
            }

            return null;
        }
    }
}
