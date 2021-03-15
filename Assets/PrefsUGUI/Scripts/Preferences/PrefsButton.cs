using System;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;
    using Managers;

    [Serializable]
    public class PrefsButton : PrefsGuiBase<UnityAction, PrefsGuiButton>
    {
        public event UnityAction OnClicked = delegate { };


        public PrefsButton(
            string key, UnityAction action, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<UnityAction, PrefsGuiButton>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, action ?? delegate { }, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        public virtual void ManualClick()
            => this.properties.Gui.ManualClick();

        public override void Reload()
            => this.ResetDefaultValue();

        protected override void AddPrefsToSyncManager()
            => EditSyncManager.AddPrefs(this);

        protected override void OnCreatedGuiInternal(PrefsGuiButton gui)
        {
            gui.Initialize(this.GuiLabel, null);
            gui.OnClicked += this.FireOnClicked;
        }

        protected override void Regist()
            => this.OnRegisted();

        protected virtual void FireOnClicked()
        {
            this.OnClicked();
            this.FireOnEditedInGui();
            this.Value?.Invoke();
        }
    }
}
