using System;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Managers.Classes;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsButton : PrefsGuiBase<UnityAction, PrefsGuiButton>
    {
        public event UnityAction OnClicked = delegate { };


        public PrefsButton(
            string key, UnityAction action, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<UnityAction, PrefsGuiButton>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, action ?? delegate { }, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.UnEditSync = true;
        }

        public virtual void ManualClick()
            => this.properties.Gui.ManualClick();

        public override void Reload()
            => this.ResetDefaultValue();

        protected override PrefsType GetPrefsType()
            => PrefsType.PrefsButton;

        protected override void OnReceivedEditSyncMessage(EditSyncElement.Message message)
            => this.ManualClick();

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
