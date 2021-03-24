using System;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Preferences;
    using Managers;

    [Serializable]
    public partial class RemovableHierarchy : Hierarchy
    {
        public event Action OnRemoved = delegate { };

        public override HierarchyType HierarchyType => HierarchyType.Removable;
        public virtual bool UnEditSync { get; set; } = false;

        protected EditSyncElement element = null;


        public RemovableHierarchy(
            string hierarchyName, Action onRemoved = null, int sortOrder = DefaultSortOrder,
            Hierarchy parent = null, string saveKey = "", Action<Hierarchy> onCreatedGui = null
        )
            : base(hierarchyName, sortOrder, parent, saveKey, onCreatedGui)
        {
            if (onRemoved != null)
            {
                this.OnRemoved += onRemoved;
            }

            this.element = new EditSyncElement(this);
        }

        public virtual void ManualRemove()
            => this.FireOnRemoved();

        protected override void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiRemovableButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsGuiRemovableButton gui)
        {
            this.properties.OnCreatedGui(gui, this.HierarchyName);

            gui.Initialize(this.HierarchyName, this.FireOnHierarchyClicked, this.FireOnRemoved);
            EditSyncManager.AddElement(this.element);

            this.FireOnCreatedGui();
        }

        protected virtual void FireOnRemoved()
        {
            this.OnRemoved.Invoke();
            this.Dispose();
        }

        protected override void DisposeInternal(bool disposing)
        {
            base.DisposeInternal(disposing);

            EditSyncManager.RemoveElement(this.element);
            this.OnRemoved = null;
        }
    }
}
