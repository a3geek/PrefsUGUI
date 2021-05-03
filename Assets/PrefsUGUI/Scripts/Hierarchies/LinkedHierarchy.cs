using System;

namespace PrefsUGUI
{
    using Hierarchies.Abstracts;
    using Guis;
    using Guis.Preferences;
    using Managers;
    using static Prefs;

    [Serializable]
    public class LinkedHierarchy : AbstractHierarchy
    {
        public virtual Hierarchy LinkTarget { get; protected set; }
        public override HierarchyType HierarchyType => HierarchyType.Linked;

        protected Action<LinkedHierarchy> onCreatedGui = null;


        public LinkedHierarchy(
            string hierarchyName, Hierarchy linkTarget, int sortOrder = DefaultSortOrder,
            Hierarchy parent = null, Action<LinkedHierarchy> onCreatedGui = null
        )
            : base(hierarchyName)
        {
            this.parent = parent;
            this.LinkTarget = linkTarget;
            this.properties.GuiSortOrder = sortOrder;
            this.onCreatedGui = onCreatedGui;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();
            this.SaveKeyPath = this.LinkTarget.SaveKeyPath;

            this.Regist();
        }

        protected override void Regist()
            => PrefsManager.AddLinkedGuiHierarchy<PrefsGuiButton>(this, this.OnCreatedGuiButton);

        protected override void FireOnCreatedGui()
            => this.onCreatedGui?.Invoke(this);

        #region IDisposable Support
        ~LinkedHierarchy()
        {
            this.Dispose(false);
        }

        protected override void DisposeInternal(bool disposing)
        {
            base.DisposeInternal(disposing);
            this.onCreatedGui = null;
        }
        #endregion
    }
}
