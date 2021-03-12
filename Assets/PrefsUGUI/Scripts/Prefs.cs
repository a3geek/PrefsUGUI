using System;

namespace PrefsUGUI
{
    using Managers;
    using Preferences.Abstracts;
    using UnityEngine;
    using XmlStorage;
    using XmlStorageConsts = XmlStorage.Systems.XmlStorageConsts;

    public static class Prefs
    {
        public const char HierarchySeparator = '/';

        public static event Action<PrefsBase> OnPrefsEditedinGui = delegate { };

        public static string AggregationName => PrefsManager.PrefsParameters.AggregationName;
        public static string FileName => PrefsManager.PrefsParameters.FileName;

        private static bool WillQuit = false;


        static Prefs()
        {
            Application.quitting += () => WillQuit = true;
            PrefsManager.OnAnyPrefsEditedInGui += prefs =>
            {
                if (WillQuit == false)
                {
                    OnPrefsEditedinGui(prefs);
                }
            };
        }

        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            PrefsManager.ExecuteStorageSetters();

            Storage.ChangeAggregation(current);
            Storage.Save();
        }

        public static void ShowGUI()
        {
            if (PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.ShowGUI();
            }
        }

        public static void NotifyWillSceneLoad()
            => PrefsManager.NotifyWillSceneLoad();

        public static bool IsShowing()
            => PrefsManager.PrefsGuis != null && PrefsManager.PrefsGuis.IsShowing;

        public static void SetCanvasSize(float width, float height)
        {
            if (PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.SetCanvasSize(width, height);
            }
        }
    }
}
