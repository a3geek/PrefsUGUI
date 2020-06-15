using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Preferences;
    using XmlStorage;
    using XmlStorageConsts = XmlStorage.Systems.XmlStorageConsts;

    /// <summary>
    /// Central part of PrefsUGUI.
    /// </summary>
    public static partial class Prefs
    {
        public const char HierarchySeparator = '/';

        public static string AggregationName { get; private set; } = "";
        public static string FileName { get; private set; } = "";

        private static PrefsGuis PrefsGuis = null;
        private static ConcurrentBag<Action> StorageValueSetters = new ConcurrentBag<Action>();
        private static ConcurrentQueue<Action> PrefsActionsCache = new ConcurrentQueue<Action>();


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

            ConcurrentQueue<Action> GetPrefsActionsCache() => PrefsActionsCache;
            PrefsGuis.SetPrefsActionsCacheGetter(GetPrefsActionsCache);
        }

        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;


            foreach (var setter in StorageValueSetters)
            {
                setter?.Invoke();
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        public static void ShowGUI()
        {
            if (PrefsGuis != null)
            {
                PrefsGuis.ShowGUI();
            }
        }

        public static bool IsShowing()
            => PrefsGuis != null && PrefsGuis.IsShowing;

        public static void SetCanvasSize(float width, float height)
        {
            if (PrefsGuis != null)
            {
                PrefsGuis.SetCanvasSize(width, height);
            }
        }

        public static void RemoveGuiHierarchy(string fullHierarchyName)
        {
            void RemoveGuiHierarchy() => PrefsGuis.RemoveCategory(fullHierarchyName);
            PrefsActionsCache.Enqueue(RemoveGuiHierarchy);
        }

        private static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);
            PrefsActionsCache.Enqueue(AddPrefs);
        }

        private static void RemovePrefs(string prefsSaveKey)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(prefsSaveKey);
            PrefsActionsCache.Enqueue(RemovePrefs);
        }
    }
}
