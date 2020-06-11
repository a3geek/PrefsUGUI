using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Commons;
    using Guis.Preferences;
    using PrefsList = Commons.SortedList<(Prefs.PrefsBase prefs, Preferences.PrefsGuiBase prefsGui)>;

    public class Category
    {
        public RectTransform Content = null;
        public string CategoryName = "";
        public List<Category> Nexts = new List<Category>();
        public Category Previous = null;
        public PrefsList PrefsList = new PrefsList(
            (p1, p2) => p1.prefs.GuiSortOrder - p2.prefs.GuiSortOrder
        );
        public SortedList<PrefsGuiButton> Buttons = new SortedList<PrefsGuiButton>(
            (b1, b2) => string.Compare(b1.GetLabel(), b2.GetLabel())
        );


        public void SetActive(bool active)
        {
            this.Content.gameObject.SetActive(active);
        }
    }
}
