using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Hierarchies.Abstracts
{
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Managers;
    using Preferences.Abstracts;
    using UnityEngine.Events;
    using static Prefs;

    [Serializable]
    public abstract class AbstractHierarchy : IDisposable
    {
        public const int DefaultSortOrder = 0;

        public event Action OnHierarchyClicked = delegate { };

        public virtual string HierarchyName => this.hierarchyName;
        public virtual AbstractHierarchy Parent => this.parent;
        public virtual bool IsCreatedGui => this.properties.IsCreatedGui;
        public virtual HierarchyType HierarchyType => HierarchyType.Standard;
        public virtual Guid HierarchyId { get; protected set; } = Guid.Empty;
        public virtual string FullHierarchy { get; protected set; } = "";
        public virtual string SaveKeyPath { get; protected set; } = "";
        public virtual IReadOnlyList<AbstractHierarchy> Parents { get; protected set; } = new List<AbstractHierarchy>();
        public virtual float BottomMargin
        {
            get => this.properties.BottomMargin;
            set => this.properties.BottomMargin = value;
        }
        public virtual float TopMargin
        {
            get => this.properties.TopMargin;
            set => this.properties.TopMargin = value;
        }
        public virtual bool VisibleGUI
        {
            get => this.properties.VisibleGUI;
            set => this.properties.VisibleGUI = value;
        }
        public virtual int GuiSortOrder
        {
            get => this.properties.GuiSortOrder;
            protected set => this.properties.GuiSortOrder = value;
        }

        [SerializeField]
        protected int sortOrder = 0;
        [SerializeField]
        protected string hierarchyName = "";
        [SerializeField]
        protected AbstractHierarchy parent = null;

        protected bool disposed = false;
        protected UnityAction changeGUI = null;
        protected PrefsGuiProperties<PrefsGuiButton> properties = new PrefsGuiProperties<PrefsGuiButton>();


        public virtual void Open(bool withClickedEvent = true)
        {
            this.changeGUI?.Invoke();

            if(withClickedEvent == true)
            {
                this.FireOnHierarchyClicked();
            }
        }

        protected virtual void OnCreatedGuiButton(PrefsGuiButton gui)
        {
            this.properties.OnCreatedGui(gui, this.HierarchyName);

            gui.Initialize(this.HierarchyName, this.FireOnHierarchyClicked);
            this.FireOnCreatedGui();
        }
        
        protected virtual List<AbstractHierarchy> GetParents()
        {
            var parents = new List<AbstractHierarchy>();
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
                hierarchy += string.IsNullOrEmpty(parent?.HierarchyName) == true? "" : parent.HierarchyName + HierarchySeparator;
            }

            return hierarchy + this.HierarchyName + HierarchySeparator;
        }

        protected virtual void FireOnHierarchyClicked()
            => this.OnHierarchyClicked.Invoke();

        protected abstract void Regist();
        protected abstract void FireOnCreatedGui();

        #region IDisposable Support
        public virtual void Dispose()
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

            this.properties.Dispose();
            this.parent = null;
            this.changeGUI = null;
            this.OnHierarchyClicked = null;
            PrefsManager.RemoveGuiHierarchy(this.HierarchyId);
            this.disposed = true;
        }
        #endregion
    }
}
