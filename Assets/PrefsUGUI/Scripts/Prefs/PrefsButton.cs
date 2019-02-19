using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PrefsUGUI
{
    using Guis;
    using Guis.Prefs;

    [Serializable]
    public class PrefsButton : Prefs.PrefsGuiConnector<GuiButton>
    {
        public override Type ValueType => typeof(UnityAction);
        public override object DefaultValueAsObject => this.defaultAction;
        public override object ValueAsObject
        {
            get { return this.onClicked; }
            set
            {
                if(value is UnityAction == false)
                {
                    return;
                }

                this.SetValueInternal((UnityAction)value);
            }
        }
        public virtual UnityAction OnClicked
        {
            get { return this.onClicked; }
            set
            {
                this.onClicked = value ?? this.onClicked;
                this.SetValueInternal(this.onClicked);
            }
        }

        protected UnityAction defaultAction = delegate { };
        protected UnityAction onClicked = null;


        public PrefsButton(string key, UnityAction defaultAction = null, GuiHierarchy hierarchy = null, string guiLabel = "")
            : base(key, hierarchy, guiLabel)
        {
            this.defaultAction = defaultAction ?? this.defaultAction;
            this.onClicked = this.onClicked ?? this.defaultAction;
        }

        protected override void OnCreatedGuiInternal(GuiButton gui)
        {
            gui.OnValueChanged += () => this.FireOnValueChanged();
            gui.Initialize(this.GuiLabel, this.OnClicked);
        }

        public override void ResetDefaultValue()
        {
            this.Reload(false);
        }

        public override void Reload(bool withEvent = false)
        {
            this.SetValueInternal(this.defaultAction);
            if(withEvent == true)
            {
                this.defaultAction();
            }
        }

        protected virtual void SetValueInternal(UnityAction action)
        {
            this.gui?.SetValue(action);
        }
    }
}
