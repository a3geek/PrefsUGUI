using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis;
    using XmlStorage;

    using Creator = Dictionary<string, Action<Guis.Factories.PrefsCanvas>>;
    using XmlStorageConsts = XmlStorage.Components.Consts;

    public static partial class Prefs
    {
        public const char HierarchySeparator = '/';

        public static string AggregationName => Application.productName;
        public static string FileName => Application.productName;

        private static PrefsGuis PrefsGuis = null;
        private static Creator Creators = new Creator();
        private static Dictionary<string, PrefsBase> Data = new Dictionary<string, PrefsBase>();


        static Prefs()
        {
            PrefsGuis = null;
            Creators = new Creator();
            Data = new Dictionary<string, PrefsBase>();
        }

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

        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            foreach(var pair in Data)
            {
                Storage.Set(pair.Value.ValueType, pair.Value.SaveKey, pair.Value.ValueAsObject, AggregationName);
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        public static void ShowGUI() => PrefsGuis?.ShowGUI();

        public static bool IsShowing() => PrefsGuis == null ? false : PrefsGuis.IsShowing;

        public static void SetSize(float width, float height) => PrefsGuis.SetSize(width, height);

        private static void AddPrefs<PrefabType>(PrefsBase prefs, Action<PrefabType> onCreated) where PrefabType : InputGuiBase
            => Creators[prefs.SaveKey] = canvas => onCreated(canvas.AddPrefs<PrefabType>(prefs));

        private static void RemovePrefs(PrefsBase prefs) => PrefsGuis?.RemovePrefs(prefs);
    }
}
