using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Preferences;
    using Preferences.Abstracts;

    [Serializable]
    public class PrefsImage : PrefsGuiBase<string, PrefsGuiImage>
    {
        public Texture Image
        {
            get => this.properties.Gui == null ? null : this.properties.Gui.GetImage();
            set
            {
                void SetImage() => this.properties.Gui.SetImage(value);

                if(this.properties.Gui != null)
                {
                    SetImage();
                }
                else
                {
                    this.properties.OnCreatedGuiEvent += SetImage;
                }
            }
        }


        public PrefsImage(
            string key, Hierarchy hierarchy = null,
            string guiLabel = null, Action<PrefsGuiBase<string, PrefsGuiImage>> onCreatedGui = null, int sortOrder = 0
        )
            : base(key, string.Empty, hierarchy, guiLabel, onCreatedGui, sortOrder)
        {
            this.Unsave = true;
        }

        public override void Reload()
            => this.ResetDefaultValue();

        protected override void OnCreatedGuiInternal(PrefsGuiImage gui)
            => gui.Initialize(this.GuiLabel);

        protected override void Regist()
            => this.OnRegisted();
    }
}
