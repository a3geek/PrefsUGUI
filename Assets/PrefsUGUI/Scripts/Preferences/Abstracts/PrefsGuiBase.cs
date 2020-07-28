using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using Guis.Preferences;
    using Managers;
    using UnityEngine.Events;

    public abstract class PrefsGuiBase<ValType, GuiType> : PrefsValueBase<ValType>, IReadOnlyPrefs<ValType>, IPrefsCommon
        where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
    {
        protected virtual event Action onCreatedGuiEvent = delegate { };

        public override string GuiLabel => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
        public virtual string GuiLabelWithoutAffix => base.GuiLabel;
        public virtual bool IsCreatedGui => this.gui != null;
        public virtual float BottomMargin
        {
            get => this.gui == null ? 0f : this.gui.GetBottomMargin();
            set
            {
                void SetBottomMargin() => this.gui.SetBottomMargin(Mathf.Max(0f, value));

                if (this.gui != null)
                {
                    SetBottomMargin();
                }
                else
                {
                    this.onCreatedGuiEvent += SetBottomMargin;
                }
            }
        }
        public virtual float TopMargin
        {
            get => this.gui == null ? 0f : this.gui.GetTopMargin();
            set
            {
                void SetTopMargin() => this.gui.SetTopMargin(Mathf.Max(0f, value));

                if (this.gui != null)
                {
                    SetTopMargin();
                }
                else
                {
                    this.onCreatedGuiEvent += SetTopMargin;
                }
            }
        }
        public virtual bool VisibleGUI
        {
            get => this.gui != null && this.gui.GetVisible();
            set
            {
                void SetVisible() => this.gui.SetVisible(value);

                if (this.gui != null)
                {
                    SetVisible();
                }
                else
                {
                    this.onCreatedGuiEvent += SetVisible;
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
            void SetLabel() => this.gui.SetLabel(this.GuiLabel);

            if (this.gui != null)
            {
                SetLabel();
            }
            else
            {
                this.onCreatedGuiEvent += SetLabel;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.disposed == true)
            {
                return;
            }

            this.gui = null;
            this.onCreatedGui = null;
            this.onCreatedGuiEvent = null;
        }

        protected abstract void OnCreatedGuiInternal(GuiType gui);


        public static implicit operator ValType(PrefsGuiBase<ValType, GuiType> prefs)
            => prefs.Get();
    }
}
