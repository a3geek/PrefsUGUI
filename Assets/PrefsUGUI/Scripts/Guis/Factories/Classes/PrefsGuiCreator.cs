using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Hierarchies.Abstracts;
    using Guis.Preferences;
    using PrefsUGUI.Preferences.Abstracts;
    using Object = UnityEngine.Object;

    public class PrefsGuiCreator
    {
        private CanvasLinks links = null;
        private PrefsGuiPrefabs prefabs = null;


        public PrefsGuiCreator(CanvasLinks links, PrefsGuiPrefabs prefabs)
        {
            this.links = links;
            this.prefabs = prefabs;
        }

        public RectTransform CreateContent()
        {
            var content = Object.Instantiate(this.links.Content, this.links.Viewport);
            content.gameObject.SetActive(false);

            return content;
        }

        public PrefsGuiButton CreateButton(AbstractGuiHierarchy current, AbstractHierarchy hierarchy, AbstractGuiHierarchy next, int sortOrder)
        {
            var isRemovable = hierarchy.HierarchyType == HierarchyType.Removable;
            var button = Object.Instantiate(
                isRemovable ? this.prefabs.RemovableButton : this.prefabs.Button,
                current.Content
            );

            var index = current.AddNextHierarchy(next, button, sortOrder);
            button.transform.SetSiblingIndex(index);
            return button;
        }

        public GuiType CreatePrefsGui<ValType, GuiType>(PrefsGuiBase<ValType, GuiType> prefs, IPrefsGuiEvents<ValType, GuiType> events, AbstractGuiHierarchy hierarchy)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var gui = Object.Instantiate(this.prefabs.GetGuiPrefab<ValType, GuiType>().Component, hierarchy.Content);

            gui.SetGuiListeners(prefs, events, hierarchy);

            var index = hierarchy.AddPrefs(prefs.PrefsId, gui, prefs.GuiSortOrder);
            gui.transform.SetSiblingIndex(index);

            return gui;
        }
    }
}
