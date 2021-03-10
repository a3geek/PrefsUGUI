using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using GuiHierarchies.Abstracts;
    using Preferences;
    using Object = UnityEngine.Object;

    public class HierarchiesStruct
    {
        public Hierarchy Top { get; } = null;
        public AbstractHierarchy Current { get; private set; } = null;

        private List<AbstractHierarchy> hierarchies = new List<AbstractHierarchy>();
        private PrefsGuiCreator creator = null;


        public HierarchiesStruct(RectTransform topContent, PrefsGuiCreator creator)
        {
            this.Top = new Hierarchy(Guid.NewGuid(), topContent, PrefsCanvas.TopHierarchyName);

            this.creator = creator;
            this.hierarchies.Add(this.Top);

            this.ChangeGUI(this.Top);
        }

        public void RemovePrefs(ref Guid prefsId)
        {
            for(var i = 0; i < this.hierarchies.Count; i++)
            {
                if(this.hierarchies[i].TryRemovePrefs(ref prefsId, out var prefsGui) == false)
                {
                    continue;
                }

                prefsGui.Dispose();
                Object.Destroy(prefsGui.gameObject);
                return;
            }
        }

        public bool GetOrCreateHierarchy(AbstractGuiHierarchy hierarchy, out AbstractHierarchy result, AbstractGuiHierarchy linkTarget = null)
        {
            if(hierarchy == null || string.IsNullOrEmpty(hierarchy.FullHierarchy) == true)
            {
                result = this.Top;
                return false;
            }
            else if(this.GetHierarchy(hierarchy, out result) == true)
            {
                return false;
            }

            var currentHierarchy = (AbstractHierarchy)this.Top;
            var parents = hierarchy?.Parents;

            foreach(var parent in (parents ?? Enumerable.Empty<AbstractGuiHierarchy>()))
            {
                var hierarchyId = parent.HierarchyId;
                this.GetOrCreateNextHierarchy(
                    currentHierarchy, ref hierarchyId, parent, parent.GuiSortOrder, out currentHierarchy
                );
            }

            var id = hierarchy.HierarchyId;
            return this.GetOrCreateNextHierarchy(
                currentHierarchy, ref id, hierarchy, hierarchy.GuiSortOrder, out result, linkTarget
            );
        }

        public AbstractHierarchy RemoveHierarchy(ref Guid hierarchyId)
        {
            if(this.Top.HierarchyId == hierarchyId)
            {
                return this.Top;
            }

            for(var i = 0; i < this.hierarchies.Count; i++)
            {
                if(this.hierarchies[i].HierarchyId == hierarchyId)
                {
                    var hierarchy = this.hierarchies[i];

                    if(hierarchy.Previous.TryRemoveNextHierarchy(hierarchy, out var button) == true)
                    {
                        Object.Destroy(button.gameObject);
                    }

                    hierarchy.Dispose();
                    Object.Destroy(hierarchy.Content.gameObject);

                    this.hierarchies.RemoveAt(i);
                    if(this.Current.HierarchyId == hierarchyId)
                    {
                        this.Current = null;
                        return hierarchy.Previous;
                    }

                    return null;
                }
            }

            return this.Top;
        }

        public AbstractHierarchy ChangeGUI(AbstractHierarchy nextHierarchy)
        {
            this.Current?.SetActive(false);

            this.Current = nextHierarchy ?? this.Top;
            this.Current.SetActive(true);

            return this.Current;
        }

        private bool GetNextHierarchy(AbstractHierarchy current, ref Guid nextHierarchyId, out AbstractHierarchy hierarchy)
        {
            hierarchy = current.GetNextHierarchy(ref nextHierarchyId);
            return hierarchy != null;
        }

        public bool GetHierarchy(AbstractGuiHierarchy guiHierarchy, out AbstractHierarchy hierarchy)
        {
            for(var i = 0; guiHierarchy != null && i < this.hierarchies.Count; i++)
            {
                if(this.hierarchies[i].HierarchyId == guiHierarchy.HierarchyId)
                {
                    hierarchy = this.hierarchies[i];
                    return true;
                }
            }

            hierarchy = this.Top;
            return false;
        }

        private bool GetOrCreateNextHierarchy(
            AbstractHierarchy current, ref Guid nextHierarchyId, AbstractGuiHierarchy nextGui, int sortOrder, out AbstractHierarchy result, AbstractGuiHierarchy linkTarget = null
        )
        {
            if(this.GetNextHierarchy(current, ref nextHierarchyId, out var hierarchy) == true)
            {
                result = hierarchy;
                return false;
            }

            var isLinked = nextGui.HierarchyType == HierarchyType.Linked;
            hierarchy = isLinked && this.GetHierarchy(linkTarget, out var linkHierarchy) == true
                ? (AbstractHierarchy)new LinkedHierarchy(nextHierarchyId, nextGui.HierarchyName, linkHierarchy)
                : new Hierarchy(nextHierarchyId, this.creator.CreateContent(), nextGui.HierarchyName, current);
            this.GetOrCreateButton(current, nextGui, hierarchy, sortOrder);

            this.hierarchies.Add(hierarchy);
            result = hierarchy;
            return true;
        }

        private bool GetNextButton(AbstractHierarchy current, AbstractGuiHierarchy nextGui, out PrefsGuiButton prefsButton)
        {
            var id = nextGui.HierarchyId;
            prefsButton = current?.GetNextButton(ref id);
            return prefsButton != null;
        }

        private PrefsGuiButton GetOrCreateButton(AbstractHierarchy current, AbstractGuiHierarchy nextGui, AbstractHierarchy next, int sortOrder)
            => this.GetNextButton(current, nextGui, out var button) == true
                ? button : this.creator.CreateButton(current, nextGui, next, sortOrder);
    }
}
