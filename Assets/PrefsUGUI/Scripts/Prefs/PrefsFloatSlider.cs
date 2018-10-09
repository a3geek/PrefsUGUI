using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsFloatSlider : Prefs.PrefsParam<float, PrefsGuiNumericSlider>
    {
        protected float min = 0f;
        protected float max = 0f;


        public PrefsFloatSlider(string key, float defaultValue = default(float), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel) { }

        public PrefsFloatSlider(string key, float minValue, float maxValue,
            float defaultValue = default(float), string guiHierarchy = "", string guiLabel = "")
            : this(key, defaultValue, guiHierarchy, guiLabel)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericSlider gui)
        {
            if(this.min == 0f && this.max == 0f)
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
