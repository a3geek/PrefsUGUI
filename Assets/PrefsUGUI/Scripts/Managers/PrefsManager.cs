using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace PrefsUGUI.Managers
{
    using GuiHierarchies.Abstracts;
    using Guis;
    using Guis.Factories;
    using Guis.Factories.Classes;
    using Guis.Preferences;
    using Preferences.Abstracts;

    public static class PrefsManager
    {
        public static event Action<PrefsBase> OnAnyPrefsValueChanged = delegate { };

        public static string AggregationName { get; private set; } = "";
        public static string FileName { get; private set; } = "";
        public static PrefsGuis PrefsGuis { get; private set; } = null;
        public static ConcurrentBag<Action> StorageValueSetters { get; } = new ConcurrentBag<Action>();

        private static ConcurrentDictionary<string, Action> AddPrefsCache = new ConcurrentDictionary<string, Action>();
        private static ConcurrentQueue<string> AddPrefsCacheOrders = new ConcurrentQueue<string>();
        private static ConcurrentDictionary<Guid, Action> RemovePrefsCache = new ConcurrentDictionary<Guid, Action>();
        private static ConcurrentDictionary<string, Action> AddGuiHierarchyCache = new ConcurrentDictionary<string, Action>();
        private static ConcurrentQueue<string> AddGuiHierarchyCacheOrders = new ConcurrentQueue<string>();
        private static ConcurrentDictionary<Guid, Action> RemoveGuiHierarchyCache = new ConcurrentDictionary<Guid, Action>();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            PrefsGuis = UnityEngine.Object.FindObjectOfType<PrefsGuis>();
            if (PrefsGuis == null)
            {
                var prefab = (GameObject)Resources.Load(PrefsGuis.PrefsGuisPrefabName, typeof(GameObject));
                if (prefab == null)
                {
                    return;
                }

                PrefsGuis = UnityEngine.Object.Instantiate(prefab).GetComponent<PrefsGuis>();
            }

            var parameters = UnityEngine.Object.FindObjectOfType<PrefsParameters>();
            AggregationName = parameters == null ? PrefsParameters.DefaultNameGetter() : parameters.AggregationName;
            FileName = parameters == null ? PrefsParameters.DefaultNameGetter() : parameters.FileName;

            void ExecuteCachingActions()
            {
                ExecuteAndClear(AddGuiHierarchyCache, AddGuiHierarchyCacheOrders);
                ExecuteAndClear(AddPrefsCache, AddPrefsCacheOrders);
                ExecuteAndClear(RemovePrefsCache);
                ExecuteAndClear(RemoveGuiHierarchyCache);
            }
            PrefsGuis.SetCachingActionsExecutor(ExecuteCachingActions);
        }

        public static void AddGuiHierarchy<GuiType>(AbstractGuiHierarchy hierarchy, Action<PrefsCanvas, Category, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            void AddGuiHierarchy() => PrefsGuis.AddCategory(hierarchy, onCreated);
            AddGuiHierarchyCache[hierarchy.FullHierarchy] = AddGuiHierarchy;
            AddGuiHierarchyCacheOrders.Enqueue(hierarchy.FullHierarchy);
        }

        public static void RemoveGuiHierarchy(Guid hierarchyId)
        {
            void RemoveGuiHierarchy() => PrefsGuis.RemoveCategory(ref hierarchyId);
            RemoveGuiHierarchyCache[hierarchyId] = RemoveGuiHierarchy;
        }

        public static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);
            AddPrefsCache[prefs.SaveKey] = AddPrefs;
            AddPrefsCacheOrders.Enqueue(prefs.SaveKey);

            void OnPrefsValueChanged() => OnAnyPrefsValueChanged(prefs);
            prefs.OnValueChanged += OnPrefsValueChanged;
        }

        public static void RemovePrefs(Guid prefsId)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(ref prefsId);
            RemovePrefsCache[prefsId] = RemovePrefs;
        }

        private static void ExecuteAndClear<T>(ConcurrentDictionary<T, Action> dictionary)
        {
            foreach (var pair in dictionary)
            {
                pair.Value?.Invoke();
            }
            dictionary.Clear();
        }

        private static void ExecuteAndClear<T>(ConcurrentDictionary<T, Action> dictionary, ConcurrentQueue<T> orders)
        {
            while (orders.TryDequeue(out var index) == true)
            {
                if (dictionary.TryRemove(index, out var action) == true)
                {
                    action?.Invoke();
                }
            }
            dictionary.Clear();
        }
    }
}
