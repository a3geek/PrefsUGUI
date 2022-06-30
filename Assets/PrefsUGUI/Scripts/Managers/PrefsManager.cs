using System;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers
{
    using Classes;
    using Guis;
    using Preferences.Abstracts;
    using UnityObject = UnityEngine.Object;

    public static partial class PrefsManager
    {
        public static PrefsParameters PrefsParameters
        {
            get => PrefsParametersInternal;
            set
            {
                Inited = false;
                InitializeInternal(value);
            }
        }
        public static bool VisibleControllsGui
        {
            set
            {
                if(PrefsGuis != null && PrefsGuis.Canvas != null)
                {
                    PrefsGuis.Canvas.VisibleControllsGui = value;
                }
            }
        }
        public static bool Inited { get; private set; } = false;
        public static PrefsGuis PrefsGuis { get; private set; } = null;

        private static ConcurrentDictionary<string, IPrefsStorageSetter> StorageSetters = new ConcurrentDictionary<string, IPrefsStorageSetter>();
        private static PrefsParameters PrefsParametersInternal = PrefsParameters.Empty;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // 特に意味はないがPrefsのstaticコンストラクタを走らせるために記述.
            Prefs.VisibleControllsGui = true;

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

            var entity = UnityObject.FindObjectOfType<PrefsParametersEntity>();
            InitializeInternal(entity != null ? entity : PrefsParameters.Empty);

            Executor.ExecuteCachedAction();
        }

        public static void NotifyWillSceneLoad()
            => Inited = false;

        public static void AddStorageSetter(string saveKey, IPrefsStorageSetter setter)
            => StorageSetters[saveKey] = setter;

        public static void RemoveStorageSetter(string saveKey)
            => StorageSetters.TryRemove(saveKey, out _);

        public static void ExecuteStorageSetters()
        {
            foreach(var setter in StorageSetters)
            {
                setter.Value.SetStorageValue();
            }
        }

        private static void InitializeInternal(PrefsParameters parameters)
        {
            if(Inited == true)
            {
                return;
            }

            if(parameters != null && parameters.IsEmpty() == false && PrefsParameters.IsEqual(parameters) == false)
            {
                PrefsParametersInternal = parameters;
            }
            if(PrefsParameters.IsEmpty() == true)
            {
                PrefsParametersInternal = PrefsParameters.Default;
            }

            Inited = true;
            if(PrefsGuis == null)
            {
                return;
            }
            
            foreach(var setter in StorageSetters)
            {
                setter.Value.OnInitializedPrefs();
            }
        }
    }
}
