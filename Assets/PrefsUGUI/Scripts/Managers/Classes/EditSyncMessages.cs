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
        None = 0, Prefs, PrefsButton, RemovableHierarchy
    }

    public class EditSyncBaseMessage : ISerializationCallbackReceiver
    {
        public const string MethodName = "EditSync";

        public string Method => this.method;
        public string Key => this.key;

        [SerializeField]
        protected string method = "";
        [SerializeField]
        protected string key = "";

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
        public PrefsType PrefsType => this.prefsType;

        [SerializeField]
        protected T value = default;
        [SerializeField]
        protected PrefsType prefsType = PrefsType.None;


        public EditSyncMessage(string json) : base(json)
        {
        }

        public EditSyncMessage(string key, T value, PrefsType prefsType) : base("")
        {
            this.method = MethodName;
            this.key = key;
            this.value = value;
            this.prefsType = prefsType;
        }
    }

    //public class EditSyncRemoveHierarchyMessage : EditSyncBaseMessage
    //{
    //    public new const string MethodName = "EditSyncRemoveHierarchy";

    //    public PrefsType PrefsType => this.prefsType;

    //    [SerializeField]
    //    private PrefsType prefsType = PrefsType.RemovableHierarchy;


    //    public EditSyncRemoveHierarchyMessage(string json) : base(json)
    //    {
    //    }

    //    public EditSyncRemoveHierarchyMessage(string saveKey, PrefsType prefsType) : base("")
    //    {
    //        this.method = MethodName;
    //        this.saveKey = saveKey;
    //        this.prefsType = prefsType;
    //    }
    //}
}
