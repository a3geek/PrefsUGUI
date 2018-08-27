using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;
    using Prefs = PrefsUGUI.Prefs;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public partial class PrefsCanvas : MonoBehaviour
    {
        public const string TopCategoryName = "";
        
        public RectTransform Panel
        {
            get { return this.links.Panel; }
        }
        
        [SerializeField]
        private GuiLinks links = new GuiLinks();
        [SerializeField]
        private GuiPrefabs prefabs = new GuiPrefabs();

        private GuiList guis = null;


        private void Awake()
        {
            this.guis = new GuiList(this);

            this.links.Close.onClick.AddListener(this.OnClickedCloseButton);
            this.links.Discard.onClick.AddListener(this.OnClickedDiscardButton);
            this.links.Save.onClick.AddListener(this.OnClickedSaveButton);
        }
        
        public PrefabType AddPrefs<PrefabType>(PrefsBase prefs) where PrefabType : InputGuiBase
        {
            var hierarchy = prefs.GuiHierarchy.TrimEnd(Prefs.HierarchySeparator);
            var split = string.IsNullOrEmpty(hierarchy) == true ?
                new string[0] :
                hierarchy.Split(Prefs.HierarchySeparator);
            
            var previous = TopCategoryName;
            for(var i = 0; i < split.Length; i++)
            {
                var categoryName = split[i];
                this.guis.GetButton(this.guis.GetCategory(previous, i > 1 ? split[i - 2] : TopCategoryName), categoryName);
                previous = categoryName;
            }

            var category = this.guis.GetCategory(previous, split.Length > 1 ? split[split.Length - 2] : TopCategoryName);
            var gui = Instantiate(this.prefabs.GetGuiPrefab<PrefabType>(), category.Content);

            this.SetGuiListeners(prefs, gui);
            category.Prefs.Add(prefs, gui);
            
            return gui;
        }

        public void RemovePrefs(PrefsBase prefs)
        {
            foreach(var pair1 in this.guis.Categories)
            {
                var prefsDic = pair1.Value.Prefs;

                if(prefsDic.ContainsKey(prefs) == true)
                {
                    Destroy(prefsDic[prefs].gameObject);
                    prefsDic.Remove(prefs);

                    return;
                }
            }
        }

        private void SetGuiListeners<PrefabType>(PrefsBase prefs, PrefabType gui) where PrefabType : InputGuiBase
        {
            gui.OnPressedDefaultButton += () => prefs.ResetDefaultValue();
            gui.OnValueChanged += () => prefs.ValueAsObject = gui.GetValueObject();

            prefs.OnValueChanged += () => gui.SetValue(prefs.ValueAsObject);
        }

        private void OnClickedDiscardButton()
        {
            foreach(var prefs in this.guis.Current.Prefs)
            {
                prefs.Key.Reload();
            }
        }
        
        private void OnClickedCloseButton()
        {
            this.guis.ChangeGUI(this.guis.Current.ParentCategoryName);
        }

        private void OnClickedSaveButton()
        {
            Prefs.Save();
            gameObject.SetActive(false);
        }
        
        private void Reset()
        {
            this.links.Reset(gameObject);
        }
    }
}
