using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Utilities;
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    public partial class PrefsCanvas
    {
        private class Category
        {
            public RectTransform Content = null;
            public Dictionary<PrefsBase, GuiBase> Prefs = new Dictionary<PrefsBase, GuiBase>();

            public SortedList<GuiButton> Buttons = new SortedList<GuiButton>(
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

            public PrefabType GetGui<PrefabType>(PrefsBase prefs, Category category) where PrefabType : InputGuiBase
            {
                var gui = Instantiate(this.canvas.prefabs.GetGuiPrefab<PrefabType>(), category.Content);

                this.SetGuiListeners(prefs, gui);
                category.Prefs.Add(prefs, gui);

                return gui;
            }

            public GuiButton GetButton(Category category, string label, string targetCategoryName, int sortOrder)
            {
                var button = Instantiate(this.canvas.prefabs.Button, category.Content);
                button.Initialize(label, () => this.canvas.ChangeGUI(category, targetCategoryName));

                var index = category.Buttons.Add(button, sortOrder);
                button.transform.SetSiblingIndex(index);

                return button;
            }

            private void SetGuiListeners<PrefabType>(PrefsBase prefs, PrefabType gui) where PrefabType : InputGuiBase
            {
                gui.OnPressedDefaultButton += () => prefs.ResetDefaultValue();
                gui.OnValueChanged += () => prefs.ValueAsObject = gui.GetValueObject();

                prefs.OnValueChanged += () => gui.SetValue(prefs.ValueAsObject);
            }
        }
    }
}
