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


        public void ReceivedEditSyncMessage(string message)
        {
            var baseMessage = new EditSyncBaseMessage(message);
            if(this.prefs.TryGetValue(baseMessage.SaveKey, out var prefs) == true)
            {
                prefs.OnReceivedEditSyncMessage(message);
            }
            else if(this.buttons.TryGetValue(baseMessage.SaveKey, out var button) == true)
            {
                button.ManualClick();
            }
        }

        public static void AddPrefs<ValType>(PrefsValueBase<ValType> prefs)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            void OnEdited()
            {
                instance.transfer.Send(prefs.GetEditSyncMessage().ToJson());
            }

            instance.prefs[prefs.SaveKey] = prefs;
            prefs.OnEditedInGui += OnEdited;
        }

        public static void AddPrefs(PrefsButton prefs)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }

            void OnEdited()
            {
                instance.transfer.Send(prefs.GetEditSyncMessage().ToJson());
            }

            instance.buttons[prefs.SaveKey] = prefs;
            prefs.OnEditedInGui += OnEdited;
        }

        public static void SetSyncTransfer(ISyncTransfer transfer)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return;
            }
            instance.transfer = transfer;
        }

        private static bool TryGetInstance(out EditSyncManager instance)
        {
            if(Instance != null)
            {
                instance = Instance;
                return Instance.enabled && Instance.transfer != null;
            }
            else if(Instance == null && IsTryedFindInstance == true)
            {
                instance = null;
                return false;
            }

            instance = Instance = FindObjectOfType<EditSyncManager>();
            IsTryedFindInstance = true;
            return true;
        }
    }
}
