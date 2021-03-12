﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers
{
    using Classes;
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

    public static partial class PrefsManager
    {
        public static IPrefsParameters PrefsParameters
        {
            get => PrefsParametersInternal;
            set
            {
                Inited = false;
                InitializeInternal(value);
            }
        }
        public static bool Inited { get; private set; } = false;
        public static PrefsGuis PrefsGuis { get; private set; } = null;

        private static ConcurrentDictionary<string, IPrefsStorageSetter> StorageSetters = new ConcurrentDictionary<string, IPrefsStorageSetter>();
        private static ConcurrentStack<PrefsBase> FastPrefsCache = new ConcurrentStack<PrefsBase>();
        private static IPrefsParameters PrefsParametersInternal = PrefsUGUI.PrefsParameters.Empty;


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
            PrefsGuis.SetCacheExecutor(Executor);

            InitializeInternal(UnityObject.FindObjectOfType<PrefsParametersEntity>());
            SceneManager.sceneLoaded += SceneLoaded;
        }

        public static void NotifyWillSceneLoad()
            => Inited = false;

        public static void AddStorageSetter(string saveKey, IPrefsStorageSetter setter)
            => StorageSetters[saveKey] = setter;

        public static void ExecuteStorageSetters()
        {
            foreach(var setter in StorageSetters)
            {
                setter.Value.SetStorageValue();
            }
        }

        private static void InitializeInternal(IPrefsParameters parameters)
        {
            if(Inited == true)
            {
                return;
            }

            if(parameters != null && PrefsParameters.IsEqual(parameters) == false)
            {
                PrefsParametersInternal = parameters;
            }
            if(PrefsParameters.IsEmpty() == true)
            {
                PrefsParametersInternal = PrefsUGUI.PrefsParameters.Default;
            }

            while(FastPrefsCache.TryPop(out var prefs) == true)
            {
                prefs.Reload(true);
            }

            Inited = true;
        }

        private static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(mode == LoadSceneMode.Single)
            {
                InitializeInternal(UnityObject.FindObjectOfType<PrefsParametersEntity>());
            }
        }
    }
}
