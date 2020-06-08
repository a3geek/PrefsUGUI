using System;
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
        /// <summary>Char for separating for hierarchy's string.</summary>
        public const char HierarchySeparator = '/';

        /// <summary>Aggregation name for saving.</summary>
        /// <remarks>For defailts, refer to XmlStorage</remarks>
        public static string AggregationName { get; private set; } = "";
        /// <summary>File name for saving.</summary>
        public static string FileName { get; private set; } = "";

        /// <summary>Reference to <see cref="PrefsGuis"/> component.</summary>
        private static PrefsGuis PrefsGuis = null;
        private static Queue<Action> PrefsActionsCache = new Queue<Action>();
        private static List<Action> ValueSetters = new List<Action>();


        /// <summary>
        /// Initialize at before scene load.
        /// If I can't find a <see cref="PrefsGuis"/> component at GameScene, I will load its prefab from Resource, and instantiate it.
        /// </summary>
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

            while(PrefsActionsCache.Count > 0)
            {
                PrefsActionsCache.Dequeue()?.Invoke();
            }
        }

        /// <summary>
        /// Save all data.
        /// </summary>
        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            for(var i = 0; i < ValueSetters.Count; i++)
            {
                ValueSetters[i]?.Invoke();
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        /// <summary>
        /// Toggle on for show the GUI.
        /// </summary>
        public static void ShowGUI()
        {
            if(PrefsGuis != null)
            {
                PrefsGuis.ShowGUI();
            }
        }

        /// <summary>
        /// Get whether the GUI is showing.
        /// </summary>
        /// <returns>It's showing, If returned to true.</returns>
        public static bool IsShowing()
            => PrefsGuis != null && PrefsGuis.IsShowing;

        /// <summary>
        /// Change cnavas size.
        /// </summary>
        /// <param name="width">Width of new canvas size.</param>
        /// <param name="height">Height of new canvas size.</param>
        public static void SetCanvasSize(float width, float height)
        {
            if(PrefsGuis != null)
            {
                PrefsGuis.SetCanvasSize(width, height);
            }
        }

        public static void RemoveGuiHierarchy(GuiHierarchy hierarchy)
        {
            if(PrefsGuis != null)
            {
                PrefsGuis.RemoveCategory(hierarchy);
            }
        }

        private static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);

            if(PrefsGuis == null)
            {
                PrefsActionsCache.Enqueue(AddPrefs);
            }
            else
            {
                AddPrefs();
            }
        }

        /// <summary>
        /// Remove registered information.
        /// </summary>
        /// <param name="prefs">Prefs member for remove.</param>
        private static void RemovePrefs(PrefsBase prefs)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(prefs);

            if(PrefsGuis == null)
            {
                PrefsActionsCache.Enqueue(RemovePrefs);
            }
            else
            {
                RemovePrefs();
            }
        }
    }
}
