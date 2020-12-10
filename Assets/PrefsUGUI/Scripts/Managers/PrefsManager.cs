using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Managers
{
    using Commons;
    using GuiHierarchies.Abstracts;
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Preferences.Abstracts;

    public static class PrefsManager
    {
        public static event Action<PrefsBase> OnAnyPrefsValueChanged = delegate { };

        public static bool Inited { get; private set; } = false;
        public static string AggregationName { get; private set; } = "";
        public static string FileName { get; private set; } = "";
        public static PrefsGuis PrefsGuis { get; private set; } = null;
        public static OrderableConcurrentCache<string, Action> StorageValueSetters { get; } = new OrderableConcurrentCache<string, Action>();

        private static ConcurrentStack<PrefsBase> FastPrefsCache = new ConcurrentStack<PrefsBase>();
        private static OrderableConcurrentCache<string, Action> AddPrefsCache = new OrderableConcurrentCache<string, Action>();
        private static OrderableConcurrentCache<Guid, Action> RemovePrefsCache = new OrderableConcurrentCache<Guid, Action>();
        private static OrderableConcurrentCache<string, Action> AddGuiHierarchiesCache = new OrderableConcurrentCache<string, Action>();
        private static OrderableConcurrentCache<Guid, Action> RemoveGuiHierarchiesCache = new OrderableConcurrentCache<Guid, Action>();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            PrefsGuis = UnityEngine.Object.FindObjectOfType<PrefsGuis>();
            if(PrefsGuis == null)
            {
                var prefab = (GameObject)Resources.Load(PrefsGuis.PrefsGuisPrefabName, typeof(GameObject));
                if(prefab == null)
                {
                    return;
                }

                PrefsGuis = UnityEngine.Object.Instantiate(prefab).GetComponent<PrefsGuis>();
            }

            var parameters = UnityEngine.Object.FindObjectOfType<PrefsParameters>();
            AggregationName = parameters == null ? PrefsParameters.DefaultNameGetter() : parameters.AggregationName;
            FileName = parameters == null ? PrefsParameters.DefaultNameGetter() : parameters.FileName;

            while(FastPrefsCache.TryPop(out var prefs) == true)
            {
                prefs.Reload(true);
            }
            Inited = true;

            void ExecuteCachingActions()
            {
                Action<Action> action = act => act?.Invoke();

                AddGuiHierarchiesCache.TakeEach(action);
                AddPrefsCache.TakeEach(action);
                RemovePrefsCache.TakeEach(action);
                RemoveGuiHierarchiesCache.TakeEach(action);
            }
            PrefsGuis.SetCachingActionsExecutor(ExecuteCachingActions);
        }

        public static void AddGuiHierarchy<GuiType>(AbstractGuiHierarchy hierarchy, Action<PrefsCanvas, Category, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            void AddGuiHierarchy() => PrefsGuis.AddCategory(hierarchy, onCreated);
            AddGuiHierarchiesCache.Add(hierarchy.FullHierarchy, AddGuiHierarchy);
        }

        public static void RemoveGuiHierarchy(Guid hierarchyId)
        {
            void RemoveGuiHierarchy() => PrefsGuis.RemoveCategory(ref hierarchyId);
            RemoveGuiHierarchiesCache.Add(hierarchyId, RemoveGuiHierarchy);
        }

        public static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);
            AddPrefsCache.Add(prefs.SaveKey, AddPrefs);

            if(Inited == false)
            {
                FastPrefsCache.Push(prefs);
            }

            void OnPrefsValueChanged() => OnAnyPrefsValueChanged(prefs);
            prefs.OnValueChanged += OnPrefsValueChanged;
        }

        public static void RemovePrefs(Guid prefsId)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(ref prefsId);
            RemovePrefsCache.Add(prefsId, RemovePrefs);
        }
    }
}
