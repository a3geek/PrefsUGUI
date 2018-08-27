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
        private class GuiList
        {
            public class GuiCategory
            {
                public string CategoryName = "";
                public string ParentCategoryName = "";

                public RectTransform Content = null;
                public Dictionary<PrefsBase, GuiBase> Prefs = new Dictionary<PrefsBase, GuiBase>();
                public List<GuiButton> Buttons = new List<GuiButton>();
            }

            public GuiCategory Current
            {
                get; private set;
            }
            public Dictionary<string, GuiCategory> Categories
            {
                get; private set;
            }

            private PrefsCanvas canvas = null;


            public GuiList(PrefsCanvas canvas, GuiCategory topGui = null)
            {
                this.canvas = canvas;
                this.Categories = new Dictionary<string, GuiCategory>();

                var top = topGui ?? new GuiCategory()
                {
                    CategoryName = TopCategoryName,
                    ParentCategoryName = TopCategoryName,
                    Content = this.canvas.links.Content
                };
                this.Categories.Add(top.CategoryName, top);
                this.SetButtonsActive(true);

                this.Current = top;
            }

            public GuiCategory GetCategory(string categoryName, string parentCategoryName, RectTransform content = null)
            {
                if(this.Categories.ContainsKey(categoryName) == true)
                {
                    return this.Categories[categoryName];
                }

                var category = new GuiCategory()
                {
                    CategoryName = categoryName,
                    ParentCategoryName = parentCategoryName,
                    Content = content ?? Instantiate(this.canvas.prefabs.Content, this.canvas.links.Viewport)
                };
                category.Content.gameObject.SetActive(false);

                this.Categories.Add(categoryName, category);
                return category;
            }

            public GuiButton GetButton(GuiCategory category, string categoryName)
            {
                foreach(var b in category.Buttons)
                {
                    if(categoryName == b.GetLabel())
                    {
                        return b;
                    }
                }

                var button = Instantiate(this.canvas.prefabs.Button, category.Content);
                button.Initialize(categoryName, () => this.ChangeGUI(categoryName));

                category.Buttons.Add(button);
                return button;
            }

            public void ChangeGUI(string categoryName)
            {
                if(this.Categories.ContainsKey(categoryName) == false)
                {
                    return;
                }

                if(this.Current != null)
                {
                    this.Current.Content.gameObject.SetActive(false);
                }
                this.Current = this.Categories[categoryName];

                this.canvas.links.Scroll.content = this.Current.Content;
                this.Current.Content.gameObject.SetActive(true);

                this.SetButtonsActive(categoryName == TopCategoryName);
            }

            private void SetButtonsActive(bool isTop)
            {
                if(isTop == true)
                {
                    this.canvas.links.Close.gameObject.SetActive(false);
                    this.canvas.links.Save.gameObject.SetActive(true);
                }
                else
                {
                    this.canvas.links.Close.gameObject.SetActive(true);
                    this.canvas.links.Save.gameObject.SetActive(false);
                }
            }
        }
    }
}
