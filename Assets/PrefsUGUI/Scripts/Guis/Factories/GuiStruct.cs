using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories
{
    public partial class PrefsCanvas
    {
        private class GuiStruct
        {
            public Category Current { get; private set; }
            public List<Category> Categories { get; } = new List<Category>();

            private GuiCreator creator = null;
            private Category top = null;


            public GuiStruct(RectTransform topContent, GuiCreator creator)
            {
                this.top = new Category()
                {
                    Content = topContent,
                    CategoryName = TopCategoryName
                };

                this.creator = creator;
                this.Categories.Add(this.top);

                this.ChangeGUI(this.top);
            }

            public Category GetCategory(GuiHierarchy hierarchy)
            {
                if(hierarchy == null)
                {
                    return this.top;
                }

                var previous = this.top;
                var parents = hierarchy.Parents;

                for(var i = 0; i < parents.Count; i++)
                {
                    previous = this.GetCategory(previous, parents[i]);
                }
                previous = this.GetCategory(previous, hierarchy);

                return previous;
            }

            public void RemoveCategory(GuiHierarchy hierarchy)
            {
                if(hierarchy == null)
                {
                    return;
                }

                var previous = this.top;
                var parents = hierarchy.Parents;
                var split = hierarchy.SplitHierarchy;

                for(var i = 0; i < parents.Count; i++)
                {
                    previous = this.GetCategory(previous, parents[i]);
                }

                var idx = split.Length - 1;
                var button = this.GetButton(previous, split[idx], split[idx], hierarchy.GetSortOrder(idx));
                var next = this.GetCategory(previous, split[idx]);

                previous.Buttons.Remove(button);
                previous.Nexts.Remove(next);

                Destroy(button.gameObject);
                Destroy(next.Content.gameObject);
            }

            private PrefsGuiButton GetButton(Category category, string label)
            {
                foreach(var b in category.Buttons)
                {
                    if(b.GetLabel() == label)
                    {
                        return b;
                    }
                }

                return null;
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

            private Category GetCategory(Category previous, GuiHierarchy hierarchy)
            {
                var split = hierarchy.SplitHierarchy;

                for(var i = 0; i < split.Length; i++)
                {
                    var name = split[i];
                    this.GetButton(previous, name, name, hierarchy.GetSortOrder(i));

                    previous = this.GetCategory(previous, name);
                }

                return previous;
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

                this.Categories.Add(cat);
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

            private PrefsGuiButton GetButton(Category category, string label, string targetCategoryName, int sortOrder)
            {
                foreach(var b in category.Buttons)
                {
                    if(b.GetLabel() == label)
                    {
                        return b;
                    }
                }

                return this.creator.GetButton(category, label, targetCategoryName, sortOrder);
            }
        }
    }
}
