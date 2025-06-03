using System;

namespace PrefsUGUI
{
    using Hierarchies.Abstracts;
    using Managers;
    using Preferences.Abstracts;
    using UnityEngine;
    using XmlStorage;

    public static class Prefs
    {
        public const char HierarchySeparator = '/';

        public static event Action OnSaved = delegate { };
        public static event Action<IPrefsCommon> OnPrefsAdded = delegate { };
        public static event Action<AbstractHierarchy> OnHierarchyAdded = delegate { };
        public static event Action<PrefsBase> OnPrefsEditedinGui = delegate { };

        public static PrefsParameters PrefsParameters
        {
            get => PrefsManager.PrefsParameters;
            set => PrefsManager.PrefsParameters = value;
        }
        public static bool VisibleControllsGui
        {
            set => PrefsManager.VisibleControllsGui = value;
        }
        public static string AggregationName => PrefsParameters.AggregationName;
        public static string FileName => PrefsParameters.FileName;

        private static PrefsEditedEvents PrefsEditedEventer = new PrefsEditedEvents();


        static Prefs()
        {
            PrefsManager.PrefsEditedEventer = PrefsEditedEventer;
        }

        public static void Save()
        {
            var current = Storage.CurrentDataGroupName;

            Storage.CurrentDataGroupName = AggregationName;

            PrefsManager.ExecuteStorageSetters();

            Storage.CurrentDataGroupName = current;
            Storage.Save();

            OnSaved();
        }

        public static void ShowGUI()
        {
            if(PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.ShowGUI();
            }
        }

        public static void HideGUI()
        {
            if(PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.HideGUI();
            }
        }

        public static void NotifyWillSceneLoad()
            => PrefsManager.NotifyWillSceneLoad();

        public static bool IsShowing()
            => PrefsManager.PrefsGuis != null && PrefsManager.PrefsGuis.IsShowing;

        public static void SetCanvasSize(float width, float height)
        {
            if(PrefsManager.PrefsGuis != null)
            {
                PrefsManager.PrefsGuis.SetCanvasSize(width, height);
            }
        }


        private class PrefsEditedEvents : PrefsManager.IPrefsEditedEvents
        {
            private bool willQuit = false;


            public PrefsEditedEvents()
            {
                Application.quitting += this.OnQuitting;
            }

            public void OnAnyPrefsEditedInGui(PrefsBase prefs)
            {
                if(this.willQuit == false)
                {
                    OnPrefsEditedinGui(prefs);
                }
            }

            public void OnAnyPrefsAdded(IPrefsCommon prefs)
                => OnPrefsAdded(prefs);

            public void OnAnyHierarchyAdded(AbstractHierarchy hierarchy)
                => OnHierarchyAdded(hierarchy);

            private void OnQuitting()
                => this.willQuit = true;
        }
    }
}
