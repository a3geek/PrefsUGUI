using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using static Prefs;

    [Serializable]
    public sealed class GuiHierarchy
    {
        public const int DefaultSortOrder = 0;

        public string[] SplitHierarchy => this.Hierarchy.TrimEnd(HierarchySeparator).Split(HierarchySeparator);
        public string Hierarchy => this.hierarchy;
        public int[] SortOrders => this.sortOrders;
        public GuiHierarchy Parent => this.parent;
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
        {
            ;
        }

        public GuiHierarchy(string hierarchy, int[] sortOrders, GuiHierarchy parent = null)
        {
            this.parent = parent;
            this.sortOrders = (sortOrders == null || sortOrders.Length <= 0) ? new int[] { DefaultSortOrder } : sortOrders;

            this.hierarchy = (hierarchy.TrimEnd(HierarchySeparator) + HierarchySeparator).TrimStart(HierarchySeparator);
        }

        public int GetSortOrder(int index)
        {
            var orders = this.SortOrders;
            return index >= 0 && index < orders.Length ? orders[index] :
                (orders.Length == 1 ? orders[0] : DefaultSortOrder);
        }
    }
}
