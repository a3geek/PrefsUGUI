using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using CustomExtensions.Csharp;
    using Managers.Classes;
    using Managers;

    public class EditSyncBaseMessage : ISerializationCallbackReceiver
    {
        public string SaveKey => this.saveKey;

        [SerializeField]
        protected string saveKey = "";

        protected string json = "";


        public EditSyncBaseMessage(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
            this.json = json;
        }

        /// <remarks<see cref="JsonUtility.FromJsonOverwrite(string, object)"/>関数によって、コールされる</remarks>
        public virtual void OnAfterDeserialize()
        {
        }

        /// <remarks<see cref="JsonUtility.FromJsonOverwrite(string, object)"/>関数によって、コールされる</remarks>
        public virtual void OnBeforeSerialize()
        {
        }

        public virtual string ToJson()
            => string.IsNullOrEmpty(this.json) == false ? this.json : JsonUtility.ToJson(this);
    }

    public class EditSyncMessage<T> : EditSyncBaseMessage
    {
        public T Value => this.value;


        [SerializeField]
        protected T value = default;


        public EditSyncMessage(string json) : base(json)
        {
        }

        public EditSyncMessage(string saveKey, T value) : base("")
        {
            this.saveKey = saveKey;
            this.value = value;
        }
    }

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
