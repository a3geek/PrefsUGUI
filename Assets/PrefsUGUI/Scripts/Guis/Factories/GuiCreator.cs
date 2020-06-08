using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Guis.Preferences;
    using Utilities;
    using PrefsBase = Prefs.PrefsBase;

    public partial class PrefsCanvas
    {
        private class Category
        {
            public RectTransform Content = null;
            public Dictionary<PrefsBase, PrefsGuiBase> Prefs = new Dictionary<PrefsBase, PrefsGuiBase>();

            public SortedList<PrefsGuiButton> Buttons = new SortedList<PrefsGuiButton>(
                (b1, b2) => string.Compare(b1.GetLabel(), b2.GetLabel())
            );

            public string CategoryName = "";
            public List<Category> Nexts = new List<Category>();
            public Category Previous = null;


            public void SetActive(bool active)
            {
                this.Content.gameObject.SetActive(active);
            }
        }

        private class GuiCreator
        {
            private PrefsCanvas canvas = null;


            public GuiCreator(PrefsCanvas canvas)
            {
                this.canvas = canvas;
                this.canvas.links.Content.gameObject.SetActive(false);
            }

            public RectTransform GetContent()
            {
                var content = Instantiate(this.canvas.links.Content, this.canvas.links.Viewport);
                content.gameObject.SetActive(false);

                return content;
            }

            public PrefsGuiButton GetButton(Category category, string label, string targetCategoryName, int sortOrder)
            {
                var button = Instantiate(this.canvas.prefabs.Button, category.Content);
                button.Initialize(label, () => this.canvas.ChangeGUI(category, targetCategoryName));

                var index = category.Buttons.Add(button, sortOrder);
                button.transform.SetSiblingIndex(index);

                return button;
            }

            public GuiType GetGui<ValType, GuiType>(Prefs.PrefsValueBase<ValType> prefs, Category category)
                where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
            {
                var gui = Instantiate(this.canvas.prefabs.GetGuiPrefab<ValType, GuiType>().Component, category.Content);

                gui.SetGuiListeners(prefs);

                void onPrefsValueChanged() => gui.SetValue(prefs.Value);
                prefs.OnValueChanged += onPrefsValueChanged;

                category.Prefs.Add(prefs, gui);
                return gui;
            }
        }
    }
}
