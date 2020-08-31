﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using GuiHierarchies.Abstracts;
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
            for (var i = 0; i < this.categories.Count; i++)
            {
                if (this.categories[i].TryRemovePrefs(ref prefsId, out var prefsGui) == false)
                {
                    continue;
                }

                prefsGui.Dispose();
                Object.Destroy(prefsGui.gameObject);
                return;
            }
        }

        public Category GetOrCreateCategory(AbstractGuiHierarchy hierarchy)
        {
            if (hierarchy == null)
            {
                return this.Top;
            }

            var currentCategory = this.Top;
            var parents = hierarchy?.Parents;

            foreach (var parent in (parents ?? Enumerable.Empty<AbstractGuiHierarchy>()))
            {
                var categoryId = parent.HierarchyId;
                currentCategory = this.GetOrCreateNextCategory(
                    currentCategory, ref categoryId, parent.HierarchyName, parent, parent.GuiSortOrder
                );
            }

            var id = hierarchy.HierarchyId;
            return this.GetOrCreateNextCategory(
                currentCategory, ref id, hierarchy.HierarchyName, hierarchy, hierarchy.GuiSortOrder
            );
        }

        public Category RemoveCategory(ref Guid categoryId)
        {
            if (this.Top.CategoryId == categoryId)
            {
                return this.Top;
            }

            for (var i = 0; i < this.categories.Count; i++)
            {
                if (this.categories[i].CategoryId == categoryId)
                {
                    var category = this.categories[i];

                    if (category.Previous.TryRemoveNextCategory(category, out var button) == true)
                    {
                        Object.Destroy(button.gameObject);
                    }

                    category.Dispose();
                    Object.Destroy(category.Content.gameObject);

                    this.categories.RemoveAt(i);
                    if (this.Current.CategoryId == categoryId)
                    {
                        this.Current = null;
                        return category.Previous;
                    }

                    return null;
                }
            }

            return this.Top;
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
            Category currentCategory, ref Guid nextCategoryId, string nextCategoryName, AbstractGuiHierarchy hierarchy, int sortOrder
        )
        {
            if (this.GetNextCategory(currentCategory, ref nextCategoryId, out var category) == true)
            {
                return category;
            }

            category = new Category(nextCategoryId, this.creator.CreateContent(), nextCategoryName, currentCategory);
            this.GetOrCreateButton(currentCategory, hierarchy, category, sortOrder);

            this.categories.Add(category);
            return category;
        }

        private bool GetNextButton(Category currentCategory, AbstractGuiHierarchy hierarchy, out PrefsGuiButton prefsButton)
        {
            prefsButton = currentCategory?.GetNextButton(hierarchy.HierarchyName);
            return prefsButton != null;
        }

        private PrefsGuiButton GetOrCreateButton(Category currentCategory, AbstractGuiHierarchy hierarchy, Category nextCategory, int sortOrder)
            => this.GetNextButton(currentCategory, hierarchy, out var button) == true
                ? button : this.creator.CreateButton(currentCategory, hierarchy, nextCategory, sortOrder);
    }
}
