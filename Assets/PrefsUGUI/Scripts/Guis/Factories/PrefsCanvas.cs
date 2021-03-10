using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Classes;
    using GuiHierarchies.Abstracts;
    using Guis.Preferences;
    using PrefsUGUI.Preferences.Abstracts;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public partial class PrefsCanvas : MonoBehaviour
    {
        public const string TopHierarchyName = "";
        public const string TopHierarchyText = "Hierarchy...";

        public RectTransform Panel => this.links.Panel;

        [SerializeField]
        private Color topHierarchyColor = Color.gray;
        [SerializeField]
        private Color untopHierarchyColor = Color.white;
        [SerializeField]
        private CanvasLinks links = new CanvasLinks();
        [SerializeField]
        private PrefsGuiPrefabs prefabs = new PrefsGuiPrefabs();

        private PrefsGuiCreator creator = null;
        private HierarchiesStruct structs = null;
        private List<string> hierarchyTexts = new List<string>();


        private void Awake()
        {
            this.creator = new PrefsGuiCreator(this.links, this.prefabs);
            this.links.Content.gameObject.SetActive(false);

            var topContent = this.creator.CreateContent();
            this.structs = new HierarchiesStruct(topContent, this.creator);
            this.OnHierarchyChanged(this.structs.Current);

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
            var hierarchy = this.structs.GetOrCreateHierarchy(prefs.GuiHierarchy);
            var gui = this.creator.CreatePrefsGui<ValType, GuiType>(prefs, hierarchy);

            return gui;
        }

        public void RemovePrefs(ref Guid prefsId)
            => this.structs.RemovePrefs(ref prefsId);

        public AbstractHierarchy GetOrAddHierarchy(GuiHierarchy hierarchy)
            => this.structs.GetOrCreateHierarchy(hierarchy);

        public AbstractHierarchy GetOrAddHierarchy(LinkedGuiHierarchy hierarchy)
            => this.structs.GetOrCreateHierarchy(hierarchy, hierarchy.LinkTarget);

        public void RemoveHierarchy(ref Guid hierarchyId)
        {
            var next = this.structs.RemoveHierarchy(ref hierarchyId);
            if(next != null)
            {
                this.ChangeGUI(next);
            }
        }

        public void ChangeGUI(AbstractHierarchy nextHierarchy)
        {
            this.hierarchyTexts.Add(nextHierarchy.HierarchyName);
            this.OnHierarchyChanged(this.structs.ChangeGUI(nextHierarchy));
        }

        private void OnClickedDiscardButton()
            => this.structs.Current.Discard();

        private void OnClickedCloseButton()
        {
            this.hierarchyTexts.RemoveAt(this.hierarchyTexts.Count - 1);
            this.OnHierarchyChanged(this.structs.ChangeGUI(this.structs.Current.Previous));
        }

        private void OnClickedSaveButton()
        {
            Prefs.Save();
            this.gameObject.SetActive(false);
        }

        private void OnHierarchyChanged(AbstractHierarchy hierarchy)
        {
            this.links.Scroll.content = hierarchy.Content;

            var isTop = this.SetHierarchy(hierarchy);
            this.SetButtonActive(isTop);
        }

        private bool SetHierarchy(AbstractHierarchy hierarchy)
        {
            var hierarchyName = "";
            for(var i = 0; i < this.hierarchyTexts.Count; i++)
            {
                hierarchyName += this.hierarchyTexts[i] + Prefs.HierarchySeparator;
            }

            var isTop = hierarchy.HierarchyId == this.structs.Top.HierarchyId;
            this.links.Hierarchy.color = isTop == true ? this.topHierarchyColor : this.untopHierarchyColor;
            this.links.Hierarchy.fontStyle = isTop == true ? FontStyle.Italic : FontStyle.Normal;
            this.links.Hierarchy.text = isTop == true
                ? TopHierarchyText
                : hierarchyName;

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
    }
}
