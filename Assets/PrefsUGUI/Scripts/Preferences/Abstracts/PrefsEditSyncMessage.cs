using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using CustomExtensions.Csharp;
    using Managers.Classes;
    using Managers;
    
    public abstract partial class PrefsValueBase<ValType>
    {
        public class Message : EditSyncMessage<ValType>
        {
            public Message(string json) : base(json)
            {
            }

            public Message(string saveKey, ValType value) : base(saveKey, value)
            {
            }
        }


        public override void OnReceivedEditSyncMessage(string message)
        {
            var esm = new Message(message);
            if(esm.SaveKey == this.SaveKey && this.UnEditSync == false)
            {
                this.Set(esm.Value);
            }
        }

        public Message GetEditSyncMessage()
            => new Message(this.SaveKey, this.Get());
    }
}
