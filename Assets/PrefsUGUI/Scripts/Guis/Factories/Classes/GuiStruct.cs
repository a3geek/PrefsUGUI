using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Preferences;
    using UnityEngine.Experimental.UIElements;

    public class GuiStruct
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
                CategoryName = PrefsCanvas.TopCategoryName
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

            for(var i = 0; i < (parents?.Count ?? 0); i++)
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
            var button = this.GetOrCreateButton(previous, split[idx], split[idx], hierarchy.GetSortOrder(idx));
            var next = this.GetOrCreateNextCategory(previous, split[idx]);

            previous.Buttons.Remove(button);
            previous.Nexts.Remove(next);

            Destroy(button.gameObject);
            Destroy(next.Content.gameObject);
        }

        public Category ChangeGUI(Category currentCategory, string nextCategoryName)
        {
            Category category = currentCategory == null || nextCategoryName == PrefsCanvas.TopCategoryName
                ? this.top
                : this.GetNextCategory(currentCategory, nextCategoryName, out category) == true ? category : null;

            return this.ChangeGUI(category);
        }

        public Category ChangeGUI(Category category)
        {
            this.Current?.SetActive(false);

            this.Current = category;
            this.Current?.SetActive(true);

            return this.Current;
        }

        private Category GetOrCreateCategory(Category previous, GuiHierarchy hierarchy)
        {
            var hierarchies = hierarchy.SplitHierarchy;

            for(var i = 0; i < hierarchies.Length; i++)
            {
                var categoryName = hierarchies[i];
                this.GetOrCreateButton(previous, categoryName, categoryName, hierarchy.GetSortOrder(i));

                previous = this.GetOrCreateNextCategory(previous, categoryName);
            }

            return previous;
        }

        private bool GetNextCategory(Category currentCategory, string nextCategoryName, out Category category)
        {
            category = null;

            for(var i = 0; i < currentCategory.Nexts.Count; i++)
            {
                if(currentCategory.Nexts[i].CategoryName == nextCategoryName)
                {
                    category = currentCategory.Nexts[i];
                    return true;
                }
            }

            return false;
        }

        private Category GetOrCreateNextCategory(Category currentCategory, string nextCategoryName)
        {
            if(this.GetNextCategory(currentCategory, nextCategoryName, out var category) == true)
            {
                return category;
            }

            category = new Category()
            {
                Content = this.creator.GetContent(),
                CategoryName = nextCategoryName,
                Previous = currentCategory
            };

            this.Categories.Add(category);
            currentCategory.Nexts.Add(category);

            return category;
        }

        private bool GetButton(Category category, string label, out PrefsGuiButton prefsButton)
        {
            prefsButton = null;

            foreach(var button in category.Buttons)
            {
                if(button.GetLabel() == label)
                {
                    prefsButton = button;
                    return true;
                }
            }

            return false;
        }

        private PrefsGuiButton GetOrCreateButton(Category category, string label, string targetCategoryName, int sortOrder)
            => this.GetButton(category, label, out var button) == true
                ? button : this.creator.GetButton(category, label, targetCategoryName, sortOrder);
    }
}
