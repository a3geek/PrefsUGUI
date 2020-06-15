using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    public class PrefsGuiCreator
    {
        private PrefsCanvas canvas = null;
        private CanvasLinks links = null;
        private PrefsGuiPrefabs prefabs = null;


        public PrefsGuiCreator(PrefsCanvas canvas, CanvasLinks links, PrefsGuiPrefabs prefabs)
        {
            this.canvas = canvas;
            this.links = links;
            this.prefabs = prefabs;
        }

        public RectTransform CreateContent()
        {
            var content = Object.Instantiate(this.links.Content, this.links.Viewport);
            content.gameObject.SetActive(false);

            return content;
        }

        public PrefsGuiButton CreateButton(Category currentCategory, string label, Category nextCategory, int sortOrder)
        {
            void onButtonClicked() => this.canvas.ChangeGUI(nextCategory);

            var button = Object.Instantiate(this.prefabs.Button, currentCategory.Content);
            button.Initialize(label, onButtonClicked);

            var index = currentCategory.AddNextCategory(nextCategory, button, sortOrder);
            button.transform.SetSiblingIndex(index);

            return button;
        }

        public GuiType CreatePrefsGui<ValType, GuiType>(Prefs.PrefsValueBase<ValType> prefs, Category category)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            var gui = Object.Instantiate(this.prefabs.GetGuiPrefab<ValType, GuiType>().Component, category.Content);

            gui.SetGuiListeners(prefs);

            void onPrefsValueChanged() => gui.SetValue(prefs.Value);
            prefs.OnValueChanged += onPrefsValueChanged;

            category.PrefsList.Add((prefs, gui), prefs.GuiSortOrder);
            return gui;
        }
    }
}
