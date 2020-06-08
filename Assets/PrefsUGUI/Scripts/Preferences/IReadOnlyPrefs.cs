using System;

namespace PrefsUGUI
{
    public interface IReadOnlyPrefs<ValType> : IDisposable
    {
        #region "PrefsBase"
        event Action OnValueChanged;

        string SaveKey { get; }
        string Key { get; }
        string GuiLabel { get; }
        bool Unsave { get; }
        GuiHierarchy GuiHierarchy { get; }
        #endregion

        #region "PrefsValueBase"
        ValType Value { get; }
        ValType DefaultValue { get; }

        ValType Get();
        ValType GetDefaultValue();
        #endregion

        #region "PrefsGuiBase"
        string GuiLabelWithoutAffix { get; }
        float BottomMargin { get; }
        float TopMargin { get; }
        bool VisibleGUI { get; }
        string GuiLabelPrefix { get; }
        string GuiLabelSufix { get; }
        #endregion
    }
}
