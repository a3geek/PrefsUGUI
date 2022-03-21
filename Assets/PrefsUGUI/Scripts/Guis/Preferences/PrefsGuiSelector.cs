using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Preferences
{
    /// <summary>
    /// <see cref="Enum"/>型用のGUIクラス
    /// </summary>
    /// <remarks>valueに保存するのは<see cref="Enum"/>の整数値</remarks>
    [Serializable]
    [AddComponentMenu("")]
    public class PrefsGuiSelector : PrefsInputGuiBase<int, PrefsGuiSelector>
    {
        public override PrefsGuiSelector Component => this;

        [SerializeField]
        protected Dropdown dropdown = null;

        protected bool inited = false;


        protected virtual void Awake()
        {
            this.dropdown.onValueChanged.AddListener(this.OnDropdownChanged);
        }

        protected override void Reset()
        {
            base.Reset();
            this.dropdown = this.GetComponentInChildren<Dropdown>();
        }

        public virtual void Initialize(string label, List<string> options, int initialValue)
        {
            this.SetLabel(label);

            this.Refresh(options, initialValue);

            this.inited = true;
        }

        protected virtual void Refresh(List<string> options, int initialValue)
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(options);

            this.SetValue(initialValue);
        }

        protected override void SetFieldsInternal()
        {
            this.inited = false;
            base.SetFieldsInternal();

            this.dropdown.value = this.value;
            this.inited = true;
        }

        protected override bool IsDefaultValue()
            => this.value == this.prefsEvents.GetDefaultValue();

        protected virtual void OnDropdownChanged(int index)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValueInternal(index);
            this.SetFields(true);
        }
    }
}
