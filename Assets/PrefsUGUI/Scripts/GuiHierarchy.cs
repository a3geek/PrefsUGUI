using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    [Serializable]
    public sealed class GuiHierarchy
    {
        public const int DefaultSortOrder = 0;
        
        public string[] SplitHierarchy
        {
            get { return this.Hierarchy.TrimEnd(Prefs.HierarchySeparator).Split(Prefs.HierarchySeparator); }
        }
        
        public string Hierarchy
        {
            get { return this.hierarchy; }
        }
        public int[] SortOrders
        {
            get { return this.sortOrders; }
        }
        public GuiHierarchy Parent
        {
            get { return this.parent; }
        }
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

        [SerializeField]
        private string hierarchy = "";
        [SerializeField]
        private int[] sortOrders = new int[0];
        [SerializeField]
        private GuiHierarchy parent = null;

        
        public GuiHierarchy(string hierarchy, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null)
            : this(hierarchy, new int[] { sortOrder }, parent)
        {; }

        public GuiHierarchy(string hierarchy, int[] sortOrders, GuiHierarchy parent = null)
        {
            this.parent = parent;
            this.sortOrders = (sortOrders == null || sortOrders.Length <= 0) ? new int[] { DefaultSortOrder } : sortOrders;

            if(string.IsNullOrEmpty(hierarchy) == false)
            {
                this.hierarchy = hierarchy.TrimEnd(Prefs.HierarchySeparator) + Prefs.HierarchySeparator;
                this.hierarchy = this.hierarchy.TrimStart(Prefs.HierarchySeparator);
            }
        }

        public int GetSortOrder(int index)
        {
            var orders = this.SortOrders;
            return orders.Length > index ? orders[index] :
                (orders.Length == 1 ? orders[0] : DefaultSortOrder);
        }
    }
}
