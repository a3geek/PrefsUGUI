using System;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Managers;
    using Managers.Classes;

    [Serializable]
    public class RemovableHierarchy : Hierarchy
    {
        public event Action OnRemoved = delegate { };

        public override HierarchyType HierarchyType => HierarchyType.Removable;


        public RemovableHierarchy(
            string hierarchyName, Action onRemoved = null, int sortOrder = DefaultSortOrder,
            Hierarchy parent = null, Action<Hierarchy> onCreatedGui = null
        )
            : base(hierarchyName, sortOrder, parent, onCreatedGui)
        {
            if (onRemoved != null)
            {
                this.OnRemoved += onRemoved;
            }
        }

        public virtual void ManualRemove()
            => this.FireOnRemoved();

        public virtual EditSyncRemoveHierarchyMessage GetEditSyncRemoveHierarchyMessage()
            => new EditSyncRemoveHierarchyMessage(this.SaveKeyPath, PrefsType.RemovableHierarchy);

        protected override void Regist()
            => PrefsManager.AddGuiHierarchy<PrefsGuiRemovableButton>(this, this.OnCreatedGuiButton);

        protected virtual void OnCreatedGuiButton(PrefsGuiRemovableButton gui)
        {
            this.properties.OnCreatedGui(gui, this.HierarchyName);

            gui.Initialize(this.HierarchyName, this.FireOnHierarchyClicked, this.FireOnRemoved);
            EditSyncManager.AddRemovableHierarchy(this);

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
            EditSyncManager.RemoveRemovableHierarchy(this);
            this.OnRemoved = null;
        }
    }
}
