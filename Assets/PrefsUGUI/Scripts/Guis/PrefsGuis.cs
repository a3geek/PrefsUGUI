using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Guis
{
    using Guis.Factories;
    using Guis.Preferences;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(ExecutionOrder)]
    public class PrefsGuis : MonoBehaviour
    {
        public const int ExecutionOrder = -30000;
        public const string PrefsGuisPrefabName = "PrefsGuis";

        public bool IsShowing
        {
            get => this.Canvas.gameObject.activeSelf;
        }
        public PrefsCanvas Canvas { get; private set; } = null;

        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("prefsCanvas")]
        private PrefsCanvas prefsCanvasPrefab = null;
        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("eventSystem")]
        private EventSystem eventSystemPrefab = null;

        private Func<ConcurrentQueue<Action>> prfsActionsCacheGetter = null;


        private void Awake()
        {
            this.Canvas = this.CreateCanvas();
            if (EventSystem.current == null)
            {
                this.CreateEventSystem();
            }

            this.DequeuePrefsActionsCache();
        }

        private void Start()
        {
            this.DequeuePrefsActionsCache();
        }

        private void Update()
        {
            this.DequeuePrefsActionsCache();
        }

        public void SetPrefsActionsCacheGetter(Func<ConcurrentQueue<Action>> getter)
            => this.prfsActionsCacheGetter = this.prfsActionsCacheGetter ?? getter;

        public void ShowGUI()
        {
            if (this.Canvas != null)
            {
                this.Canvas.gameObject.SetActive(true);
            }
        }

        public void AddPrefs<ValType, GuiType>(Prefs.PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            if (this.Canvas != null)
            {
                onCreated?.Invoke(this.Canvas.AddPrefs<ValType, GuiType>(prefs));
            }
        }

        public void RemovePrefs(string prefsSaveKey)
        {
            if (this.Canvas != null)
            {
                this.Canvas.RemovePrefs(prefsSaveKey);
            }
        }

        public void RemoveCategory(string fullHierarchyName)
        {
            if (this.Canvas != null)
            {
                this.Canvas.RemoveCategory(fullHierarchyName);
            }
        }

        public void SetCanvasSize(float width, float height)
        {
            if (this.Canvas == null)
            {
                return;
            }

            var delta = this.Canvas.Panel.sizeDelta;
            this.Canvas.Panel.sizeDelta = new Vector2(
                width <= 0f ? delta.x : width,
                height <= 0f ? delta.y : height
            );
        }

        private void DequeuePrefsActionsCache()
        {
            if (this.prfsActionsCacheGetter == null)
            {
                return;
            }

            var queue = this.prfsActionsCacheGetter.Invoke();
            while (queue.TryDequeue(out var action) == true)
            {
                action?.Invoke();
            }
        }

        private PrefsCanvas CreateCanvas()
        {
            var canvas = Instantiate(this.prefsCanvasPrefab, Vector3.zero, Quaternion.identity, this.transform);
            canvas.gameObject.SetActive(false);

            return canvas;
        }

        private EventSystem CreateEventSystem()
            => Instantiate(this.eventSystemPrefab, Vector3.zero, Quaternion.identity, this.transform);
    }
}
