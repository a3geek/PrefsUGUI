using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Commons;
    using Guis.Preferences;
    using PrefsGuiBase = Preferences.PrefsGuiBase;

    public abstract class AbstractGuiHierarchy : IDisposable
    {
        public event Action OnDiscard = delegate { };

        public virtual RectTransform Content { get; protected set; }
        public virtual string HierarchyName { get; protected set; }
        public virtual Guid HierarchyId { get; protected set; }
        public virtual AbstractGuiHierarchy Previous { get; protected set; }
        public virtual PrefsGuiButton GuiButton { get; protected set; }

        private Dictionary<AbstractGuiHierarchy, PrefsGuiButton> nextHierarchies = new Dictionary<AbstractGuiHierarchy, PrefsGuiButton>();
        private MultistageSortedList<PrefsGuiButton> nextButtons = new MultistageSortedList<PrefsGuiButton>(
            (b1, b2) => string.Compare(b1.GetLabel(), b2.GetLabel())
        );
        private Dictionary<Guid, PrefsGuiBase> prefsGuis = new Dictionary<Guid, PrefsGuiBase>();
        private SortedList<PrefsGuiBase> prefs = new SortedList<PrefsGuiBase>();


        public virtual PrefsGuiButton GetNextButton(ref Guid hierarchyId)
        {
            foreach(var hierarchy in this.nextHierarchies)
            {
                if(hierarchy.Key.HierarchyId == hierarchyId)
                {
                    return hierarchy.Value;
                }
            }

            return null;
        }

        public virtual AbstractGuiHierarchy GetNextHierarchy(ref Guid nextHierarchyId)
        {
            foreach(var hierarchy in this.nextHierarchies)
            {
                if(hierarchy.Key.HierarchyId == nextHierarchyId)
                {
                    return hierarchy.Key;
                }
            }

            return null;
        }

        public virtual int AddNextHierarchy(AbstractGuiHierarchy nextHierarchy, PrefsGuiButton button, int sortOrder)
        {
            button.gameObject.SetActive(false);
            nextHierarchy.GuiButton = button;
            this.nextHierarchies[nextHierarchy] = button;
            return this.nextButtons.Add(button, sortOrder);
        }

        public virtual bool TryRemoveNextHierarchy(AbstractGuiHierarchy nextHierarchy, out PrefsGuiButton button)
        {
            if(this.nextHierarchies.TryGetValue(nextHierarchy, out button) == false)
            {
                return false;
            }

            this.nextButtons.Remove(button);
            this.nextHierarchies.Remove(nextHierarchy);
            return true;
        }

        public virtual int AddPrefs(Guid guid, PrefsGuiBase prefsGui, int sortOrder)
        {
            this.ActivateGuiButtons();
            this.prefsGuis[guid] = prefsGui;
            return this.prefs.Add(prefsGui, sortOrder) + this.nextButtons.Count;
        }

        public virtual bool TryRemovePrefs(ref Guid guid, out PrefsGuiBase prefsGui)
        {
            if(this.prefsGuis.TryGetValue(guid, out prefsGui) == false)
            {
                return false;
            }

            this.prefs.Remove(prefsGui);
            this.prefsGuis.Remove(guid);
            return true;
        }

        public virtual void SetActive(bool active)
            => this.Content.gameObject.SetActive(active);

        public virtual void Discard()
            => this.OnDiscard();

        public virtual void Dispose()
        {
            this.OnDiscard = null;
            this.GuiButton = null;
            this.nextHierarchies.Clear();
            this.nextButtons.Clear();
            this.prefsGuis.Clear();
            this.prefs.Clear();
        }

        protected virtual void ActivateGuiButtons()
        {
            if(this.GuiButton != null)
            {
                this.GuiButton.SetVisible(true);
            }

            var previous = this.Previous;
            while(previous != null)
            {
                if(previous.GuiButton != null)
                {
                    previous.GuiButton.SetVisible(true);
                }

                previous = previous.Previous;
            }
        }
    }
}
