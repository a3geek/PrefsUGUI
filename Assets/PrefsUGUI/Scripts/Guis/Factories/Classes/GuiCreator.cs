using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Guis.Preferences;

    public class GuiCreator
    {
        private PrefsCanvas canvas = null;
        private CanvasLinks links = null;
        private PrefsGuiPrefabs prefabs = null;


        public GuiCreator(PrefsCanvas canvas, CanvasLinks links, PrefsGuiPrefabs prefabs)
        {
            this.canvas = canvas;
            this.links = links;
            this.prefabs = prefabs;
        }

        public RectTransform GetContent()
        {
            var content = Object.Instantiate(this.links.Content, this.links.Viewport);
            content.gameObject.SetActive(false);

            return content;
        }

        public PrefsGuiButton GetButton(Category category, string label, string targetCategoryName, int sortOrder)
        {
            void onButtonClicked() => this.canvas.ChangeGUI(category, targetCategoryName);

            var button = Object.Instantiate(this.prefabs.Button, category.Content);
            button.Initialize(label, onButtonClicked);

            var index = category.Buttons.Add(button, sortOrder);
            button.transform.SetSiblingIndex(index);

            return button;
        }

        public GuiType GetGui<ValType, GuiType>(Prefs.PrefsValueBase<ValType> prefs, Category category)
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
