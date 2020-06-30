using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using Guis.Preferences;
    using Managers;

    public abstract class PrefsGuiBase<ValType, GuiType> : PrefsValueBase<ValType>, IReadOnlyPrefs<ValType>
        where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
    {
        public override string GuiLabel => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
        public virtual string GuiLabelWithoutAffix => base.GuiLabel;

        public virtual float BottomMargin
        {
            get => this.gui == null ? 0f : this.gui.GetBottomMargin();
            set
            {
                if (this.gui != null)
                {
                    this.gui.SetBottomMargin(Mathf.Max(0f, value));
                }
            }
        }
        public virtual float TopMargin
        {
            get => this.gui == null ? 0f : this.gui.GetTopMargin();
            set
            {
                if (this.gui != null)
                {
                    this.gui.SetTopMargin(Mathf.Max(0f, value));
                }
            }
        }
        public virtual bool VisibleGUI
        {
            get => this.gui != null && this.gui.GetVisible();
            set
            {
                if (this.gui != null)
                {
                    this.gui.SetVisible(value);
                }
            }
        }
        public virtual string GuiLabelPrefix
        {
            get => this.guiLabelPrefix;
            set
            {
                this.guiLabelPrefix = value ?? "";
                this.UpdateLabel();
            }
        }
        public virtual string GuiLabelSufix
        {
            get => this.guiLabelSufix;
            set
            {
                this.guiLabelSufix = value ?? "";
                this.UpdateLabel();
            }
        }
        public override int GuiSortOrder
        {
            get; protected set;
        }

        [SerializeField]
        protected string guiLabelPrefix = "";
        [SerializeField]
        protected string guiLabelSufix = "";

        protected GuiType gui = null;
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

        public virtual void OverrideGui(PrefsGuiBase<ValType, GuiType> src)
        {
            this.key = src.key;
            this.guiLabel = src.guiLabel;

            this.GuiSortOrder = src.GuiSortOrder;
            this.BottomMargin = src.BottomMargin;
            this.TopMargin = src.TopMargin;
            this.guiLabelPrefix = src.GuiLabelPrefix;
            this.guiLabelSufix = src.GuiLabelSufix;
            this.VisibleGUI = src.VisibleGUI;

            this.UpdateLabel();
        }

        protected override void OnRegisted()
            => PrefsManager.AddPrefs<ValType, GuiType>(this, this.OnCreatedGui);

        protected virtual void OnCreatedGui(GuiType gui)
        {
            this.gui = gui;

            this.OnCreatedGuiInternal(gui);
            this.onCreatedGui?.Invoke(this);
        }

        protected virtual void UpdateLabel()
        {
            if (this.gui != null)
            {
                this.gui.SetLabel(this.GuiLabel);
            }
        }

        protected abstract void OnCreatedGuiInternal(GuiType gui);


        public static implicit operator ValType(PrefsGuiBase<ValType, GuiType> prefs)
            => prefs.Get();
    }
}
