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
    public class PrefsGuiEnum : PrefsInputGuiBase<int, PrefsGuiEnum>
    {
        public override PrefsGuiEnum Component => this;

        [SerializeField]
        protected Dropdown dropdown = null;

        protected bool inited = false;
        protected Dictionary<int, int> indexToValue = new Dictionary<int, int>();


        protected override void Awake()
        {
            base.Awake();
            this.dropdown.onValueChanged.AddListener(this.OnDropdownChanged);
        }

        protected override void Reset()
        {
            base.Reset();
            this.dropdown = this.GetComponentInChildren<Dropdown>();
        }

        public virtual void Initialize<T>(string label, int initialValue, Func<int> defaultGetter) where T : Enum
        {
            this.SetLabel(label);

            this.defaultGetter = defaultGetter;
            this.Refresh<T>(initialValue);

            this.inited = true;
        }

        protected virtual void Refresh<T>(int initialValue) where T : Enum
        {
            var options = new List<string>();
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            int? iniValue = null;

            for(var i = 0; i < values.Count(); i++)
            {
                var value = values.ElementAt(i);
                var valueInt = Convert.ToInt32(value);

                if(iniValue == null || valueInt == initialValue)
                {
                    iniValue = valueInt;
                }

                this.indexToValue[i] = valueInt;
                options.Add(value.ToString());
            }

            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(options);

            this.SetValue(iniValue ?? 0);
        }

        protected override void SetFields()
        {
            this.inited = false;
            base.SetFields();

            foreach(var pair in this.indexToValue)
            {
                if(pair.Value == this.value)
                {
                    // list index.
                    this.dropdown.value = pair.Key;
                }
            }

            this.inited = true;
        }

        protected override bool IsDefaultValue()
            => this.value == this.defaultGetter();

        protected virtual void OnDropdownChanged(int index)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValueInternal(this.indexToValue[index]);
            this.FireOnValueChanged();
        }
    }
}
