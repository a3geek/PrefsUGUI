﻿using System;
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

        public event Action OnDisposed = delegate { };
        public event Action OnHierarchyClicked = delegate { };

        public virtual string HierarchyName => this.hierarchyName;
        public virtual string SaveKey => string.IsNullOrEmpty(this.saveKey) == true ? this.HierarchyName : this.saveKey;
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
        protected string hierarchyName = "";
        [SerializeField]
        protected string saveKey = "";
        [SerializeField]
        protected AbstractHierarchy parent = null;

        protected bool disposed = false;
        protected UnityAction changeGUI = null;
        protected PrefsGuiProperties<PrefsGuiButton> properties = null;


        public AbstractHierarchy(string hierarchyName)
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.properties = new PrefsGuiProperties<PrefsGuiButton>(this.HierarchyName);
        }

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
            this.properties.OnCreatedGui(gui);

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
                hierarchy += string.IsNullOrEmpty(parent?.HierarchyName) == true ? "" : parent.HierarchyName + HierarchySeparator;
            }

            return hierarchy + this.HierarchyName + HierarchySeparator;
        }

        protected virtual string GetFullSaveKeyPath()
        {
            var saveKeyPath = "";
            foreach(var parent in this.Parents)
            {
                saveKeyPath += string.IsNullOrEmpty(parent?.SaveKey) == true ? "" : parent.SaveKey + HierarchySeparator;
            }

            return saveKeyPath + this.SaveKey + HierarchySeparator;
        }

        protected virtual void FireOnHierarchyClicked()
            => this.OnHierarchyClicked.Invoke();

        protected abstract void Regist();
        protected abstract void FireOnCreatedGui();

        #region IDisposable Support
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(this.disposed == true)
            {
                return;
            }

            this.DisposeInternal(disposing);
        }

        protected virtual void DisposeInternal(bool disposing)
        {
            this.OnDisposed();
            PrefsManager.RemoveGuiHierarchy(this.HierarchyId);

            this.properties.Dispose();
            this.parent = null;
            this.changeGUI = null;
            this.OnHierarchyClicked = null;
            this.OnDisposed = null;

            this.disposed = true;
        }
        #endregion
    }
}
