using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Classes;
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
        
        private GuiCreator creator = null;
        private GuiStruct structs = null;


        private void Awake()
        {
            this.creator = new GuiCreator(this);
            this.structs = new GuiStruct(this.creator.GetContent(), this.creator);

            this.links.Close.onClick.AddListener(this.OnClickedCloseButton);
            this.links.Discard.onClick.AddListener(this.OnClickedDiscardButton);
            this.links.Save.onClick.AddListener(this.OnClickedSaveButton);
            
            this.SetButtonActive(true);
        }
        
        public PrefabType AddPrefs<PrefabType>(PrefsBase prefs) where PrefabType : InputGuiBase
        {
            var category = this.structs.GetCategory(prefs.GuiHierarchy);
            var gui = this.creator.GetGui<PrefabType>(prefs, category);

            return gui;
        }

        public void RemovePrefs(PrefsBase prefs)
        {
            var categories = this.structs.Categories;

            for(var i = 0; i < categories.Count; i++)
            {
                var dic = categories[i].Prefs;

                if(dic.ContainsKey(prefs) == true)
                {
                    var gui = dic[prefs].gameObject;

                    dic.Remove(prefs);
                    Destroy(gui);

                    return;
                }
            }
        }

        private void ChangeGUI(GuiStruct.Category previous, string targetCategoryName)
        {
            this.SetScroll(this.structs.ChangeGUI(previous, targetCategoryName));
        }
        
        private void OnClickedDiscardButton()
        {
            foreach(var prefs in this.structs.Current.Prefs)
            {
                prefs.Key.Reload();
            }
        }
        
        private void OnClickedCloseButton()
        {
            this.SetScroll(this.structs.ChangeGUI(this.structs.Current.Previous));
        }

        private void OnClickedSaveButton()
        {
            Prefs.Save();
            gameObject.SetActive(false);
        }

        private void SetScroll(GuiStruct.Category category)
        {
            this.links.Scroll.content = category.Content;
            this.SetButtonActive(this.structs.Current.CategoryName == TopCategoryName);
        }

        private void SetButtonActive(bool isTop)
        {
            if(isTop == true)
            {
                this.links.Close.gameObject.SetActive(false);
                this.links.Save.gameObject.SetActive(true);
            }
            else
            {
                this.links.Close.gameObject.SetActive(true);
                this.links.Save.gameObject.SetActive(false);
            }
        }
        
        private void Reset()
        {
            this.links.Reset(gameObject);
        }
    }
}
