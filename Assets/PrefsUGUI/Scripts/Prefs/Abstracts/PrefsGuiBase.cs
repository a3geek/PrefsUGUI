using System;
using UnityEngine;

namespace PrefsUGUI
{
    using Guis.Prefs;

    public static partial class Prefs
    {
        public abstract class PrefsGuiBase<ValType, GuiType> : PrefsValueBase<ValType>, IReadOnlyPrefs<ValType> where GuiType : PrefsInputGuiBase<ValType>
        {
            public override string GuiLabel => this.guiLabelPrefix + this.guiLabel + this.guiLabelSufix;
            public virtual string GuiLabelWithoutAffix => base.GuiLabel;

            public virtual float BottomMargin
            {
                get => this.gui?.GetBottomMargin() ?? 0f;
                set => this.gui?.SetBottomMargin(Mathf.Max(0f, value));
            }
            public virtual float TopMargin
            {
                get => this.gui?.GetTopMargin() ?? 0f;
                set => this.gui?.SetTopMargin(Mathf.Max(0f, value));
            }
            public virtual bool VisibleGUI
            {
                get => this.gui?.GetVisible() ?? false;
                set => this.gui?.SetVisible(value);
            }
            public virtual string GuiLabelPrefix
            {
                get => this.guiLabelPrefix;
                set
                {
                    this.guiLabelPrefix = value ?? "";
                    this.UpdateLabel();
                }
            }
            public virtual string GuiLabelSufix
            {
                get => this.guiLabelSufix;
                set
                {
                    this.guiLabelSufix = value ?? "";
                    this.UpdateLabel();
                }
            }

            [SerializeField]
            protected string guiLabelPrefix = "";
            [SerializeField]
            protected string guiLabelSufix = "";

            protected GuiType gui = null;
            protected Action<PrefsGuiBase<ValType, GuiType>> onCreatedGui = null;


            public PrefsGuiBase(
                string key, ValType defaultValue = default, GuiHierarchy hierarchy = null, string guiLabel = null,
                Action<PrefsGuiBase<ValType, GuiType>> onCreatedGui = null
            )
                : base(key, defaultValue, hierarchy, guiLabel)
            {
                this.onCreatedGui = onCreatedGui;
            }

            protected override void OnRegisted()
                => AddPrefs<ValType, GuiType>(this, gui => this.OnCreatedGui(gui));

            protected virtual void OnCreatedGui(GuiType gui)
            {
                this.gui = gui;
                this.OnCreatedGuiInternal(gui);
                this.onCreatedGui?.Invoke(this);
            }

            protected virtual void UpdateLabel()
                => this.gui?.SetLabel(this.GuiLabel);

            protected abstract void OnCreatedGuiInternal(GuiType gui);


            public static implicit operator ValType(PrefsGuiBase<ValType, GuiType> prefs)
                => prefs.Get();
        }
    }
}
