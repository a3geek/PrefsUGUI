using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Factories
{
    using PrefsBase = PrefsUGUI.Prefs.PrefsBase;

    public partial class PrefsCanvas
    {
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
            
            public PrefabType GetGui<PrefabType>(PrefsBase prefs, GuiStruct.Category category) where PrefabType : InputGuiBase
            {
                var gui = Instantiate(this.canvas.prefabs.GetGuiPrefab<PrefabType>(), category.Content);

                this.SetGuiListeners(prefs, gui);
                category.Prefs.Add(prefs, gui);

                return gui;
            }

            public GuiButton GetButton(GuiStruct.Category category, string label, string targetCategoryName)
            {
                var button = Instantiate(this.canvas.prefabs.Button, category.Content);
                button.Initialize(label, () => this.canvas.ChangeGUI(category, targetCategoryName));

                category.Buttons.Add(button);
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
