using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsGuiProperties<GuiType> : IDisposable where GuiType : PrefsGuiBase
    {
        protected event Action OnCreatedGuiEvent = delegate { };

        public GuiType Gui => this.gui;
        public string GuiLabel
        {
            get => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
            set
            {
                this.guiLabel = value ?? this.guiLabel;
                this.UpdateLabel();
            }
        }
        public string GuiLabelWithoutAffix => this.guiLabel;
        public bool IsCreatedGui => this.gui != null;
        public float BottomMargin
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
                    this.OnCreatedGuiEvent += SetBottomMargin;
                }
            }
        }
        public float TopMargin
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
                    this.OnCreatedGuiEvent += SetTopMargin;
                }
            }
        }
        public bool VisibleGUI
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
                    this.OnCreatedGuiEvent += SetVisible;
                }
            }
        }
        public string GuiLabelPrefix
        {
            get => this.guiLabelPrefix;
            set
            {
                this.guiLabelPrefix = value ?? "";
                this.UpdateLabel();
            }
        }
        public string GuiLabelSufix
        {
            get => this.guiLabelSufix;
            set
            {
                this.guiLabelSufix = value ?? "";
                this.UpdateLabel();
            }
        }
        public int GuiSortOrder { get; set; } = 0;


        [SerializeField]
        protected string guiLabel = "";
        [SerializeField]
        protected string guiLabelPrefix = "";
        [SerializeField]
        protected string guiLabelSufix = "";

        protected GuiType gui = null;


        public PrefsGuiProperties(string guiLabel)
        {
            this.guiLabel = guiLabel;
        }

        public void OnCreatedGui(GuiType gui)
        {
            this.gui = gui;
            this.UpdateLabel();
            this.OnCreatedGuiEvent();
        }

        public void Dispose()
        {
            this.gui = null;
            this.OnCreatedGuiEvent = null;
        }

        protected void UpdateLabel()
        {
            void SetLabel() => this.gui.SetLabel(this.GuiLabel);

            if (this.gui != null)
            {
                SetLabel();
            }
            else
            {
                this.OnCreatedGuiEvent += SetLabel;
            }
        }
    }
}
