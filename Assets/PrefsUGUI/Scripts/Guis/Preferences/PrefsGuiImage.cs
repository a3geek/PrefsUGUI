using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    using Factories.Classes;
    using PrefsUGUI.Preferences.Abstracts;

    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiImage : PrefsGuiBase, IPrefsGuiConnector<string, PrefsGuiImage>
    {
        public virtual PrefsGuiImage Component => this;

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

        public virtual void Initialize(string label)
        {
            this.SetLabel(label);
        }

        public void SetImage(Texture texture)
            => this.rawImage.texture = texture;

        public Texture GetImage()
            => this.rawImage.texture;

        public virtual string GetValue()
            => string.Empty;

        public virtual void SetValue(string text)
        {
        }

        public virtual void SetGuiListeners(PrefsValueBase<string> prefs, IPrefsGuiEvents<string, PrefsGuiImage> prefsEventer, AbstractGuiHierarchy hierarchy)
        {
        }
    }
}
