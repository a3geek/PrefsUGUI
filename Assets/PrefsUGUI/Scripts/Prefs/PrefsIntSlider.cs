using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsIntSlider : Prefs.PrefsParam<int, PrefsGuiNumericSlider>
    {
        protected int min = 0;
        protected int max = 0;


        public PrefsIntSlider(string key, int defaultValue = default(int), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        public PrefsIntSlider(string key, int minValue, int maxValue,
            int defaultValue = default(int), string guiHierarchy = "", string guiLabel = "")
            : this(key, defaultValue, guiHierarchy, guiLabel)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGui(PrefsGuiNumericSlider gui)
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
