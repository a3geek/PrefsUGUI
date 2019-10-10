using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiImageLabel : PrefsGuiLabel
    {
        [SerializeField]
        protected RawImage rawImage = null;


        public Texture SetImage(Texture texture)
            => this.rawImage.texture = texture;

        public Texture GetImage()
            => this.rawImage.texture;

        protected override void Reset()
        {
            base.Reset();
            this.rawImage = this.elements?.GetComponentInChildren<RawImage>();
        }
    }
}
