using System;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsLabel : PrefsGuiBase<string, PrefsGuiLabel>
    {
        public PrefsLabel(
            string key, string text, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<string, PrefsGuiLabel>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, text ?? "", hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.Unsave = true;
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiLabel gui)
            => gui.Initialize(this.GuiLabel, this.Get());
    }
}
