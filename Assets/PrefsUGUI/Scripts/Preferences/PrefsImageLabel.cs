using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI
{
    using Guis.Preferences;

    [Serializable]
    public class PrefsImageLabel : Prefs.PrefsGuiBase<string, PrefsGuiImageLabel>
    {
        public Texture Image
        {
            get => this.gui == null ? null : this.gui.GetImage();
            set
            {
                if(this.gui != null)
                {
                    this.gui.SetImage(value);
                }
            }
        }


        public PrefsImageLabel(
            string key, string text, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<string, PrefsGuiImageLabel>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, text ?? "", hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        public override void Reload(bool withEvent = true)
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiImageLabel gui)
            => gui.Initialize(this.GuiLabel, this.Get());

        protected override void Regist()
            => this.OnRegisted();
    }
}
