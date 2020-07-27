using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Managers;
    using static Prefs;

    [Serializable]
    public class LinkedGuiHierarchy : GuiHierarchy
    {
        protected GuiHierarchy linkParent = null;


        public LinkedGuiHierarchy(
            string hierarchyName, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null, GuiHierarchy linkParent = null,
            Action<GuiHierarchy> onCreatedGui = null
        )
            : base(hierarchyName, sortOrder, parent, onCreatedGui)
        {
            this.linkParent = linkParent;
        }

        protected override void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiButton gui)
        {
            this.gui = gui;

            this.onButtonClicked = () =>
            {
                this.linkParent?.Open(false);
                this.FireOnHierarchyClicked();
            };

            gui.Initialize(this.HierarchyName, this.onButtonClicked);
            this.onCreatedGui?.Invoke(this);
        }
    }
}
