using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    /*
     * valueに保存するのはenumの整数値.
     */
    [AddComponentMenu("")]
    public class PrefsGuiEnum : InputGuiValueBase<int>
    {
        [SerializeField]
        protected Dropdown dropdown = null;

        protected bool inited = false;
        protected Dictionary<int, int> indexToValue = new Dictionary<int, int>();


        protected override void Awake()
        {
            base.Awake();
            this.dropdown.onValueChanged.AddListener(this.OnDropdownChanged);
        }

        public void Initialize<T>(string label, Type type, int initialValue, Func<T, int> converter, Func<int> defaultGetter)
        {
            this.SetLabel(label);

            this.defaultGetter = defaultGetter;
            this.Refresh(type, initialValue, converter);

            this.inited = true;
        }

        protected virtual void Refresh<T>(Type type, int initialValue, Func<T, int> converter)
        {
            if(type.IsEnum == false)
            {
                return;
            }

            var options = new List<string>();
            var values = Enum.GetValues(type).Cast<T>().OrderBy(e => e);
            var iniVal = 0;
            var i = 0;

            foreach(var e in values)
            {
                var ei = converter(e);

                this.indexToValue[i++] = ei;
                options.Add(e.ToString());

                if(ei == initialValue)
                {
                    iniVal = ei;
                }
            }

            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(options);

            this.SetValue(iniVal);
        }

        protected override void SetFields()
        {
            this.inited = false;
            base.SetFields();

            foreach(var pair in this.indexToValue)
            {
                if(pair.Value == this.value)
                {
                    this.dropdown.value = pair.Key; // list index.
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

        protected override void Reset()
        {
            base.Reset();
            this.dropdown = GetComponentInChildren<Dropdown>();
        }
    }
}
