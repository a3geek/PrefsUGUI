using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsLabel : Prefs.PrefsGuiBaseConnector<string, PrefsGuiLabel>
    {
        public PrefsLabel(string key, string text, GuiHierarchy hierarchy = null, string guiLabel = null)
            : base(key, text ?? "", hierarchy, guiLabel)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiLabel gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
            this.OnValueChanged += () => gui.SetValue(this.Get());
        }

        protected override void Regist()
            => this.AfterRegist();
    }
}
