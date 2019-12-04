using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsIntSlider : Prefs.PrefsExtends<int, PrefsGuiNumericSliderInteger>
    {
        protected int min = 0;
        protected int max = 0;


        public PrefsIntSlider(string key, int defaultValue = default(int), GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBaseConnector<int, PrefsGuiNumericSliderInteger>> onCreatedGui = null)
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui) { }

        public PrefsIntSlider(string key, int minValue, int maxValue,
            int defaultValue = default(int), GuiHierarchy hierarchy = null, string guiLabel = null,
            Action<Prefs.PrefsGuiBaseConnector<int, PrefsGuiNumericSliderInteger>> onCreatedGui = null)
            : this(key, defaultValue, hierarchy, guiLabel, onCreatedGui)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericSliderInteger gui)
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
