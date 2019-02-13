using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsIntSlider : Prefs.PrefsParam<int, PrefsGuiNumericSlider>
    {
        protected int min = 0;
        protected int max = 0;


        public PrefsIntSlider(string key, int defaultValue = default(int), GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, defaultValue, hierarchy, guiLabel) { }

        public PrefsIntSlider(string key, int minValue, int maxValue,
            int defaultValue = default(int), GuiHierarchy hierarchy = null, string guiLabel = "")
            : this(key, defaultValue, hierarchy, guiLabel)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericSlider gui)
        {
            if(this.min == 0 && this.max == 0)
            {
                gui.Initialize(this.GuiLabel, this.Get(), () => this.DefaultValue);
            }
            else
            {
                gui.Initialize(this.GuiLabel, this.Get(), this.min, this.max, () => this.DefaultValue);
            }
        }
    }
}
