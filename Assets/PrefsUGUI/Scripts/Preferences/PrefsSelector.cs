using System;
using System.Collections.Generic;
using System.Linq;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsSelector : PrefsGuiBase<int, PrefsGuiSelector>
    {
        public string SelectOption => this.options.ElementAtOrDefault(this.Value) ?? string.Empty;

        private readonly List<string> options = new List<string>();


        public PrefsSelector(
            string key, List<string> options, int defaultValue = 0, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<int, PrefsGuiSelector>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, Convert.ToInt32(defaultValue), hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.options = options;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiSelector gui)
            => gui.Initialize(this.GuiLabel, this.options, this.Get());
    }
}
