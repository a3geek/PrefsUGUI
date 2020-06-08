using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    [Serializable]
    public class GuiPrefabs
    {
        [Serializable]
        public class GuiPrefab
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


            public GuiPrefab(PrefsGuiType guiType)
            {
                this.guiType = guiType;
                this.key = guiType.ToString();
            }

            public void Reset()
            {
                this.key = this.guiType.ToString();
            }
        }


        public PrefsGuiButton Button => this.buttonPrefab;

        [SerializeField]
        private PrefsGuiButton buttonPrefab = null;
        [SerializeField]
        private GuiPrefab[] guiPrefabs = new GuiPrefab[]
        {
            new GuiPrefab(PrefsGuiType.PrefsGuiBool), new GuiPrefab(PrefsGuiType.PrefsGuiColor),
            new GuiPrefab(PrefsGuiType.PrefsGuiColorSlider), new GuiPrefab(PrefsGuiType.PrefsGuiEnum),
            new GuiPrefab(PrefsGuiType.PrefsGuiNumericDecimal), new GuiPrefab(PrefsGuiType.PrefsGuiNumericInteger),
            new GuiPrefab(PrefsGuiType.PrefsGuiNumericSliderDecimal), new GuiPrefab(PrefsGuiType.PrefsGuiNumericSliderInteger),
            new GuiPrefab(PrefsGuiType.PrefsGuiString), new GuiPrefab(PrefsGuiType.PrefsGuiVector2),
            new GuiPrefab(PrefsGuiType.PrefsGuiVector2Int), new GuiPrefab(PrefsGuiType.PrefsGuiVector3),
            new GuiPrefab(PrefsGuiType.PrefsGuiVector3Int), new GuiPrefab(PrefsGuiType.PrefsGuiVector4),
            new GuiPrefab(PrefsGuiType.PrefsGuiRect), new GuiPrefab(PrefsGuiType.PrefsGuiButton),
            new GuiPrefab(PrefsGuiType.PrefsGuiLabel), new GuiPrefab(PrefsGuiType.PrefsGuiImageLabel)
        };


        public GuiType GetGuiPrefab<ValType, GuiType>()
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var guiType = PrefsGuiTypeExtentions.GetPrefsGuiTypeByComponentType(typeof(GuiType));

            for(var i = 0; i < this.guiPrefabs.Length; i++)
            {
                if(this.guiPrefabs[i].GuiType == guiType)
                {
                    return (GuiType)this.guiPrefabs[i].Prefab;
                }
            }

            return null;
        }

        public void Reset()
        {
            for(var i = 0; i < this.guiPrefabs.Length; i++)
            {
                this.guiPrefabs[i].Reset();
            }
        }
    }
}
