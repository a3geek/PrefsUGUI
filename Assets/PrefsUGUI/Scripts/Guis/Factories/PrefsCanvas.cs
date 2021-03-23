using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Classes;
    using Hierarchies.Abstracts;
    using Guis.Preferences;
    using PrefsUGUI.Preferences.Abstracts;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public class PrefsCanvas : MonoBehaviour
    {
        public const string TopHierarchyName = "";
        public const string TopHierarchyText = "Hierarchy...";

        public RectTransform Panel => this.links.Panel;
        public bool VisibleControllsGui
        {
            set
            {
                this.links.Close.gameObject.SetActive(value);
                this.links.Discard.gameObject.SetActive(value);
                this.links.Save.gameObject.SetActive(value);
            }
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
        private GuiHierarchiesStruct structs = null;
        private List<string> hierarchyTexts = new List<string>();


        private void Awake()
        {
            this.creator = new PrefsGuiCreator(this.links, this.prefabs);
            this.links.Content.gameObject.SetActive(false);

            var topContent = this.creator.CreateContent();
            this.structs = new GuiHierarchiesStruct(topContent, this.creator);
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

        public GuiType AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, IPrefsGuiEvents<ValType, GuiType> events)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var hierarchy = this.GetOrAddHierarchy(prefs.GuiHierarchy);
            var gui = this.creator.CreatePrefsGui(prefs, events, hierarchy);

            return gui;
        }

        public void RemovePrefs(ref Guid prefsId)
            => this.structs.RemovePrefs(ref prefsId);

        public AbstractGuiHierarchy GetOrAddHierarchy(Hierarchy hierarchy)
            => this.structs.GetOrCreateHierarchy(hierarchy, out var result) == true
                ? this.OnAddedHierarchy(result) : result;

        public AbstractGuiHierarchy GetOrAddHierarchy(LinkedHierarchy hierarchy)
            => this.structs.GetOrCreateHierarchy(hierarchy, out var result, hierarchy.LinkTarget) == true
                ? this.OnAddedHierarchy(result) : result;

        public void RemoveHierarchy(ref Guid hierarchyId)
        {
            var next = this.structs.RemoveHierarchy(ref hierarchyId);
            if(next != null)
            {
                this.ChangeGUI(next);
            }
        }

        private AbstractGuiHierarchy OnAddedHierarchy(AbstractGuiHierarchy hierarchy)
        {
            hierarchy.GuiButton.OnClicked += () => this.ChangeGUI(hierarchy);
            return hierarchy;
        }

        private void ChangeGUI(AbstractGuiHierarchy nextHierarchy)
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

        private void OnHierarchyChanged(AbstractGuiHierarchy hierarchy)
        {
            this.links.Scroll.content = hierarchy.Content;

            var isTop = this.SetHierarchy(hierarchy);
            this.SetButtonActive(isTop);
        }

        private bool SetHierarchy(AbstractGuiHierarchy hierarchy)
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
