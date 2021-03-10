using System;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

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

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiButton gui)
        {
            gui.Initialize(this.GuiLabel, this.FireOnClicked);
        }

        protected override void Regist()
            => this.OnRegisted();

        protected virtual void FireOnClicked()
        {
            this.OnClicked();
            this.Value?.Invoke();
        }
    }
}
