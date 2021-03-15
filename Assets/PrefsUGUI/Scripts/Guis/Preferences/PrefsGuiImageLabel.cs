using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Factories.Classes;
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiImageLabel : PrefsGuiLabel, IPrefsGuiConnector<string, PrefsGuiImageLabel>
    {
        public new virtual PrefsGuiImageLabel Component => this;

        [SerializeField]
        protected RawImage rawImage = null;


        protected override void Reset()
        {
            base.Reset();

            if(this.elements != null)
            {
                this.rawImage = this.elements.GetComponentInChildren<RawImage>();
            }
        }

        public void SetImage(Texture texture)
            => this.rawImage.texture = texture;

        public Texture GetImage()
            => this.rawImage.texture;

        public virtual void SetGuiListeners(PrefsValueBase<string> prefs, IPrefsGuiEvents<string, PrefsGuiImageLabel> prefsEventer, AbstractGuiHierarchy hierarchy)
        {
        }
    }
}
