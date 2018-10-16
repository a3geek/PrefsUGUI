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

            protected GuiType gui = null;


            public PrefsGuiConnector(string key, string guiHierarchy = "", string guiLabel = "")
                : base(key, guiHierarchy, guiLabel)
            {
                ;
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
