using System;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Guis;

    [Serializable]
    public class PrefsButton : Prefs.PrefsGuiBase<UnityAction, PrefsGuiButton>
    {
        public UnityAction OnClicked
        {
            get { return this.Value; }
            set { this.Value = value; }
        }

        public PrefsButton(
            string key, UnityAction action, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<UnityAction, PrefsGuiButton>> onCreatedGui = null
        )
            : base(key, action ?? delegate { }, hierarchy, guiLabel, onCreatedGui)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiButton gui)
            => gui.Initialize(this.GuiLabel, this.Get());

        protected override void Regist()
            => this.OnRegisted();
    }
}
