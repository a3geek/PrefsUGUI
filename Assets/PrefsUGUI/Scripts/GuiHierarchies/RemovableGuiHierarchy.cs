﻿using System;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Managers;

    [Serializable]
    public class RemovableGuiHierarchy : GuiHierarchy
    {
        public event Action OnRemoved = delegate { };

        public override HierarchyType HierarchyType => HierarchyType.Removable;


        public RemovableGuiHierarchy(
            string hierarchyName, Action onRemoved = null, int sortOrder = DefaultSortOrder,
            GuiHierarchy parent = null, Action<GuiHierarchy> onCreatedGui = null
        )
            : base(hierarchyName, sortOrder, parent, onCreatedGui)
        {
            if (onRemoved != null)
            {
                this.OnRemoved += onRemoved;
            }
        }

        protected override void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiRemovableButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiRemovableButton gui)
        {
            this.gui = gui;

            this.onButtonClicked = () =>
            {
                this.FireOnHierarchyClicked();
                canvas.ChangeGUI(category);
            };

            void FireOnRemoved()
            {
                this.OnRemoved?.Invoke();
                this.Dispose();
            }

            gui.Initialize(this.HierarchyName, this.onButtonClicked, FireOnRemoved);
            this.onCreatedGui?.Invoke(this);
        }

        protected override void Dispose(bool disposing)
        {
            this.OnRemoved = null;
            base.Dispose(disposing);
        }
    }
}
