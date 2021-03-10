using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Guis
{
    using Commons;
    using Factories;
    using Factories.Classes;
    using GuiHierarchies.Abstracts;
    using Preferences;
    using PrefsUGUI.Preferences.Abstracts;

    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(ExecutionOrder)]
    public class PrefsGuis : MonoBehaviour
    {
        public interface ICacheExecutor
        {
            void ExecuteCacheAction();
        }

        public const int ExecutionOrder = -30000;
        public const string PrefsGuisPrefabName = "PrefsGuis";

        public bool IsShowing => this.Canvas.gameObject.activeSelf;
        public PrefsCanvas Canvas { get; private set; } = null;

        [SerializeField]
        private PrefsCanvas prefsCanvasPrefab = null;
        [SerializeField]
        private EventSystem eventSystemPrefab = null;

        private ICacheExecutor executor = null;
        private MultikeyDictionary<string, Guid, PrefsGuiBase> guis = new MultikeyDictionary<string, Guid, PrefsGuiBase>();


        private void Awake()
        {
            this.Canvas = this.CreateCanvas();
            if(EventSystem.current == null)
            {
                this.CreateEventSystem();
            }

            this.executor?.ExecuteCacheAction();
        }

        private void Start()
        {
            this.executor?.ExecuteCacheAction();
        }

        private void Update()
        {
            this.executor?.ExecuteCacheAction();
        }

        public void SetCacheExecutor(ICacheExecutor executor)
            => this.executor = this.executor ?? executor;

        public void ShowGUI()
        {
            if(this.Canvas == null)
            {
                return;
            }

            this.Canvas.gameObject.SetActive(true);
        }

        public void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            if(this.Canvas == null)
            {
                return;
            }

            var gui = (GuiType)this.guis.GetOrAdd(
                prefs.SaveKey, prefs.PrefsId, () => this.Canvas.AddPrefs<ValType, GuiType>(prefs)
            );
            onCreated?.Invoke(gui);
        }

        public void RemovePrefs(ref Guid prefsId)
        {
            if(this.Canvas == null)
            {
                return;
            }

            this.guis.Remove(prefsId);
            this.Canvas.RemovePrefs(ref prefsId);
        }

        public void AddHierarchy<GuiType>(GuiHierarchy guiHierarchy, Action<PrefsCanvas, AbstractHierarchy, GuiType> onCreated)
             where GuiType : PrefsGuiButton
        {
            if(this.Canvas == null)
            {
                return;
            }

            var hierarchy = this.Canvas.GetOrAddHierarchy(guiHierarchy);
            onCreated?.Invoke(this.Canvas, hierarchy, (GuiType)hierarchy.GuiButton);
        }

        public void AddHierarchy<GuiType>(LinkedGuiHierarchy guiHierarchy, Action<PrefsCanvas, AbstractHierarchy, GuiType> onCreated)
             where GuiType : PrefsGuiButton
        {
            if(this.Canvas == null)
            {
                return;
            }

            var hierarchy = this.Canvas.GetOrAddHierarchy(guiHierarchy);
            onCreated?.Invoke(this.Canvas, hierarchy, (GuiType)hierarchy.GuiButton);
        }

        public void RemoveHierarchy(ref Guid hierarchyId)
        {
            if(this.Canvas == null)
            {
                return;
            }

            this.Canvas.RemoveHierarchy(ref hierarchyId);
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
