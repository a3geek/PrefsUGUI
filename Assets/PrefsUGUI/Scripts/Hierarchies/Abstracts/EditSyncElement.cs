using UnityEngine;

namespace PrefsUGUI
{
    using Managers.Classes;

    public partial class RemovableHierarchy : Hierarchy
    {
        protected class EditSyncElement : IEditSyncElement
        {
            public class Message : EditSyncBaseMessage
            {
                public PrefsType PrefsType => this.prefsType;

                [SerializeField]
                protected PrefsType prefsType = PrefsType.None;


                public Message(string json) : base(json)
                {
                }

                public Message(string key, PrefsType prefsType) : base(key)
                {
                    this.prefsType = prefsType;
                }
            }

            private RemovableHierarchy parent = null;


            public EditSyncElement(RemovableHierarchy parent)
            {
                this.parent = parent;
            }

            public PrefsType GetPrefsType()
                => this.parent.GetPrefsType();

            public string GetEditSyncKey()
                => this.parent.GetEditSyncKey();

            public string GetEditSyncMessage()
                => this.parent.GetEditSyncMessage();

            public void OnReceivedEditSyncMessage(string message)
                => this.parent.OnReceivedEditSyncMessage(message);

            public void RegistSendMessageEvent(IEditSyncSender sender)
                => this.parent.RegistSendMessageEvent(sender);
        }

        protected EditSyncElement editSyncElement = null;


        protected virtual PrefsType GetPrefsType()
            => PrefsType.RemovableHierarchy;

        protected virtual string GetEditSyncKey()
            => this.SaveKeyPath;

        protected virtual string GetEditSyncMessage()
            => new EditSyncElement.Message(this.GetEditSyncKey(), this.GetPrefsType()).ToJson();

        protected virtual void OnReceivedEditSyncMessage(string message)
        {
            var syncedMessage = new EditSyncElement.Message(message);
            if(syncedMessage.Key == this.GetEditSyncKey() && this.UnEditSync == false)
            {
                this.OnReceivedEditSyncMessage(syncedMessage);
            }
        }

        protected virtual void OnReceivedEditSyncMessage(EditSyncElement.Message message)
            => this.ManualRemove();

        protected virtual void RegistSendMessageEvent(IEditSyncSender sender)
            => this.OnRemoved += () => sender.Send(this.editSyncElement);
    }
}
