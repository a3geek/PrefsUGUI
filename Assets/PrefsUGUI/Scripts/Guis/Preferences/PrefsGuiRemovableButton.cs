using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    [Serializable]
    public class PrefsGuiRemovableButton : PrefsGuiButton
    {
        [SerializeField]
        protected Button removeButton = null;

        protected UnityAction onRemoveClicked = null;


        protected override void Reset()
        {
            base.Reset();
            this.removeButton = this.GetComponentsInChildren<Button>()?.LastOrDefault();
        }
    }
}
