using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrefsUGUI.Managers.Classes
{
    public interface IPrefsStorageSetter
    {
        void SetStorageValue();
        void OnInitializedPrefs();
    }
}
