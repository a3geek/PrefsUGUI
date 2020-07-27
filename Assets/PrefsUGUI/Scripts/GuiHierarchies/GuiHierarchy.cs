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
    using UnityEngine.Events;
    using static Prefs;

    [Serializable]
    public class GuiHierarchy : IDisposable
    {
        public const int DefaultSortOrder = 0;

        public event Action OnHierarchyClicked = delegate { };

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
        protected Action<GuiHierarchy> onCreatedGui = null;
        protected UnityAction onButtonClicked = null;


        public GuiHierarchy(
            string hierarchyName, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null,
            Action<GuiHierarchy> onCreatedGui = null
        )
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.parent = parent;
            this.sortOrder = sortOrder;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();

            this.onCreatedGui = onCreatedGui;

            this.Regist();
        }

        public virtual void Open(bool withEvent = false)
        {
            this.onButtonClicked?.Invoke();

            if(withEvent == true)
            {
                this.FireOnHierarchyClicked();
            }
        }

        protected virtual void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiButton gui)
        {
            this.gui = gui;

            this.onButtonClicked = () =>
            {
                canvas.ChangeGUI(category);
                this.FireOnHierarchyClicked();
            };

            gui.Initialize(this.HierarchyName, this.onButtonClicked);
            this.onCreatedGui?.Invoke(this);
        }

        protected virtual List<GuiHierarchy> GetParents()
        {
            var parents = new List<GuiHierarchy>();
            var parent = this.Parent;

            while (parent != null)
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
            foreach (var parent in this.Parents)
            {
                hierarchy += string.IsNullOrEmpty(parent?.HierarchyName) == true ? "" : parent.HierarchyName + HierarchySeparator;
            }

            return hierarchy + this.HierarchyName + HierarchySeparator;
        }

        protected virtual void FireOnHierarchyClicked()
            => this.OnHierarchyClicked?.Invoke();

        #region IDisposable Support
        ~GuiHierarchy()
        {
            this.Dispose(false);
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed == true)
            {
                return;
            }

            this.parent = null;
            this.gui = null;
            this.onCreatedGui = null;
            this.onButtonClicked = null;
            this.OnHierarchyClicked = null;
            PrefsManager.RemoveGuiHierarchy(this.HierarchyId);
            this.disposed = true;
        }
        #endregion
    }
}
