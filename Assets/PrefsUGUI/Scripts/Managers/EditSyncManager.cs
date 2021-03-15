using System;
using System.Collections.Concurrent;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers
{
    using Classes;
    using Guis;
    using Preferences.Abstracts;
    using XmlStorage;
    using Hierarchies.Abstracts;
    using XmlStorage.Systems;
    using XmlStorage.Systems.Utilities;
    using UnityObject = UnityEngine.Object;

    public interface ISyncTransfer
    {
        void Send(string text);
    }

    [AddComponentMenu("PrefsUGUI/Managers/Edit Sync Manager")]
    public class EditSyncManager : MonoBehaviour
    {
        public static EditSyncManager Instance { get; private set; }

        private static bool IsTryedFindInstance = false;

        [SerializeField]
        private ISyncTransfer transfer = null;

        private Dictionary<string, PrefsBase> prefs = new Dictionary<string, PrefsBase>();
        private Dictionary<string, PrefsButton> buttons = new Dictionary<string, PrefsButton>();
        private Dictionary<string, RemovableHierarchy> removableHierarchies = new Dictionary<string, RemovableHierarchy>();

        
        public void ReceivedEditSyncMessage(string message)
        {
            var baseMessage = new EditSyncBaseMessage(message);
            if(baseMessage.Method == EditSyncBaseMessage.MethodName)
            {
                if(this.prefs.TryGetValue(baseMessage.SaveKey, out var prefs) == true)
                {
                    prefs.OnReceivedEditSyncMessage(message);
                }
                else if(this.buttons.TryGetValue(baseMessage.SaveKey, out var button) == true)
                {
                    button.ManualClick();
                }
            }
            else if(baseMessage.Method == EditSyncRemoveHierarchyMessage.MethodName)
            {
                if(this.removableHierarchies.TryGetValue(baseMessage.SaveKey, out var hierarchy) == true)
                {
                    hierarchy.ManualRemove();
                }
            }
        }

        public static void AddPrefs<ValType>(PrefsValueBase<ValType> prefs)
        {
            if(AddPrefsInternal(prefs, out var instance) == true)
            {
                instance.prefs[prefs.SaveKey] = prefs;
            }
        }

        public static void AddPrefs(PrefsButton prefs)
        {
            if(AddPrefsInternal(prefs, out var instance) == true)
            {
                instance.buttons[prefs.SaveKey] = prefs;
            }
        }

        public static void AddRemovableHierarchy(RemovableHierarchy removableHierarchy)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            instance.removableHierarchies[removableHierarchy.SaveKeyPath] = removableHierarchy;
            removableHierarchy.OnRemoved +=
                () => instance.transfer.Send(removableHierarchy.GetEditSyncRemoveHierarchyMessage().ToJson());
        }

        public static void RemovePrefs<ValType>(PrefsValueBase<ValType> prefs)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            instance.prefs.Remove(prefs.SaveKey);
        }

        public static void RemovePrefs(PrefsButton prefs)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            instance.buttons.Remove(prefs.SaveKey);
        }
        
        public static void RemoveRemovableHierarchy(RemovableHierarchy removableHierarchy)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            instance.removableHierarchies.Remove(removableHierarchy.SaveKeyPath);
        }

        public static void SetSyncTransfer(ISyncTransfer transfer)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }
            instance.transfer = transfer;
        }

        private static bool AddPrefsInternal<ValType>(PrefsValueBase<ValType> prefs, out EditSyncManager instance)
        {
            if(TryGetInstance(out instance) == false)
            {
                return false;
            }

            prefs.OnEditedInGui += () => Instance.transfer.Send(prefs.GetEditSyncMessage().ToJson());
            return true;
        }

        private static bool TryGetInstance(out EditSyncManager instance)
        {
            if(Instance != null)
            {
                instance = Instance;
                return IsEnable(instance);
            }
            else if(Instance == null && IsTryedFindInstance == true)
            {
                instance = null;
                return false;
            }

            instance = Instance = FindObjectOfType<EditSyncManager>();
            IsTryedFindInstance = true;
            return IsEnable(instance);
        }

        private static bool IsEnable(EditSyncManager instance)
            => instance != null && instance.enabled == true && instance.transfer != null;
    }
}
