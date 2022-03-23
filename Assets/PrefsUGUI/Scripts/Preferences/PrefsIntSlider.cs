using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsIntSlider : PrefsGuiBase<int, PrefsGuiNumericSliderInteger>
    {
        public int Min
        {
            get => this.min;
            set
            {
                void SetMin() => this.properties.Gui.MinValue = value;

                if(this.properties.Gui != null)
                {
                    SetMin();
                }
                else
                {
                    this.properties.OnCreatedGuiEvent += SetMin;
                }
            }
        }
        public int Max
        {
            get => this.max;
            set
            {
                void SetMax() => this.properties.Gui.MaxValue = value;

                if(this.properties.Gui != null)
                {
                    SetMax();
                }
                else
                {
                    this.properties.OnCreatedGuiEvent += SetMax;
                }
            }
        }

        [SerializeField]
        protected int min = 0;
        [SerializeField]
        protected int max = 0;


        public PrefsIntSlider(
            string key, int defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<int, PrefsGuiNumericSliderInteger>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        public PrefsIntSlider(
            string key, int minValue, int maxValue, int defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<int, PrefsGuiNumericSliderInteger>> onCreatedGui = null, int sortOrder = 0
        )
            : this(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericSliderInteger gui)
        {
            if(this.min == this.max)
            {
                gui.Initialize(this.GuiLabel, this.Get());
            }
            else
            {
                gui.Initialize(this.GuiLabel, this.Get(), this.min, this.max);
            }
        }
    }
}
