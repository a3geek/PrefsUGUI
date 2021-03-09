using System;

namespace PrefsUGUI
{
    using GuiHierarchies.Abstracts;
    using static Prefs;

    [Serializable]
    public class GuiHierarchy : AbstractGuiHierarchy
    {
        protected Action<GuiHierarchy> onCreatedGui = null;


        public GuiHierarchy(
            string hierarchyName, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null,
            Action<GuiHierarchy> onCreatedGui = null
        )
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.parent = parent;
            this.sortOrder = sortOrder;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();
            this.SaveKeyPath = this.FullHierarchy;

            this.onCreatedGui = onCreatedGui;

            this.Regist();
        }

        protected override void FireOnCreatedGui()
            => this.onCreatedGui?.Invoke(this);

        #region IDisposable Support
        ~GuiHierarchy()
        {
            this.Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.onCreatedGui = null;
        }
        #endregion
    }
}
