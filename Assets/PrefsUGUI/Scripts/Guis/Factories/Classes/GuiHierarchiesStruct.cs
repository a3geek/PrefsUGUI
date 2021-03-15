using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Hierarchies.Abstracts;
    using Preferences;
    using Object = UnityEngine.Object;

    public class GuiHierarchiesStruct
    {
        public GuiHierarchy Top { get; } = null;
        public AbstractGuiHierarchy Current { get; private set; } = null;

        private List<AbstractGuiHierarchy> hierarchies = new List<AbstractGuiHierarchy>();
        private PrefsGuiCreator creator = null;


        public GuiHierarchiesStruct(RectTransform topContent, PrefsGuiCreator creator)
        {
            this.Top = new GuiHierarchy(Guid.NewGuid(), topContent, PrefsCanvas.TopHierarchyName);

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

        public bool GetOrCreateHierarchy(AbstractHierarchy hierarchy, out AbstractGuiHierarchy result, AbstractHierarchy linkTarget = null)
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

            var currentHierarchy = (AbstractGuiHierarchy)this.Top;
            var parents = hierarchy?.Parents;

            foreach(var parent in (parents ?? Enumerable.Empty<AbstractHierarchy>()))
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

        public AbstractGuiHierarchy RemoveHierarchy(ref Guid hierarchyId)
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

        public AbstractGuiHierarchy ChangeGUI(AbstractGuiHierarchy nextHierarchy)
        {
            this.Current?.SetActive(false);

            this.Current = nextHierarchy ?? this.Top;
            this.Current.SetActive(true);

            return this.Current;
        }

        private bool GetNextHierarchy(AbstractGuiHierarchy current, ref Guid nextHierarchyId, out AbstractGuiHierarchy hierarchy)
        {
            hierarchy = current.GetNextHierarchy(ref nextHierarchyId);
            return hierarchy != null;
        }

        public bool GetHierarchy(AbstractHierarchy guiHierarchy, out AbstractGuiHierarchy hierarchy)
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
            AbstractGuiHierarchy current, ref Guid nextHierarchyId, AbstractHierarchy nextGui, int sortOrder, out AbstractGuiHierarchy result, AbstractHierarchy linkTarget = null
        )
        {
            if(this.GetNextHierarchy(current, ref nextHierarchyId, out var hierarchy) == true)
            {
                result = hierarchy;
                return false;
            }

            var isLinked = nextGui.HierarchyType == HierarchyType.Linked;
            hierarchy = isLinked && this.GetHierarchy(linkTarget, out var linkHierarchy) == true
                ? (AbstractGuiHierarchy)new LinkedGuiHierarchy(nextHierarchyId, nextGui.HierarchyName, linkHierarchy)
                : new GuiHierarchy(nextHierarchyId, this.creator.CreateContent(), nextGui.HierarchyName, current);
            this.GetOrCreateButton(current, nextGui, hierarchy, sortOrder);

            this.hierarchies.Add(hierarchy);
            result = hierarchy;
            return true;
        }

        private bool GetNextButton(AbstractGuiHierarchy current, AbstractHierarchy nextGui, out PrefsGuiButton prefsButton)
        {
            var id = nextGui.HierarchyId;
            prefsButton = current?.GetNextButton(ref id);
            return prefsButton != null;
        }

        private PrefsGuiButton GetOrCreateButton(AbstractGuiHierarchy current, AbstractHierarchy nextGui, AbstractGuiHierarchy next, int sortOrder)
            => this.GetNextButton(current, nextGui, out var button) == true
                ? button : this.creator.CreateButton(current, nextGui, next, sortOrder);
    }
}
