using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
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

        public PrefsGuiButton CreateButton(Category currentCategory, AbstractGuiHierarchy hierarchy, Category nextCategory, int sortOrder)
        {
            PrefsGuiButton button = Object.Instantiate(
                hierarchy.HierarchyType == HierarchyType.Standard ? this.prefabs.Button : this.prefabs.RemovableButton,
                currentCategory.Content
            );

            var index = currentCategory.AddNextCategory(nextCategory, button, sortOrder);
            button.transform.SetSiblingIndex(index);
            return button;
        }

        public GuiType CreatePrefsGui<ValType, GuiType>(PrefsValueBase<ValType> prefs, Category category)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var gui = Object.Instantiate(this.prefabs.GetGuiPrefab<ValType, GuiType>().Component, category.Content);

            gui.SetGuiListeners(prefs);

            void onPrefsValueChanged() => gui.SetValue(prefs.Get());
            prefs.OnValueChanged += onPrefsValueChanged;

            void OnDisposed() => category.OnDiscard -= prefs.Reload;
            prefs.OnDisposed += OnDisposed;
            category.OnDiscard += prefs.Reload;

            var index = category.AddPrefs(prefs.PrefsId, gui, prefs.GuiSortOrder);
            gui.transform.SetSiblingIndex(index);

            return gui;
        }
    }
}
