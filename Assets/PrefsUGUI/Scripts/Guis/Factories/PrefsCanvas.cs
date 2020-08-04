using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Classes;
    using Guis.Preferences;
    using PrefsUGUI.Preferences.Abstracts;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public partial class PrefsCanvas : MonoBehaviour
    {
        public const string TopCategoryName = "";
        public const string TopHierarchyText = "hierarchy...";

        public RectTransform Panel
        {
            get { return this.links.Panel; }
        }

        [SerializeField]
        private Color topHierarchyColor = Color.gray;
        [SerializeField]
        private Color untopHierarchyColor = Color.white;
        [SerializeField]
        private CanvasLinks links = new CanvasLinks();
        [SerializeField]
        private PrefsGuiPrefabs prefabs = new PrefsGuiPrefabs();

        private PrefsGuiCreator creator = null;
        private CategoriesStruct structs = null;


        private void Awake()
        {
            this.creator = new PrefsGuiCreator(this.links, this.prefabs);
            this.links.Content.gameObject.SetActive(false);

            var topContent = this.creator.CreateContent();
            this.structs = new CategoriesStruct(topContent, this.creator);
            this.OnCategoryChanged(this.structs.Current);

            this.links.Close.onClick.AddListener(this.OnClickedCloseButton);
            this.links.Discard.onClick.AddListener(this.OnClickedDiscardButton);
            this.links.Save.onClick.AddListener(this.OnClickedSaveButton);
        }

        private void Reset()
        {
            this.links.Reset(this.gameObject);
        }

        private void OnValidate()
        {
            this.prefabs.OnValidate();
        }

        public GuiType AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var category = this.structs.GetOrCreateCategory(prefs.GuiHierarchy);
            var gui = this.creator.CreatePrefsGui<ValType, GuiType>(prefs, category);

            return gui;
        }

        public void RemovePrefs(ref Guid prefsId)
            => this.structs.RemovePrefs(ref prefsId);

        public Category AddCategory(AbstractGuiHierarchy hierarchy)
            => this.structs.GetOrCreateCategory(hierarchy);

        public void RemoveCategory(ref Guid categoryId)
            => this.ChangeGUI(this.structs.RemoveCategory(ref categoryId));

        public void ChangeGUI(Category nextCategory, string hierarchyCategoryName = null)
            => this.OnCategoryChanged(this.structs.ChangeGUI(nextCategory), hierarchyCategoryName);

        private void OnClickedDiscardButton()
            => this.structs.Current.Discard();

        private void OnClickedCloseButton()
            => this.OnCategoryChanged(this.structs.ChangeGUI(this.structs.Current.Previous));

        private void OnClickedSaveButton()
        {
            Prefs.Save();
            this.gameObject.SetActive(false);
        }

        private void OnCategoryChanged(Category category, string hierarchyCategoryName = null)
        {
            this.links.Scroll.content = category.Content;

            var isTop = this.SetHierarchy(category, hierarchyCategoryName);
            this.SetButtonActive(isTop);
        }

        private bool SetHierarchy(Category category, string hierarchyCategoryName = null)
        {
            var hierarchy = hierarchyCategoryName ?? category.CategoryName;
            var previous = category.Previous;
            while (previous != null)
            {
                hierarchy = previous.CategoryName + Prefs.HierarchySeparator + hierarchy;
                previous = previous.Previous;
            }
            var isTop = category.CategoryId == this.structs.Top.CategoryId;

            this.links.Hierarchy.color = isTop == true ? this.topHierarchyColor : this.untopHierarchyColor;
            this.links.Hierarchy.fontStyle = isTop == true ? FontStyle.Italic : FontStyle.Normal;
            this.links.Hierarchy.text = isTop == true
                ? TopHierarchyText
                : hierarchy.TrimStart(Prefs.HierarchySeparator) + Prefs.HierarchySeparator;

            return isTop;
        }

        private void SetButtonActive(bool isTop)
        {
            if (isTop == true)
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
    }
}
