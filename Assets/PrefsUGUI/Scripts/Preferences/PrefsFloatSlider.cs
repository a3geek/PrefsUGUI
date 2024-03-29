﻿using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Preferences.Abstracts;
    using Guis.Preferences;

    [Serializable]
    public class PrefsFloatSlider : PrefsGuiBase<float, PrefsGuiNumericSliderDecimal>
    {
        public float Min
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
        public float Max
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
        protected float min = 0f;
        [SerializeField]
        protected float max = 0f;


        public PrefsFloatSlider(
            string key, float defaultValue = default, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<float, PrefsGuiNumericSliderDecimal>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        public PrefsFloatSlider(
            string key, float minValue, float maxValue, float defaultValue = default(float), Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<float, PrefsGuiNumericSliderDecimal>> onCreatedGui = null, int sortOrder = 0
        )
            : this(key, defaultValue, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.min = minValue;
            this.max = maxValue;
        }

        protected override void OnCreatedGuiInternal(PrefsGuiNumericSliderDecimal gui)
        {
            if (this.min == this.max)
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
