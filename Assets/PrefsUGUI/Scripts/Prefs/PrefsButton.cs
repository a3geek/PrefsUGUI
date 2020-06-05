using System;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Guis;

    [Serializable]
    public class PrefsButton : Prefs.PrefsGuiBase<UnityAction, GuiButton>
    {
        public UnityAction OnClicked
        {
            get { return this.Value; }
            set { this.Value = value; }
        }

        public PrefsButton(
            string key, UnityAction action, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<UnityAction, GuiButton>> onCreatedGui = null
        )
            : base(key, action ?? delegate { }, hierarchy, guiLabel, onCreatedGui)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(GuiButton gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
            this.OnValueChanged += () => gui.SetValue(this.Get());
        }

        protected override void Regist()
            => this.OnRegisted();
    }
}
