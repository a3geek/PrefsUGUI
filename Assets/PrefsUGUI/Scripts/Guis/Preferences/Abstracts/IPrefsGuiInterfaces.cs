using System;
using UnityEngine;

namespace PrefsUGUI.Guis.Preferences
{
    using PrefsUGUI.Preferences.Abstracts;

    public interface IPrefsGuiEvents<ValType>
    {
        void OnEditedInGui(ValType value);
        void OnClickedDefaultButton();
        ValType GetDefaultValue();
    }

    public interface IPrefsGuiBase
    {
        //event Action OnEditedInGui;

        void SetBottomMargin(float value);
        float GetBottomMargin();
        void SetTopMargin(float value);
        float GetTopMargin();
        void SetLabel(string label);
        string GetLabel();
        void SetVisible(bool visible);
        bool GetVisible();
    }

    public interface IPrefsGuiConnector<ValType, ComponentType> : IPrefsGuiBase where ComponentType : PrefsGuiBase
    {
        ComponentType Component { get; }

        ValType GetValue();
        void SetValue(ValType value);
        void SetGuiListeners(PrefsValueBase<ValType> prefs, IPrefsGuiEvents<ValType> events);
    }
}
