using System;

namespace PrefsUGUI
{
    public interface IReadOnlyPrefs<ValType> : IDisposable
    {
        #region "PrefsBase"

        string Key { get; }
        string GuiLabel { get; }
        #endregion

        #region "PrefsValueBase"
        ValType Value { get; }
        ValType DefaultValue { get; }

        ValType Get();
        #endregion

        #region "PrefsGUiBaseConnector"

        string GuiLabelWithoutAffix { get; }
        float BottomMargin { get; }
        float TopMargin { get; }
        bool VisibleGUI { get; }
        string GuiLabelPrefix { get; }
        string GuiLabelSufix { get; }
        #endregion
    }
}
