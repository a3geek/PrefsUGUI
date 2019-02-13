using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;

    public static partial class Prefs
    {
        [Serializable]
        public abstract class PrefsGuiConnector<GuiType> : PrefsBase where GuiType : InputGuiBase
        {
            public override string GuiLabel => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
            public virtual string GuiLabelWithoutAffix => this.guiLabel;

            public virtual float BottomMargin
            {
                get { return this.gui == null ? 0f : this.gui.BottomMargin; }
                set
                {
                    if(this.gui != null)
                    {
                        this.gui.BottomMargin = value;
                    }
                }
            }
            public virtual bool VisibleGUI
            {
                get { return this.gui == null ? false : this.gui.gameObject.activeSelf; }
                set
                {
                    if(this.gui != null)
                    {
                        this.gui.gameObject.SetActive(value);
                    }
                }
            }
            public string GuiLabelPrefix
            {
                get { return this.guiLabelPrefix; }
                set
                {
                    this.guiLabelPrefix = value ?? "";
                    this.UpdateLabel();
                }
            }
            public string GuiLabelSufix
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


            public PrefsGuiConnector(string key, GuiHierarchy hierarchy = null, string guiLabel = "")
                : base(key, hierarchy, guiLabel)
            {
                ;
            }

            protected virtual void UpdateLabel()
            {
                this.gui?.SetLabel(this.GuiLabel);
            }

            protected override void Regist()
            {
                base.Regist();
                AddPrefs<GuiType>(this, gui =>
                {
                    this.gui = gui;
                    this.OnCreatedGuiInternal(gui);
                });
            }

            protected abstract void OnCreatedGuiInternal(GuiType gui);
        }
    }
}
