using System;

namespace PrefsUGUI
{
    using GuiHierarchies.Abstracts;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using static Prefs;

    [Serializable]
    public class LinkedGuiHierarchy : AbstractGuiHierarchy
    {
        protected Action<LinkedGuiHierarchy> onCreatedGui = null;
        protected GuiHierarchy linkParent = null;


        public LinkedGuiHierarchy(
            string hierarchyName, GuiHierarchy linkParent, int sortOrder = DefaultSortOrder, GuiHierarchy parent = null,
            Action<LinkedGuiHierarchy> onCreatedGui = null
        )
        {
            this.hierarchyName = hierarchyName.Replace(HierarchySeparator.ToString(), string.Empty);
            this.parent = parent;
            this.linkParent = linkParent;
            this.sortOrder = sortOrder;
            this.onCreatedGui = onCreatedGui;

            this.HierarchyId = Guid.NewGuid();
            this.Parents = this.GetParents();
            this.FullHierarchy = this.GetFullHierarchy();

            this.Regist();
        }

        protected override void OnCreatedGuiButton(PrefsCanvas canvas, Category category, PrefsGuiButton gui)
        {
            this.properties.OnCreatedGui(gui, this.HierarchyName);

            this.changeGUI = () => this.linkParent?.Open(false);

            gui.Initialize(this.HierarchyName, () =>
            {
                this.FireOnHierarchyClicked();
                this.changeGUI();
            });
            this.onCreatedGui?.Invoke(this);
        }

        protected override void FireOnCreatedGui()
            => this.onCreatedGui?.Invoke(this);

        #region IDisposable Support
        ~LinkedGuiHierarchy()
        {
            this.Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.disposed == true)
            {
                return;
            }

            this.onCreatedGui = null;
        }
        #endregion
    }
}
