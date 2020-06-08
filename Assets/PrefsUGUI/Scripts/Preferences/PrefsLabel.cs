using System;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsLabel : Prefs.PrefsGuiBase<string, PrefsGuiLabel>
    {
        public PrefsLabel(
            string key, string text, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<string, PrefsGuiLabel>> onCreatedGui = null
        )
            : base(key, text ?? "", hierarchy, guiLabel, onCreatedGui)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiLabel gui)
            => gui.Initialize(this.GuiLabel, this.Get());

        protected override void Regist()
            => this.OnRegisted();
    }
}
