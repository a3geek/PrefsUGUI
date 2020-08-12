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
    public abstract class AbstractGuiHierarchy : IDisposable
    {
        public const int DefaultSortOrder = 0;

        public event Action OnHierarchyClicked = delegate { };

        public virtual HierarchyType HierarchyType => HierarchyType.Standard;
        public virtual string HierarchyName => this.hierarchyName;
        public virtual int SortOrder => this.sortOrder;
        public virtual AbstractGuiHierarchy Parent => this.parent;
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
        public virtual Guid HierarchyId { get; protected set; } = Guid.Empty;
        public virtual IReadOnlyList<AbstractGuiHierarchy> Parents { get; protected set; } = new List<AbstractGuiHierarchy>();
        public virtual string FullHierarchy { get; protected set; } = "";

        [SerializeField]
        protected string hierarchyName = "";
        [SerializeField]
        protected int sortOrder = 0;
        [SerializeField]
        protected AbstractGuiHierarchy parent = null;

        protected bool disposed = false;
        protected PrefsGuiButton gui = null;
        protected UnityAction<string> changeGUI = null;


        public virtual void Open(string hierarchyCategoryName = null, bool withClickedEvent = true)
        {
            this.changeGUI?.Invoke(hierarchyCategoryName);

            if(withClickedEvent == true)
            {
                this.FireOnHierarchyClicked();
            }
        }

        protected virtual void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiButton gui)
        {
            this.gui = gui;

            this.changeGUI = (string hierarchyCategoryName) => canvas.ChangeGUI(category, hierarchyCategoryName);

            gui.Initialize(this.HierarchyName, () =>
            {
                this.changeGUI(null);
                this.FireOnHierarchyClicked();
            });
            this.FireOnCreatedGui();
        }

        protected virtual List<AbstractGuiHierarchy> GetParents()
        {
            var parents = new List<AbstractGuiHierarchy>();
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

        protected abstract void FireOnCreatedGui();

        #region IDisposable Support
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
            this.changeGUI = null;
            this.OnHierarchyClicked = null;
            PrefsManager.RemoveGuiHierarchy(this.HierarchyId);
            this.disposed = true;
        }
        #endregion
    }
}
