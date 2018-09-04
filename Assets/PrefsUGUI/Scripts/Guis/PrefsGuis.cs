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
    [DefaultExecutionOrder(DefaultExecutionOrder)]
    public class PrefsGuis : MonoBehaviour
    {
        public const int DefaultExecutionOrder = -32000;

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
        private Func<Creator> creatorGetter = null;


        private void Awake()
        {
            this.system = EventSystem.current;
        }

        private void Update()
        {
            if(this.prefsCanvas == null || this.eventSystem == null)
            {
                return;
            }

            this.system = this.system ?? Instantiate(this.eventSystem);
            this.canvas = this.canvas ?? this.CreateCanvas();
            this.Create();
        }

        public void SetCreatorGetter(Func<Creator> getter)
        {
            this.creatorGetter = this.creatorGetter ?? getter;
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
            if(this.creatorGetter == null)
            {
                return;
            }

            var prefs = this.creatorGetter();
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
