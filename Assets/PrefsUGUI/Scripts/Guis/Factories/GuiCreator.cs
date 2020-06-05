using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    using Guis.Prefs;
    using Utilities;

    using Prefs = PrefsUGUI.Prefs;
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    public partial class PrefsCanvas
    {
        private class Category
        {
            public RectTransform Content = null;
            public Dictionary<PrefsBase, PrefsGuiBase> Prefs = new Dictionary<PrefsBase, PrefsGuiBase>();

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

            public GuiButton GetButton(Category category, string label, string targetCategoryName, int sortOrder)
            {
                var button = Instantiate(this.canvas.prefabs.Button, category.Content);
                button.Initialize(label, () => this.canvas.ChangeGUI(category, targetCategoryName));

                var index = category.Buttons.Add(button, sortOrder);
                button.transform.SetSiblingIndex(index);

                return button;
            }

            public PrefabType GetGui<ValType, PrefabType>(Prefs.PrefsValueBase<ValType> prefs, Category category) where PrefabType : PrefsInputGuiBase<ValType>
            {
                var gui = Instantiate(this.canvas.prefabs.GetGuiPrefab<PrefabType>(), category.Content);

                this.SetGuiListeners(prefs, gui);
                category.Prefs.Add(prefs, gui);

                return gui;
            }

            public PrefabType GetGui<PrefabType>(PrefsBase prefs, Category category) where PrefabType : PrefsGuiBase
            {
                var gui = Instantiate(this.canvas.prefabs.GetGuiPrefab<PrefabType>(), category.Content);
                category.Prefs.Add(prefs, gui);

                return gui;
            }

            private void SetGuiListeners<ValType, PrefabType>(Prefs.PrefsValueBase<ValType> prefs, PrefabType gui) where PrefabType : PrefsInputGuiBase<ValType>
            {
                gui.SetGuiListeners(prefs);
                prefs.OnValueChanged += () => gui.SetValue(prefs.Value);
            }
        }
    }
}
