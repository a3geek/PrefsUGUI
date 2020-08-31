﻿using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using Guis.Preferences;
    using Managers;

    public abstract class PrefsGuiBase<ValType, GuiType> : PrefsValueBase<ValType>, IReadOnlyPrefs<ValType>, IPrefsCommon
        where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
    {
        public override string GuiLabel => this.properties.GuiLabel;
        public virtual string GuiLabelWithoutAffix => this.properties.GuiLabelWithoutAffix;
        public virtual bool IsCreatedGui => this.properties.IsCreatedGui;
        public virtual float BottomMargin
        {
            get => this.properties.BottomMargin;
            set => this.properties.BottomMargin = value;
        }
        public virtual float TopMargin
        {
            get => this.properties.TopMargin;
            set => this.properties.TopMargin = value;
        }
        public virtual bool VisibleGUI
        {
            get => this.properties.VisibleGUI;
            set => this.properties.VisibleGUI = value;
        }
        public virtual string GuiLabelPrefix
        {
            get => this.properties.GuiLabelPrefix;
            set => this.properties.GuiLabelPrefix = value;
        }
        public virtual string GuiLabelSufix
        {
            get => this.properties.GuiLabelSufix;
            set => this.properties.GuiLabelSufix = value;
        }
        public override int GuiSortOrder
        {
            get => this.properties.GuiSortOrder;
            protected set => this.properties.GuiSortOrder = value;
        }

        protected PrefsGuiProperties<GuiType> properties = new PrefsGuiProperties<GuiType>();
        protected Action<PrefsGuiBase<ValType, GuiType>> onCreatedGui = null;


        public PrefsGuiBase(
            string key, ValType defaultValue = default, GuiHierarchy hierarchy = null, string guiLabel = null,
            Action<PrefsGuiBase<ValType, GuiType>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, defaultValue, hierarchy, guiLabel)
        {
            this.onCreatedGui = onCreatedGui;
            this.GuiSortOrder = sortOrder;
        }

        protected override void OnRegisted()
            => PrefsManager.AddPrefs<ValType, GuiType>(this, this.OnCreatedGui);

        protected virtual void OnCreatedGui(GuiType gui)
        {
            this.properties.OnCreatedGui(gui, this.guiLabel);

            this.OnCreatedGuiInternal(gui);
            this.onCreatedGui?.Invoke(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.disposed == true)
            {
                return;
            }

            this.properties.Dispose();
            this.onCreatedGui = null;
        }

        protected abstract void OnCreatedGuiInternal(GuiType gui);


        public static implicit operator ValType(PrefsGuiBase<ValType, GuiType> prefs)
            => prefs.Get();
    }
}
