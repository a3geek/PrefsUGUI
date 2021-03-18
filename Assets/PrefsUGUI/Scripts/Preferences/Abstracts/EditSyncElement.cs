namespace PrefsUGUI.Preferences.Abstracts
{
    using Managers.Classes;

    public abstract partial class PrefsValueBase<ValType>
    {
        protected class EditSyncElement : IEditSyncElement
        {
            public class Message : EditSyncMessage<ValType>
            {
                public Message(string json) : base(json)
                {
                }

                public Message(string key, ValType value, PrefsType prefsType) : base(key, value, prefsType)
                {
                }
            }

            private PrefsValueBase<ValType> parent = null;


            public EditSyncElement(PrefsValueBase<ValType> parent)
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
            => PrefsType.Prefs;

        protected virtual string GetEditSyncKey()
            => this.SaveKey;

        protected virtual string GetEditSyncMessage()
            => new EditSyncElement.Message(this.GetEditSyncKey(), this.Get(), this.GetPrefsType()).ToJson();

        protected virtual void OnReceivedEditSyncMessage(string message)
        {
            var syncedMessage = new EditSyncElement.Message(message);
            if(syncedMessage.Key == this.GetEditSyncKey() && this.UnEditSync == false)
            {
                this.OnReceivedEditSyncMessage(syncedMessage);
            }
        }

        protected virtual void OnReceivedEditSyncMessage(EditSyncElement.Message message)
            => this.Set(message.Value);

        protected virtual void RegistSendMessageEvent(IEditSyncSender sender)
            => this.OnEditedInGui += () => sender.Send(this.editSyncElement);
    }
}
