using System;
using UnityEngine;

namespace PrefsUGUI.Preferences.Abstracts
{
    using CustomExtensions.Csharp;
    using Managers.Classes;
    using Managers;

    public abstract partial class PrefsBase
    {
        protected class StorageSetter : IPrefsStorageSetter
        {
            private PrefsBase prefs = null;


            public StorageSetter(PrefsBase prefs)
            {
                this.prefs = prefs;
            }

            public void SetStorageValue()
                => this.prefs.SetStorageValue();

            public void OnInitializedPrefs()
                => this.prefs.Reload();
        }
    }
}
