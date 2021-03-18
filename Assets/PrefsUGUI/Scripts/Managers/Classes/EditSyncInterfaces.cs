using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers.Classes
{
    public interface IEditSyncSender
    {
        void Send(IEditSyncElement element);
    }

    public interface IEditSyncElement
    {
        PrefsType GetPrefsType();
        string GetEditSyncKey();
        string GetEditSyncMessage();
        void OnReceivedEditSyncMessage(string message);
        void RegistSendMessageEvent(IEditSyncSender sender);
    }
}
