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

        private Action cachingActionsExecutor = null;
        private MultikeyDictionary<string, Guid, PrefsGuiBase> guis = new MultikeyDictionary<string, Guid, PrefsGuiBase>();
        private MultikeyDictionary<string, Guid, Category> categories = new MultikeyDictionary<string, Guid, Category>();


        private void Awake()
        {
            this.Canvas = this.CreateCanvas();
            if (EventSystem.current == null)
            {
                this.CreateEventSystem();
            }

            this.cachingActionsExecutor?.Invoke();
        }

        private void Start()
        {
            this.cachingActionsExecutor?.Invoke();
        }

        private void Update()
        {
            this.cachingActionsExecutor?.Invoke();
        }

        public void SetCachingActionsExecutor(Action executor)
            => this.cachingActionsExecutor = this.cachingActionsExecutor ?? executor;

        public void ShowGUI()
        {
            if (this.Canvas != null)
            {
                this.Canvas.gameObject.SetActive(true);
            }
        }

        public void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            if(this.Canvas == null)
            {
                return;
            }

            onCreated?.Invoke(
                (GuiType)this.guis.GetOrAdd(prefs.SaveKey, prefs.PrefsId, () => this.Canvas.AddPrefs<ValType, GuiType>(prefs))
            );
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

        public void AddCategory<GuiType>(AbstractGuiHierarchy hierarchy, Action<PrefsCanvas, Category, GuiType> onCreated)
             where GuiType : PrefsGuiButton
        {
            if(this.Canvas == null)
            {
                return;
            }

            var category = this.categories.GetOrAdd(
                hierarchy.FullHierarchy, hierarchy.HierarchyId, () => this.Canvas.AddCategory(hierarchy)
            );
            onCreated?.Invoke(this.Canvas, category, (GuiType)category.GuiButton);
        }

        public void RemoveCategory(ref Guid categoryId)
        {
            if(this.Canvas == null)
            {
                return;
            }

            this.Canvas.RemoveCategory(ref categoryId);
            this.categories.Remove(categoryId);
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
