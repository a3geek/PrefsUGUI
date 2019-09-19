using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using static Prefs;

    /// <summary>
    /// Hierarchy preset for GUI.
    /// </summary>
    [Serializable]
    public sealed class GuiHierarchy
    {
        /// <summary>Default sort order for GUI hierarchy.</summary>
        public const int DefaultSortOrder = 0;

        /// <summary>Splitted hierarchy.</summary>
        public string[] SplitHierarchy => this.Hierarchy.TrimEnd(HierarchySeparator).Split(HierarchySeparator);
        /// <summary>Hierarchy of GUI.</summary>
        public string Hierarchy => this.hierarchy;
        /// <summary>Sort order for GUI hierarchy from root.</summary>
        public int[] SortOrders => this.sortOrders;
        /// <summary>Parent in GUI.</summary>
        public GuiHierarchy Parent => this.parent;
        /// <summary>All Parents in GUI to the root.</summary>
        public List<GuiHierarchy> Parents
        {
            get
            {
                var parents = new List<GuiHierarchy>();
                var parent = this.Parent;

                while(parent != null)
                {
                    parents.Insert(0, parent);
                    parent = parent.Parent;
                }

                return parents;
            }
        }

        /// <summary>Hierarchy of GUI.</summary>
        [SerializeField]
        private string hierarchy = "";
        /// <summary>Sort order for GUI hierarchy from root.</summary>
        [SerializeField]
        private int[] sortOrders = new int[0];
        /// <summary>Parent in GUI.</summary>
        [SerializeField]
        private GuiHierarchy parent = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hierarchy">Hierarchy of GUI.</param>
        /// <param name="sortOrder">Sort order for GUI hierarchy</param>
        /// <param name="parent">Parent in GUI.</param>
        public GuiHierarchy(string hierarchy, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null)
            : this(hierarchy, new int[] { sortOrder }, parent)
        {
            ;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hierarchy">Hierarchy of GUI.</param>
        /// <param name="sortOrders">Sort order for GUI hierarchy from root.</param>
        /// <param name="parent">Parent in GUI.</param>
        public GuiHierarchy(string hierarchy, int[] sortOrders, GuiHierarchy parent = null)
        {
            this.parent = parent;
            this.sortOrders = (sortOrders == null || sortOrders.Length <= 0) ? new int[] { DefaultSortOrder } : sortOrders;

            this.hierarchy = (hierarchy.TrimEnd(HierarchySeparator) + HierarchySeparator).TrimStart(HierarchySeparator);
        }

        /// <summary>
        /// Get sorted order by the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetSortOrder(int index)
        {
            var orders = this.SortOrders;
            return index >= 0 && index < orders.Length ? orders[index] :
                (orders.Length == 1 ? orders[0] : DefaultSortOrder);
        }

        public void Dispose()
        {
            RemoveGuiHierarchy(this);
        }
    }
}
