using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsImageLabel : Prefs.PrefsGuiBaseConnector<string, PrefsGuiImageLabel>
    {
        public Texture Image
        {
            get { return this.gui?.GetImage(); }
            set { this.gui?.SetImage(value); }
        }


        public PrefsImageLabel(string key, string text, GuiHierarchy hierarchy = null, string guiLabel = null)
            : base(key, text ?? "", hierarchy, guiLabel)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiImageLabel gui)
        {
            gui.Initialize(this.GuiLabel, this.Get());
            this.OnValueChanged += () => gui.SetValue(this.Get());
        }

        protected override void Regist()
            => this.AfterRegist();
    }
}
