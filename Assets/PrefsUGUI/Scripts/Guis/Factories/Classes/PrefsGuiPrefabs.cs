using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsGuiPrefabs
    {
        public PrefsGuiButton Button => this.buttonPrefab;
        public PrefsGuiRemovableButton RemovableButton => this.removableButtonPrefab;

        [SerializeField]
        private PrefsGuiButton buttonPrefab = null;
        [SerializeField]
        private PrefsGuiRemovableButton removableButtonPrefab = null;
        [SerializeField]
        private PrefsGuiPrefab[] guiPrefabs = new PrefsGuiPrefab[]
        {
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiBool), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiColor),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiColorSlider), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiEnum),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiNumericDecimal), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiNumericInteger),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiNumericSliderDecimal), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiNumericSliderInteger),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiString), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiVector2),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiVector2Int), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiVector3),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiVector3Int), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiVector4),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiRect), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiButton),
            new PrefsGuiPrefab(PrefsGuiType.PrefsGuiLabel), new PrefsGuiPrefab(PrefsGuiType.PrefsGuiImageLabel)
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

        public void OnValidate()
        {
            for(var i = 0; i < this.guiPrefabs.Length; i++)
            {
                this.guiPrefabs[i].OnValidate();
            }
        }
    }
}
