using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Managers
{
    using Classes;

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

        private EditSyncSender sender = new EditSyncSender();
        private Dictionary<string, IEditSyncElement> elements = new Dictionary<string, IEditSyncElement>();


        public bool ReceivedEditSyncMessage(string message)
            => this.ReceivedEditSyncMessage(new EditSyncBaseMessage(message));

        public bool ReceivedEditSyncMessage(EditSyncBaseMessage baseMessage)
        {
            if(baseMessage.Method == EditSyncBaseMessage.MethodName)
            {
                if(this.elements.TryGetValue(baseMessage.Key, out var element) == true)
                {
                    element.OnReceivedEditSyncMessage(baseMessage.ToJson());
                    return true;
                }
            }

            return false;
        }

        public static bool AddElement(IEditSyncElement element)
        {
            if(TryGetInstance(out var instance) == false)
            {
                return false;
            }

            instance.elements[element.GetEditSyncKey()] = element;
            element.RegistSendMessageEvent(instance.sender);
            return true;
        }

        public static bool RemoveElement(IEditSyncElement element)
            => TryGetInstance(out var instance) != false && instance.elements.Remove(element.GetEditSyncKey());

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
                return true;
            }
            else if(Instance == null && IsTryedFindInstance == true)
            {
                instance = null;
                return false;
            }

            instance = Instance = FindObjectOfType<EditSyncManager>();
            IsTryedFindInstance = true;
            return instance != null;
        }


        private class EditSyncSender : IEditSyncSender
        {
            public void Send(IEditSyncElement element)
            {
                if(TryGetInstance(out var instance) == false || instance.transfer == null)
                {
                    return;
                }
                
                instance.transfer.Send(element.GetEditSyncMessage());
            }
        }
    }
}
