using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsGuiBaseConnector<ValType, GuiType> : PrefsValueBase<ValType>, IReadOnlyPrefs<ValType> where GuiType : PrefsGuiBase
        {
            public event Action OnCreatedGui = delegate { };

            public override string GuiLabel => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
            public virtual string GuiLabelWithoutAffix => this.guiLabel;

            public virtual float BottomMargin
            {
                get { return this.gui == null ? 0f : this.gui.GetBottomMargin(); }
                set { this.gui?.SetBottomMargin(Mathf.Max(0f, value)); }
            }
            public virtual float TopMargin
            {
                get { return this.gui == null ? 0f : this.gui.GetTopMargin(); }
                set { this.gui?.SetTopMargin(Mathf.Max(0f, value)); }
            }
            public virtual bool VisibleGUI
            {
                get { return this.gui == null ? false : this.gui.gameObject.activeSelf; }
                set { this.gui?.gameObject.SetActive(value); }
            }
            public virtual string GuiLabelPrefix
            {
                get { return this.guiLabelPrefix; }
                set
                {
                    this.guiLabelPrefix = value ?? "";
                    this.UpdateLabel();
                }
            }
            public virtual string GuiLabelSufix
            {
                get { return this.guiLabelSufix; }
                set
                {
                    this.guiLabelSufix = value ?? "";
                    this.UpdateLabel();
                }
            }

            [SerializeField]
            protected string guiLabelPrefix = "";
            [SerializeField]
            protected string guiLabelSufix = "";

            protected GuiType gui = null;


            public PrefsGuiBaseConnector(string key, ValType defaultValue = default(ValType), GuiHierarchy hierarchy = null, string guiLabel = null)
                : base(key, defaultValue, hierarchy, guiLabel)
            {
            }

            protected override void AfterRegist()
                => AddPrefs<GuiType>(this, gui => this.ExecuteOnCreatedGui(gui));

            protected void ExecuteOnCreatedGui(GuiType gui)
            {
                this.gui = gui;
                this.OnCreatedGuiInternal(gui);
                this.OnCreatedGui();
            }

            protected abstract void OnCreatedGuiInternal(GuiType gui);

            protected virtual void UpdateLabel()
                => this.gui?.SetLabel(this.GuiLabel);
        }
    }
}
