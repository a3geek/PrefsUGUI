using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Preferences;
    using Object = UnityEngine.Object;

    public class CategoriesStruct
    {
        public Category Top { get; } = null;
        public Category Current { get; private set; } = null;

        private List<Category> categories = new List<Category>();
        private PrefsGuiCreator creator = null;


        public CategoriesStruct(RectTransform topContent, PrefsGuiCreator creator)
        {
            this.Top = new Category(Guid.NewGuid(), topContent, PrefsCanvas.TopCategoryName);

            this.creator = creator;
            this.categories.Add(this.Top);

            this.ChangeGUI(this.Top);
        }

        public void RemovePrefs(ref Guid prefsId)
        {
            for(var i = 0; i < this.categories.Count; i++)
            {
                if(this.categories[i].TryRemovePrefs(ref prefsId, out var prefsGui) == false)
                {
                    continue;
                }

                Object.Destroy(prefsGui.gameObject);
                return;
            }
        }

        public Category GetCategory(GuiHierarchy hierarchy)
        {
            if (hierarchy == null)
            {
                return this.Top;
            }

            var currentCategory = this.Top;
            var parents = hierarchy.Parents;

            foreach (var parent in parents)
            {
                var categoryId = parent.HierarchyId;
                currentCategory = this.GetOrCreateNextCategory(
                    currentCategory, ref categoryId, parent.HierarchyName, parent.HierarchyName, parent.SortOrder
                );
            }

            var id = hierarchy.HierarchyId;
            return this.GetOrCreateNextCategory(
                currentCategory, ref id, hierarchy.HierarchyName, hierarchy.HierarchyName, hierarchy.SortOrder
            );
        }

        public void RemoveCategory(ref Guid categoryId)
        {
            for (var i = 0; i < this.categories.Count; i++)
            {
                if (this.categories[i].CategoryId == categoryId)
                {
                    var category = this.categories[i];

                    if (category.Previous.TryRemoveNextCategory(category, out var button) == true)
                    {
                        Object.Destroy(button.gameObject);
                    }

                    Object.Destroy(category.Content.gameObject);
                    this.categories.RemoveAt(i);

                    return;
                }
            }
        }

        public Category ChangeGUI(Category nextCategory)
        {
            this.Current?.SetActive(false);

            this.Current = nextCategory ?? this.Top;
            this.Current.SetActive(true);

            return this.Current;
        }

        private bool GetNextCategory(Category currentCategory, ref Guid nextCategoryId, out Category category)
        {
            category = currentCategory.GetNextCategory(ref nextCategoryId);
            return category != null;
        }

        private Category GetOrCreateNextCategory(
            Category currentCategory, ref Guid nextCategoryId, string nextCategoryName, string label, int sortOrder
        )
        {
            if (this.GetNextCategory(currentCategory, ref nextCategoryId, out var category) == true)
            {
                return category;
            }

            category = new Category(nextCategoryId, this.creator.CreateContent(), nextCategoryName, currentCategory);
            this.GetOrCreateButton(currentCategory, label, category, sortOrder);

            this.categories.Add(category);
            return category;
        }

        private bool GetButton(Category currentCategory, string label, out PrefsGuiButton prefsButton)
        {
            prefsButton = currentCategory?.GetButton(label);
            return prefsButton != null;
        }

        private PrefsGuiButton GetOrCreateButton(Category currentCategory, string label, Category nextCategory, int sortOrder)
            => this.GetButton(currentCategory, label, out var button) == true
                ? button : this.creator.CreateButton(currentCategory, label, nextCategory, sortOrder);
    }
}
