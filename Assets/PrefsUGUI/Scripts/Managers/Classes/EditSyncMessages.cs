using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers.Classes
{
    [Serializable]
    public enum PrefsType
    {
        None = 0, Prefs, PrefsButton, GuiHierarchy
    }

    public class EditSyncBaseMessage : ISerializationCallbackReceiver
    {
        public const string MethodName = "EditSync";

        public string Method => this.method;
        public string SaveKey => this.saveKey;

        [SerializeField]
        protected string method = "";
        [SerializeField]
        protected string saveKey = "";

        protected string json = "";


        public EditSyncBaseMessage(string json)
        {
            if(string.IsNullOrEmpty(json) == true)
            {
                return;
            }

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
            this.method = MethodName;
            this.saveKey = saveKey;
            this.value = value;
        }
    }

    public class EditSyncDisposeMessage : EditSyncBaseMessage
    {
        public new const string MethodName = "EditSyncDispose";

        public PrefsType PrefsType => this.prefsType;

        [SerializeField]
        private PrefsType prefsType = PrefsType.Prefs;


        public EditSyncDisposeMessage(string json) : base(json)
        {
        }

        public EditSyncDisposeMessage(string saveKey, PrefsType prefsType) : base("")
        {
            this.method = MethodName;
            this.saveKey = saveKey;
            this.prefsType = prefsType;
        }
    }
}
