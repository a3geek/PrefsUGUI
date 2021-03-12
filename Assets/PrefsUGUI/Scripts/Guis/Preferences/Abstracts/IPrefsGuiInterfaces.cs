using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    using PrefsUGUI.Preferences.Abstracts;

    public interface IPrefsGuiEvents<ValType, GuiType>
    {
        void OnCreatedGui(GuiType gui);
        void OnEditedInGui(ValType value);
        void OnClickedDefaultButton();
        ValType GetDefaultValue();
    }

    public interface IPrefsGuiBase
    {
        void SetBottomMargin(float value);
        float GetBottomMargin();
        void SetTopMargin(float value);
        float GetTopMargin();
        void SetLabel(string label);
        string GetLabel();
        void SetVisible(bool visible);
        bool GetVisible();
    }

    public interface IPrefsGuiConnector<ValType, GuiType> : IPrefsGuiBase where GuiType : PrefsGuiBase
    {
        GuiType Component { get; }

        ValType GetValue();
        void SetValue(ValType value);
        void SetGuiListeners(PrefsValueBase<ValType> prefs, IPrefsGuiEvents<ValType, GuiType> events);
    }
}
