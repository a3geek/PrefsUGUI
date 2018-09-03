using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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


        public void OnChangedDropdown(int value)
        {
            if(this.inited == false)
            {
                return;
            }

            this.SetValueInternal(value);
            this.FireOnValueChanged();
        }

        public int GetValue()
        {
            return this.value;
        }

        public string GetOption()
        {
            return this.list[this.value];
        }

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

        public override object GetValueObject()
        {
            return this.GetValue();
        }

        protected override bool IsDefaultValue()
        {
            return this.GetValue() == this.defaultGetter();
        }

        protected override void SetFields()
        {
            base.SetFields();
            this.dropdown.value = this.value;
        }

        protected virtual void SetValueInternal(int value)
        {
            this.value = this.AdjustIndex(value);
        }

        protected virtual void SetValueInternal(List<string> value)
        {
            this.list = value;
        }

        protected override void SetValueInternal(string value)
        {
            var index = 0;
            if(int.TryParse(value, out index) == true)
            {
                this.SetValueInternal(index);
            }
        }

        protected virtual int AdjustIndex(int index)
        {
            return Mathf.Max(0, Mathf.Min(this.list.Count - 1, index));
        }

        protected override void Reset()
        {
            base.Reset();
            this.dropdown = GetComponentInChildren<Dropdown>();
        }
    }
}
