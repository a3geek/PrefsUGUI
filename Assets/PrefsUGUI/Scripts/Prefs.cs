using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Prefs;
    using XmlStorage;

    using Creator = Dictionary<string, Action<Guis.Factories.PrefsCanvas>>;
    using XmlStorageConsts = XmlStorage.Components.Consts;

    /// <summary>
    /// Central part of PrefsUGUI.
    /// </summary>
    public static partial class Prefs
    {
        /// <summary>Char for separating for hierarchy's string.</summary>
        public const char HierarchySeparator = '/';

        /// <summary>Aggregation name for saving.</summary>
        /// <remarks>For defailts, refer to XmlStorage</remarks>
        public static string AggregationName => Application.productName;
        /// <summary>File name for saving.</summary>
        public static string FileName => Application.productName;

        /// <summary>Reference to <see cref="PrefsGuis"/> component.</summary>
        private static PrefsGuis PrefsGuis = null;
        /// <summary>Actions for creating each GUI.</summary>
        private static Creator Creators = new Creator();
        /// <summary>Instances of PrefsUGUI's members.</summary>
        private static List<PrefsBase> PrefsInstances = new List<PrefsBase>();


        /// <summary>
        /// Constructor
        /// </summary>
        static Prefs()
        {
            PrefsGuis = null;
            Creators = new Creator();
            PrefsInstances = new List<PrefsBase>();
        }

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

            PrefsGuis.Initialize(() => Creators);
        }

        /// <summary>
        /// Save all data.
        /// </summary>
        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            for(var i = 0; i < PrefsInstances.Count; i++)
            {
                var p = PrefsInstances[i];
                Storage.Set(p.ValueType, p.SaveKey, p.ValueAsObject, AggregationName);
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        /// <summary>
        /// Toggle on for show the GUI.
        /// </summary>
        public static void ShowGUI() => PrefsGuis?.ShowGUI();

        /// <summary>
        /// Get whether the GUI is showing.
        /// </summary>
        /// <returns>It's showing, If returned to true.</returns>
        public static bool IsShowing() => PrefsGuis == null ? false : PrefsGuis.IsShowing;

        /// <summary>
        /// Change cnavas size.
        /// </summary>
        /// <param name="width">Width of new canvas size.</param>
        /// <param name="height">Height of new canvas size.</param>
        public static void SetCanvasSize(float width, float height) => PrefsGuis.SetCanvasSize(width, height);

        private static void AddPrefs<ValType, PrefabType>(PrefsValueBase<ValType> prefs, Action<PrefabType> onCreated) where PrefabType : InputGuiValueBase<ValType>
            => Creators[prefs.SaveKey] = canvas => onCreated(canvas.AddPrefs<ValType, PrefabType>(prefs));

        /// <summary>
        /// Register to create each GUI.
        /// </summary>
        /// <typeparam name="PrefabType"></typeparam>
        /// <param name="prefs">Prefs mamber for register.</param>
        /// <param name="onCreated">Callback action when created GUI.</param>
        private static void AddPrefs<PrefabType>(PrefsBase prefs, Action<PrefabType> onCreated) where PrefabType : PrefsGuiBase
            => Creators[prefs.SaveKey] = canvas => onCreated(canvas.AddPrefs<PrefabType>(prefs));

        /// <summary>
        /// Remove registered information.
        /// </summary>
        /// <param name="prefs">Prefs member for remove.</param>
        private static void RemovePrefs(PrefsBase prefs) => PrefsGuis?.RemovePrefs(prefs);
    }
}
