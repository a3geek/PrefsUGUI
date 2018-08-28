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
        private class GuiStruct
        {
            public class Category
            {
                public RectTransform Content = null;
                public Dictionary<PrefsBase, GuiBase> Prefs = new Dictionary<PrefsBase, GuiBase>();
                public List<GuiButton> Buttons = new List<GuiButton>();

                public string CategoryName = "";
                public List<Category> Nexts = new List<Category>();
                public Category Previous = null;


                public void SetActive(bool active)
                {
                    this.Content.gameObject.SetActive(active);
                }
            }
            
            public Category Current { get; private set; }
            public List<Category> Categories
            {
                get { return this.categories; }
            }

            private readonly char separator = PrefsUGUI.Prefs.HierarchySeparator;

            private GuiCreator creator = null;
            private Category top = null;
            private List<Category> categories = new List<Category>();


            public GuiStruct(RectTransform topContent, GuiCreator creator)
            {
                this.top = new Category()
                {
                    Content = topContent,
                    CategoryName = TopCategoryName
                };

                this.creator = creator;
                this.categories.Add(this.top);

                this.ChangeGUI(this.top);
            }

            public GuiStruct(RectTransform topContent, GuiCreator creator, char separator) : this(topContent, creator)
            {
                this.separator = separator;
            }
            
            public Category GetCategory(string hierarchy)
            {
                hierarchy = hierarchy.TrimEnd(separator);
                if(string.IsNullOrEmpty(hierarchy) == true)
                {
                    return this.top;
                }

                var split = hierarchy.Split(separator);
                var previous = this.top;
                
                for(var i = 0; i < split.Length; i++)
                {
                    var name = split[i];
                    this.GetButton(previous, name, name);

                    previous = this.GetCategory(previous, name);
                }

                return previous;
            }

            public Category ChangeGUI(Category previous, string targetCategoryName)
            {
                var cat = (previous == null || targetCategoryName == TopCategoryName) ?
                    this.top : this.FindNextCategory(previous, targetCategoryName);
                
                return this.ChangeGUI(cat);
            }

            public Category ChangeGUI(Category category)
            {
                if(this.Current != null)
                {
                    this.Current.SetActive(false);
                }

                this.Current = category;
                this.Current.SetActive(true);

                return this.Current;
            }

            private Category GetCategory(Category category, string categoryName)
            {
                var cat = this.FindNextCategory(category, categoryName);
                if(cat != null)
                {
                    return cat;
                }

                cat = new Category()
                {
                    Content = this.creator.GetContent(),
                    CategoryName = categoryName,
                    Previous = category
                };

                this.categories.Add(cat);
                category.Nexts.Add(cat);

                return cat;
            }

            private Category FindNextCategory(Category category, string categoryName)
            {
                for(var i = 0; i < category.Nexts.Count; i++)
                {
                    if(category.Nexts[i].CategoryName == categoryName)
                    {
                        return category.Nexts[i];
                    }
                }

                return null;
            }
            
            private GuiButton GetButton(Category category, string label, string targetCategoryName)
            {
                foreach(var b in category.Buttons)
                {
                    if(b.GetLabel() == label)
                    {
                        return b;
                    }
                }
                
                return this.creator.GetButton(category, label, targetCategoryName);
            }
        }
    }
}
