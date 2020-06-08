using System;
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

        private Queue<Action> prefsCreators = new Queue<Action>();


        private void Awake()
        {
            this.Canvas = this.CreateCanvas();
            if(EventSystem.current == null)
            {
                this.CreateEventSystem();
            }

            this.Create();
        }

        private void Update()
        {
            this.Create();
        }

        public void ShowGUI()
        {
            if(this.Canvas != null)
            {
                this.Canvas.gameObject.SetActive(true);
            }
        }

        public void AddPrefs<ValType, GuiType>(Prefs.PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void Creator() => onCreated?.Invoke(this.Canvas.AddPrefs<ValType, GuiType>(prefs));

            this.prefsCreators.Enqueue(Creator);
        }

        public void RemovePrefs(Prefs.PrefsBase prefs)
        {
            if(this.Canvas != null)
            {
                this.Canvas.RemovePrefs(prefs);
            }
        }

        public void RemoveCategory(GuiHierarchy hierarchy)
        {
            if(this.Canvas != null)
            {
                this.Canvas.RemoveCategory(hierarchy);
            }
        }

        public void SetCanvasSize(float width, float height)
        {
            if(this.Canvas == null)
            {
                return;
            }

            var delta = this.Canvas.Panel.sizeDelta;
            this.Canvas.Panel.sizeDelta = new Vector2(
                width <= 0f ? delta.x : width,
                height <= 0f ? delta.y : height
            );
        }

        private void Create()
        {
            if(this.Canvas != null)
            {
                while(this.prefsCreators.Count > 0)
                {
                    this.prefsCreators.Dequeue()?.Invoke();
                }
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
