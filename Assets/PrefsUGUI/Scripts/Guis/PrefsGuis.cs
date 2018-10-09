using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Guis
{
    using Guis.Factories;
    using Creator = Dictionary<string, Action<Factories.PrefsCanvas>>;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(PrefsUGUI.Prefs.ExecutionOrder)]
    public class PrefsGuis : MonoBehaviour
    {
        public bool IsShowing
        {
            get { return this.canvas.gameObject.activeSelf; }
        }

        [SerializeField]
        private PrefsCanvas prefsCanvas = null;
        [SerializeField]
        private EventSystem eventSystem = null;

        private PrefsCanvas canvas = null;
        private EventSystem system = null;
        private Func<Creator> creator = null;

        
        public void Initialize(Func<Creator> creator)
        {
            this.creator = creator;

            this.canvas = this.CreateCanvas();
            this.Create();
        }

        private void Awake()
        {
            this.Create();
            this.system = EventSystem.current;
        }

        private void Update()
        {
            this.system = this.system ?? (this.eventSystem == null ? null : Instantiate(this.eventSystem));

            this.Create();
        }
        
        public void RemovePrefs(PrefsUGUI.Prefs.PrefsBase prefs)
        {
            if(this.canvas == null)
            {
                return;
            }

            this.canvas.RemovePrefs(prefs);
        }

        public void ShowGUI()
        {
            this.canvas.gameObject.SetActive(true);
        }

        public void SetSize(float width, float height)
        {
            var delta = this.canvas.Panel.sizeDelta;
            this.canvas.Panel.sizeDelta = new Vector2(
                width <= 0f ? delta.x : width,
                height <= 0f ? delta.y : height
            );
        }
        
        private void Create()
        {
            if(this.creator == null)
            {
                return;
            }

            var prefs = this.creator();
            foreach(var pair in prefs)
            {
                pair.Value(this.canvas);
            }

            prefs.Clear();
        }

        private PrefsCanvas CreateCanvas()
        {
            var canvas = Instantiate(this.prefsCanvas, Vector3.zero, Quaternion.identity, transform);
            canvas.gameObject.SetActive(false);

            return canvas;
        }
    }
}
