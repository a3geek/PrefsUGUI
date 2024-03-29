﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Commons;
    using Guis.Preferences;
    using PrefsGuiBase = Preferences.PrefsGuiBase;

    public class LinkedGuiHierarchy : AbstractGuiHierarchy
    {
        public override RectTransform Content => this.target.Content;
        public override AbstractGuiHierarchy Previous => this.target.Previous;

        protected AbstractGuiHierarchy target = null;


        public LinkedGuiHierarchy(Guid hierarchyId, string hierarchyName, AbstractGuiHierarchy target)
        {
            this.target = target;
            this.HierarchyId = hierarchyId;
            this.HierarchyName = hierarchyName;
        }

        public override PrefsGuiButton GetNextButton(ref Guid hierarchyId)
            => this.target.GetNextButton(ref hierarchyId);

        public override AbstractGuiHierarchy GetNextHierarchy(ref Guid nextHierarchyId)
            => this.target.GetNextHierarchy(ref nextHierarchyId);

        public override int AddNextHierarchy(AbstractGuiHierarchy nextHierarchy, PrefsGuiButton button, int sortOrder)
            => this.target.AddNextHierarchy(nextHierarchy, button, sortOrder);

        public override bool TryRemoveNextHierarchy(AbstractGuiHierarchy nextHierarchy, out PrefsGuiButton button)
            => this.target.TryRemoveNextHierarchy(nextHierarchy, out button);

        public override int AddPrefs(Guid guid, PrefsGuiBase prefsGui, int sortOrder)
            => this.target.AddPrefs(guid, prefsGui, sortOrder);

        public override bool TryRemovePrefs(ref Guid guid, out PrefsGuiBase prefsGui)
            => this.target.TryRemovePrefs(ref guid, out prefsGui);

        public override void Discard()
            => this.target.Discard();
    }
}
