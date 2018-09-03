using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using XmlStorage;
    using Guis;

    using Creator = Dictionary<string, Action<Guis.Factories.PrefsCanvas>>;

    public static partial class Prefs
    {
        public const char HierarchySeparator = '/';
        
        public static string AggregationName
        {
            get { return typeof(Prefs).Namespace + "-" + Application.productName; }
        }
        public static string FileName
        {
            get { return typeof(Prefs).Namespace + "_" + Application.productName; }
        }

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
            PrefsGuis = (new GameObject("Prefs GUIs")).AddComponent<PrefsGuis>();
            PrefsGuis.SetCreatorGetter(() => Creators);
        }
        
        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.FileName = FileName + Storage.Extension;

            foreach(var pair in Data)
            {
                Storage.Set(pair.Value.ValueType, pair.Value.SaveKey, pair.Value.ValueAsObject, AggregationName);
            }

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        public static void ShowGUI()
        {
            PrefsGuis.ShowGUI();
        }

        private static void AddPrefs<PrefabType>(PrefsBase prefs, Action<PrefabType> onCreated) where PrefabType : InputGuiBase
        {
            Creators[prefs.SaveKey] = canvas => onCreated(canvas.AddPrefs<PrefabType>(prefs));
        }

        private static void RemovePrefs(PrefsBase prefs)
        {
            if(PrefsGuis == null)
            {
                return;
            }

            PrefsGuis.RemovePrefs(prefs);
        }
    }
}
