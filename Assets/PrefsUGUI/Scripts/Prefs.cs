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

        public static event Action<PrefsBase> OnPrefsValueChanged = delegate { };

        public static string AggregationName => PrefsManager.AggregationName;
        public static string FileName => PrefsManager.FileName;

        private static bool WillQuit = false;


        static Prefs()
        {
            Application.quitting += () => WillQuit = true;
            PrefsManager.OnAnyPrefsEditedInGui += prefs =>
            {
                if (WillQuit == false)
                {
                    OnPrefsValueChanged(prefs);
                }
            };
        }

        public static void Save()
        {
            var current = Storage.CurrentAggregationName;

            Storage.ChangeAggregation(AggregationName);
            Storage.CurrentAggregation.FileName = FileName + XmlStorageConsts.Extension;

            PrefsManager.StorageValueSetters.TakeEach(action => action?.Invoke());

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
