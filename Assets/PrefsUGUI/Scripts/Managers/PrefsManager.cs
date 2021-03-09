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
    using AddCache = Commons.OrderableConcurrentCache<string, Action>;
    using RemoveCache = Commons.OrderableConcurrentCache<Guid, Action>;
    using UnityObject = UnityEngine.Object;

    public static class PrefsManager
    {
        private class CacheExecutor : PrefsGuis.ICacheExecutor
        {
            public AddCache AddPrefsCache = new AddCache();
            public RemoveCache RemovePrefsCache = new RemoveCache();
            public AddCache AddGuiHierarchiesCache = new AddCache();
            public RemoveCache RemoveGuiHierarchiesCache = new RemoveCache();


            public void ExecuteCacheAction()
            {
                AddGuiHierarchiesCache.TakeEach(this.Execute);
                AddPrefsCache.TakeEach(this.Execute);
                RemovePrefsCache.TakeEach(this.Execute);
                RemoveGuiHierarchiesCache.TakeEach(this.Execute);
            }

            private void Execute(Action act)
                => act?.Invoke();
        }

        public static event Action<PrefsBase> OnAnyPrefsEditedInGui = delegate { };

        public static bool Inited { get; private set; } = false;
        //public static string AggregationName { get; private set; } = "";
        //public static string FileName { get; private set; } = "";
        public static IPrefsParameters PrefsParameters
        {
            get => PrefsParametersInternal;
            set
            {
                InitializeInternal();
                PrefsParametersInternal = value;
            }
        }
        public static PrefsGuis PrefsGuis { get; private set; } = null;
        public static OrderableConcurrentCache<string, Action> StorageValueSetters { get; } = new OrderableConcurrentCache<string, Action>();

        private static CacheExecutor Executor = new CacheExecutor();
        private static ConcurrentStack<PrefsBase> FastPrefsCache = new ConcurrentStack<PrefsBase>();
        private static IPrefsParameters PrefsParametersInternal = null;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            PrefsGuis = UnityObject.FindObjectOfType<PrefsGuis>();
            if(PrefsGuis == null)
            {
                var prefab = (GameObject)Resources.Load(PrefsGuis.PrefsGuisPrefabName, typeof(GameObject));
                if(prefab == null)
                {
                    return;
                }

                PrefsGuis = UnityObject.Instantiate(prefab).GetComponent<PrefsGuis>();
            }
            UnityObject.DontDestroyOnLoad(PrefsGuis);
        }

        public static void NotifyWillSceneLoad()
        {
            Inited = false;
        }

        public static void AddGuiHierarchy<GuiType>(AbstractGuiHierarchy hierarchy, Action<PrefsCanvas, Category, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            void AddGuiHierarchy() => PrefsGuis.AddCategory(hierarchy, onCreated);
            Executor.AddGuiHierarchiesCache.Add(hierarchy.FullHierarchy, AddGuiHierarchy);
        }

        public static void AddLinkedGuiHierarchy<GuiType>(LinkedGuiHierarchy hierarchy, Action<PrefsCanvas, Category, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            //void AddGuiHierarchy() => PrefsGuis.AddCategory(hierarchy, onCreated);
            //AddGuiHierarchiesCache.Add(hierarchy.FullHierarchy, AddGuiHierarchy);
        }

        public static void RemoveGuiHierarchy(Guid hierarchyId)
        {
            void RemoveGuiHierarchy() => PrefsGuis.RemoveCategory(ref hierarchyId);
            Executor.RemoveGuiHierarchiesCache.Add(hierarchyId, RemoveGuiHierarchy);
        }

        public static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);
            Executor.AddPrefsCache.Add(prefs.SaveKey, AddPrefs);

            if(Inited == false)
            {
                FastPrefsCache.Push(prefs);
            }

            void OnPrefsEdited() => OnAnyPrefsEditedInGui(prefs);
            prefs.OnEditedInGui += OnPrefsEdited;
        }

        public static void RemovePrefs(Guid prefsId)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(ref prefsId);
            Executor.RemovePrefsCache.Add(prefsId, RemovePrefs);
        }

        private static void InitializeInternal()
        {
            var parameters = UnityObject.FindObjectOfType<PrefsParametersEntity>();
            AggregationName = parameters == null ? PrefsParametersEntity.DefaultNameGetter() : parameters.AggregationName;
            FileName = parameters == null ? PrefsParametersEntity.DefaultNameGetter() : parameters.FileName;

            while(FastPrefsCache.TryPop(out var prefs) == true)
            {
                prefs.Reload(true);
            }
            Inited = true;

            PrefsGuis.SetCacheExecutor(Executor);
        }
    }
}
