using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers
{
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
        private class CacheExecutor : PrefsGuis.ICacheExecutor, AddCache.ITaker, RemoveCache.ITaker
        {
            public AddCache AddPrefsCache = new AddCache();
            public RemoveCache RemovePrefsCache = new RemoveCache();
            public AddCache AddGuiHierarchiesCache = new AddCache();
            public RemoveCache RemoveGuiHierarchiesCache = new RemoveCache();


            public void ExecuteCacheAction()
            {
                AddGuiHierarchiesCache.TakeEach(this);
                AddPrefsCache.TakeEach(this);
                RemovePrefsCache.TakeEach(this);
                RemoveGuiHierarchiesCache.TakeEach(this);
            }

            public void Take(Action action)
                => action?.Invoke();
        }


        public static event Action<PrefsBase> OnAnyPrefsEditedInGui = delegate { };

        private static CacheExecutor Executor = new CacheExecutor();


        public static void AddGuiHierarchy<GuiType>(GuiHierarchy hierarchy, Action<PrefsCanvas, AbstractHierarchy, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            void AddGuiHierarchy() => PrefsGuis.AddHierarchy(hierarchy, onCreated);
            Executor.AddGuiHierarchiesCache.Add(hierarchy.FullHierarchy, AddGuiHierarchy);
        }

        public static void AddLinkedGuiHierarchy<GuiType>(LinkedGuiHierarchy hierarchy, Action<PrefsCanvas, AbstractHierarchy, GuiType> onCreated)
            where GuiType : PrefsGuiButton
        {
            void AddGuiHierarchy() => PrefsGuis.AddHierarchy(hierarchy, onCreated);
            Executor.AddGuiHierarchiesCache.Add(hierarchy.FullHierarchy, AddGuiHierarchy);
        }

        public static void RemoveGuiHierarchy(Guid hierarchyId)
        {
            void RemoveGuiHierarchy() => PrefsGuis.RemoveHierarchy(ref hierarchyId);
            Executor.RemoveGuiHierarchiesCache.Add(hierarchyId, RemoveGuiHierarchy);
        }

        public static void AddPrefs<ValType, GuiType>(PrefsValueBase<ValType> prefs, Action<GuiType> onCreated)
            where GuiType : PrefsGuiBase, IPrefsGuiConnector<ValType, GuiType>
        {
            void AddPrefs() => PrefsGuis.AddPrefs(prefs, onCreated);
            Executor.AddPrefsCache.Add(prefs.SaveKey, AddPrefs);

            if(Inited == false)
            {
                FastPrefsCache.Push(prefs);
            }

            void OnPrefsEdited() => OnAnyPrefsEditedInGui(prefs);
            prefs.OnEditedInGui += OnPrefsEdited;
        }

        public static void RemovePrefs(Guid prefsId)
        {
            void RemovePrefs() => PrefsGuis.RemovePrefs(ref prefsId);
            Executor.RemovePrefsCache.Add(prefsId, RemovePrefs);
        }
    }
}
