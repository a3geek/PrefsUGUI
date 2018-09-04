using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;
    using XmlStorage;

    [Serializable]
    public class PrefsEnum<T> : Prefs.PrefsParam<T, PrefsGuiEnum> where T : struct
    {
        public override object ValueAsObject
        {
            get { return this.index; }
            set
            {
                if(value is T == true)
                {
                    this.index = this.GetIndex((T)value);
                    base.ValueAsObject = value;
                }
                else if(value is int == true)
                {
                    var index = (int)value;
                    if(index < 0 || index >= this.values.Length)
                    {
                        return;
                    }

                    this.Set(this.values[index]);
                }
            }
        }

        protected int defaultIndex = 0;

        protected int index = 0;
        protected T[] values = new T[0];
        protected Dictionary<int, int> valueToIndex = new Dictionary<int, int>();
        

        public PrefsEnum(string key, T defaultValue = default(T), string guiHierarchy = "", string guiLabel = "")
            : base(key, defaultValue, guiHierarchy, guiLabel)
        {
            var type = typeof(T);
            if(type.IsEnum == false)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var values = Enum.GetValues(type).Cast<T>().OrderBy(e => e);
            this.values = new T[values.Count()];

            var i = 0;
            foreach(var e in values)
            {
                this.values[i] = e;
                this.valueToIndex[this.GetInt(e)] = i;
                this.index = (e.Equals(defaultValue) == true ? i : this.index);

                i++;
            }
            
            this.defaultIndex = this.index;
        }

        protected override void OnCreatedGui(PrefsGuiEnum gui)
        {
            var list = this.values.Select(v => v.ToString()).ToList();
            gui.Initialize(this.GuiLabel, list, () => this.defaultIndex, this.GetIndex(this.Get()));
        }

        public override void Reload(bool withEvent = true)
        {
            var index = Storage.Get(this.ValueType, this.SaveKey, this.defaultIndex, Prefs.AggregationName);
            this.SetValueInternal(this.values[index], withEvent);
        }

        protected override void SetValueInternal(T value, bool withEvent = true)
        {
            this.index = this.GetIndex(value);
            base.SetValueInternal(value, withEvent);
        }

        protected int GetIndex(T value)
        {
            return this.valueToIndex[this.GetInt(value)];
        }

        protected int GetInt(T value)
        {
            return Convert.ToInt32(value);
        }
    }
}
