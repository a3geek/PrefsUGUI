using System;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Factories.Classes
{
    using Utilities;

    [Serializable]
    public class GuiLinks
    {
        public Canvas Canvas
        {
            get { return this.canvas; }
        }
        public RectTransform Panel
        {
            get { return this.panel; }
        }
        public Text Hierarchy
        {
            get { return this.hierarchy; }
        }
        public ScrollRect Scroll
        {
            get { return this.scrollrect; }
        }
        public RectTransform Viewport
        {
            get { return this.viewport; }
        }
        public RectTransform Content
        {
            get { return this.content; }
        }
        public Button Save
        {
            get { return this.save; }
        }
        public Button Close
        {
            get { return this.close; }
        }
        public Button Discard
        {
            get { return this.discard; }
        }

        [SerializeField]
        private Canvas canvas = null;
        [SerializeField]
        private RectTransform panel = null;
        [SerializeField]
        private Text hierarchy = null;
        [SerializeField]
        private ScrollRect scrollrect = null;
        [SerializeField]
        private RectTransform viewport = null;
        [SerializeField]
        private RectTransform content = null;
        [SerializeField]
        private Button discard = null;
        [SerializeField]
        private Button save = null;
        [SerializeField]
        private Button close = null;


        public void Reset(GameObject obj)
        {
            this.canvas = obj.GetComponent<Canvas>();
            this.scrollrect = obj.GetComponentInChildren<ScrollRect>();

            var dra = obj.GetComponentInChildren<Draggable>();
            this.panel = dra != null ? (RectTransform)dra.transform : null;

            var mask = obj.GetComponentInChildren<Mask>();
            this.viewport = mask != null ? (RectTransform)mask.transform : null;

            var content = obj.GetComponentInChildren<ContentSizeFitter>();
            this.content = content != null ? (RectTransform)content.transform : null;

            var buttons = obj.GetComponentsInChildren<Button>();
            this.discard = buttons.Length > 0 ? buttons[0] : null;
            this.save = buttons.Length > 1 ? buttons[1] : null;
            this.close = buttons.Length > 2 ? buttons[2] : null;
        }
    }
}
