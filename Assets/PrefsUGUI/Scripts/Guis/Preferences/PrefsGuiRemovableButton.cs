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

        public virtual void Initialize(string label, UnityAction callback, UnityAction onRemoveClicked)
        {
            this.Initialize(label, callback);
            this.onRemoveClicked = onRemoveClicked;

            this.removeButton.onClick.AddListener(this.OnRemoveButtonClicked);
        }

        protected virtual void OnRemoveButtonClicked()
        {
            ;
        }

        public override void SetGuiListeners(Prefs.PrefsValueBase<UnityAction> prefs)
        {
            base.SetGuiListeners(prefs);

        }

        public override void Dispose()
        {
            base.Dispose();

            this.removeButton.onClick.RemoveAllListeners();
            this.onRemoveClicked = null;
        }
    }
}
