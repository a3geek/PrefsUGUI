using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsImageLabel : PrefsGuiBase<string, PrefsGuiImageLabel>
    {
        public Texture Image
        {
            get => this.properties.Gui == null ? null : this.properties.Gui.GetImage();
            set
            {
                if (this.properties.Gui != null)
                {
                    this.properties.Gui.SetImage(value);
                }
            }
        }


        public PrefsImageLabel(
            string key, string text, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<string, PrefsGuiImageLabel>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, text ?? "", hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
        }

        public override void Reload()
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiImageLabel gui)
            => gui.Initialize(this.GuiLabel, this.Get());

        protected override void Regist()
            => this.OnRegisted();
    }
}
