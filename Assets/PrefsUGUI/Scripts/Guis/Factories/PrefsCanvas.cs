using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Classes;
    using Guis.Prefs;

    using Prefs = PrefsUGUI.Prefs;
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

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
        private GuiLinks links = new GuiLinks();
        [SerializeField]
        private GuiPrefabs prefabs = new GuiPrefabs();

        private GuiCreator creator = null;
        private GuiStruct structs = null;


        private void Awake()
        {
            this.creator = new GuiCreator(this);

            var topContent = this.creator.GetContent();
            this.structs = new GuiStruct(topContent, this.creator);
            this.OnGuiChanged(this.structs.Current);

            this.links.Close.onClick.AddListener(this.OnClickedCloseButton);
            this.links.Discard.onClick.AddListener(this.OnClickedDiscardButton);
            this.links.Save.onClick.AddListener(this.OnClickedSaveButton);
        }

        public PrefabType AddPrefs<ValType, PrefabType>(Prefs.PrefsValueBase<ValType> prefs) where PrefabType : PrefsInputGuiBase<ValType>
        {
            var category = this.structs.GetCategory(prefs.GuiHierarchy);
            var gui = this.creator.GetGui<ValType, PrefabType>(prefs, category);

            return gui;
        }

        public PrefabType AddPrefs<PrefabType>(PrefsBase prefs) where PrefabType : PrefsGuiBase
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

        public void RemoveCategory(GuiHierarchy hierarchy)
            => this.structs.RemoveCategory(hierarchy);

        private void ChangeGUI(Category previous, string targetCategoryName)
            => this.OnGuiChanged(this.structs.ChangeGUI(previous, targetCategoryName));

        private void OnClickedDiscardButton()
        {
            foreach(var prefs in this.structs.Current.Prefs)
            {
                prefs.Key.Reload();
            }
        }

        private void OnClickedCloseButton() => this.OnGuiChanged(this.structs.ChangeGUI(this.structs.Current.Previous));

        private void OnClickedSaveButton()
        {
            Prefs.Save();
            gameObject.SetActive(false);
        }

        private void OnGuiChanged(Category category)
        {
            this.links.Scroll.content = category.Content;

            var isTop = this.SetHierarchy(category);
            this.SetButtonActive(isTop);
        }

        private bool SetHierarchy(Category category)
        {
            var hierarchy = category.CategoryName;
            var previous = category.Previous;
            while(previous != null)
            {
                hierarchy = previous.CategoryName + Prefs.HierarchySeparator + hierarchy;
                previous = previous.Previous;
            }
            var isTop = string.IsNullOrEmpty(hierarchy);

            this.links.Hierarchy.color = isTop == true ? this.topHierarchyColor : this.untopHierarchyColor;
            this.links.Hierarchy.fontStyle = isTop == true ? FontStyle.Italic : FontStyle.Normal;
            this.links.Hierarchy.text = isTop == true ?
                TopHierarchyText :
                hierarchy.TrimStart(Prefs.HierarchySeparator) + Prefs.HierarchySeparator;

            return isTop;
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

        private void Reset() => this.links.Reset(gameObject);
    }
}
