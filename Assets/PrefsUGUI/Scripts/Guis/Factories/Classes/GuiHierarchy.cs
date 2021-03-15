using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Commons;
    using Guis.Preferences;
    using PrefsGuiBase = Preferences.PrefsGuiBase;

    public class GuiHierarchy : AbstractGuiHierarchy, IDisposable
    {
        public GuiHierarchy(Guid hierarchyId, RectTransform content, string hierarchyName)
        {
            this.HierarchyId = hierarchyId;
            this.Content = content;
            this.HierarchyName = hierarchyName;
        }

        public GuiHierarchy(Guid hierarchyId, RectTransform content, string hierarchyName, AbstractGuiHierarchy previous)
            : this(hierarchyId, content, hierarchyName)
        {
            this.Previous = previous;
        }
    }
}
