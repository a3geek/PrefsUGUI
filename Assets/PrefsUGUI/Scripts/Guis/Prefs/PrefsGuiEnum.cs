using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiEnum : InputGuiBase
    {
        [SerializeField]
        protected Dropdown dropdown = null;

        protected int value = 0;
        protected bool inited = false;
        protected List<string> list = new List<string>();
        protected Func<int> defaultGetter = null;


        protected override void Awake()
        {
            base.Awake();
            this.dropdown.onValueChanged.AddListener(this.OnChangedDropdown);
        }

        public int GetValue() => this.value;

        public string GetOption() => this.list[this.value];

        public void SetValue(int value)
        {
            this.SetValueInternal(value);
            this.SetFields();
        }

        public void SetValue(List<string> list)
        {
            this.SetValueInternal(0);
            this.SetValueInternal(list);

            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(this.list);

            this.SetFields();
        }

        public void SetValue(List<string> value, int index)
        {
            this.SetValue(value);
            this.SetValue(index);
        }

        public void Initialize(string label, List<string> list, Func<int> defaultGetter, int index = 0)
        {
            this.defaultGetter = defaultGetter;
            this.SetLabel(label);
            this.SetValue(list, index);

            this.inited = true;
        }

        public override void SetValue(object value)
        {
            if(value is List<string> == true)
            {
                this.SetValue((List<string>)value);
            }
            else if(value is int == true)
            {
                this.SetValue((int)value);
            }
        }

        public override object GetValueObject() => this.GetValue();

        protected override UnityEvent<string>[] GetInputEvents() => new UnityEvent<string>[0];

        protected override bool IsDefaultValue() => this.GetValue() == this.defaultGetter();

        protected override void SetFields()
        {
            this.inited = false;

            base.SetFields();
            this.dropdown.value = this.value;

            this.inited = true;
        }

        protected virtual void OnChangedDropdown(int value)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValueInternal(value);
            this.FireOnValueChanged();
        }

        protected virtual void SetValueInternal(int value) => this.value = this.AdjustIndex(value);

        protected virtual void SetValueInternal(List<string> value) => this.list = value;

        protected override void SetValueInternal(string value)
        {
            var index = 0;
            if(int.TryParse(value, out index) == true)
            {
                this.SetValueInternal(index);
            }
        }

        protected virtual int AdjustIndex(int index) => Mathf.Max(0, Mathf.Min(this.list.Count - 1, index));

        protected override void Reset()
        {
            base.Reset();
            this.dropdown = GetComponentInChildren<Dropdown>();
        }
    }
}
