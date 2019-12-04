using System;

namespace PrefsUGUI
{
    public interface IReadOnlyPrefs<ValType>
    {
        #region "PrefsBase"
        event Action OnValueChanged;

        string Key { get; }
        string GuiLabel { get; }
        #endregion

        #region "PrefsValueBase"
        ValType Value { get; }
        ValType DefaultValue { get; }
        Type ValueType { get; }

        ValType Get();
        #endregion

        #region "PrefsGUiBaseConnector"
        event Action OnCreatedGui;

        string GuiLabelWithoutAffix { get; }
        float BottomMargin { get; }
        float TopMargin { get; }
        bool VisibleGUI { get; }
        string GuiLabelPrefix { get; }
        string GuiLabelSufix { get; }
        #endregion
    }
}
