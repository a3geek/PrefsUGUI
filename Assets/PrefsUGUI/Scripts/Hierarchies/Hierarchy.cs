using System;

namespace PrefsUGUI
{
    using Hierarchies.Abstracts;
    using Guis.Preferences;
    using Managers;
    using static Prefs;

    [Serializable]
    public class Hierarchy : AbstractHierarchy
    {
        protected Action<Hierarchy> onCreatedGui = null;


        public Hierarchy(
            string hierarchyName, int sortOrder = DefaultSortOrder, Hierarchy parent = null,
            string saveKey = "", Action<Hierarchy> onCreatedGui = null
        )
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.saveKey = saveKey;
            this.parent = parent;
            this.sortOrder = sortOrder;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();
            this.SaveKeyPath = this.GetFullSaveKeyPath();

            this.onCreatedGui = onCreatedGui;

            this.Regist();
        }

        protected override void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiButton>(this, this.OnCreatedGuiButton);

        protected override void FireOnCreatedGui()
            => this.onCreatedGui?.Invoke(this);

        #region IDisposable Support
        ~Hierarchy()
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
