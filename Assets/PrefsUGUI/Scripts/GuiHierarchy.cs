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
    public class GuiHierarchy : IDisposable
    {
        public const int DefaultSortOrder = 0;

        public virtual HierarchyType HierarchyType => HierarchyType.Standard;
        public virtual string HierarchyName => this.hierarchyName;
        public virtual int SortOrder => this.sortOrder;
        public virtual GuiHierarchy Parent => this.parent;
        public virtual bool VisibleGUI
        {
            get => this.gui != null && this.gui.GetVisible();
            set
            {
                if (this.gui != null)
                {
                    this.gui.SetVisible(value);
                }
            }
        }
        public virtual Guid HierarchyId { get; } = Guid.Empty;
        public virtual IReadOnlyList<GuiHierarchy> Parents { get; protected set; } = new List<GuiHierarchy>();
        public virtual string FullHierarchy { get; protected set; } = "";

        [SerializeField]
        protected string hierarchyName = "";
        [SerializeField]
        protected int sortOrder = 0;
        [SerializeField]
        protected GuiHierarchy parent = null;

        protected bool disposed = false;
        protected PrefsGuiButton gui = null;


        public GuiHierarchy(string hierarchyName, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null)
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.parent = parent;
            this.sortOrder = sortOrder;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();

            this.Regist();
        }

        protected virtual void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiButton gui)
        {
            this.gui = gui;

            void onButtonClicked() => canvas.ChangeGUI(category);
            gui.Initialize(this.HierarchyName, onButtonClicked);
        }

        protected virtual List<GuiHierarchy> GetParents()
        {
            var parents = new List<GuiHierarchy>();
            var parent = this.Parent;

            while(parent != null)
            {
                parents.Add(parent);
                parent = parent.Parent;
            }

            parents.Reverse();
            return parents;
        }

        protected virtual string GetFullHierarchy()
        {
            var hierarchy = "";
            foreach(var parent in this.Parents)
            {
                hierarchy += string.IsNullOrEmpty(parent?.HierarchyName) == true ? "" : parent.HierarchyName + HierarchySeparator;
            }

            return hierarchy + this.HierarchyName + HierarchySeparator;
        }

        #region IDisposable Support
        ~GuiHierarchy()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(this.disposed == true)
            {
                return;
            }

            this.parent = null;
            this.gui = null;
            PrefsManager.RemoveGuiHierarchy(this.HierarchyId);
            this.disposed = true;
        }
        #endregion
    }
}
