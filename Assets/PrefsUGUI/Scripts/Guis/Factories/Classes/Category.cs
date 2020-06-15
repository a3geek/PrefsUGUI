using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Commons;
    using Guis.Preferences;
    using PrefsGuiBase = Preferences.PrefsGuiBase;

    public class Category
    {
        public RectTransform Content { get; } = null;
        public string CategoryName { get; } = "";
        public Guid CategoryId { get; } = Guid.Empty;
        public Category Previous { get; } = null;

        private Dictionary<Category, PrefsGuiButton> nextCategories = new Dictionary<Category, PrefsGuiButton>();
        private MultistageSortedList<PrefsGuiButton> buttons = new MultistageSortedList<PrefsGuiButton>(
            (b1, b2) => string.Compare(b1.GetLabel(), b2.GetLabel())
        );
        private Dictionary<Guid, PrefsGuiBase> prefsGuis = new Dictionary<Guid, PrefsGuiBase>();
        private SortedList<PrefsGuiBase> prefs = new SortedList<PrefsGuiBase>();


        public Category(Guid categoryId, RectTransform content, string categoryName)
        {
            this.CategoryId = categoryId;
            this.Content = content;
            this.CategoryName = categoryName;
        }

        public Category(Guid categoryId, RectTransform content, string categoryName, Category previous)
            : this(categoryId, content, categoryName)
        {
            this.Previous = previous;
        }

        public PrefsGuiButton GetButton(string label)
        {
            foreach (var button in this.buttons)
            {
                if (button.GetLabel() == label)
                {
                    return button;
                }
            }

            return null;
        }

        public Category GetNextCategory(ref Guid nextCategoryId)
        {
            foreach (var category in this.nextCategories)
            {
                if (category.Key.CategoryId == nextCategoryId)
                {
                    return category.Key;
                }
            }

            return null;
        }

        public int AddNextCategory(Category nextCategory, PrefsGuiButton button, int sortOrder)
        {
            this.nextCategories[nextCategory] = button;
            return this.buttons.Add(button, sortOrder);
        }

        public bool TryRemoveNextCategory(Category nextCategory, out PrefsGuiButton button)
        {
            if (this.nextCategories.TryGetValue(nextCategory, out button) == false)
            {
                return false;
            }

            this.buttons.Remove(button);
            this.nextCategories.Remove(nextCategory);
            return true;
        }

        public int AddPrefs(ref Guid guid, PrefsGuiBase prefsGui, int sortOrder)
        {
            this.prefsGuis[guid] = prefsGui;
            return this.prefs.Add(prefsGui, sortOrder) + this.buttons.Count;
        }

        public bool TryRemovePrefs(ref Guid guid, out PrefsGuiBase prefsGui)
        {
            if (this.prefsGuis.TryGetValue(guid, out prefsGui) == false)
            {
                return false;
            }

            this.prefs.Remove(prefsGui);
            this.prefsGuis.Remove(guid);
            return true;
        }

        public void SetActive(bool active)
            => this.Content.gameObject.SetActive(active);
    }
}
