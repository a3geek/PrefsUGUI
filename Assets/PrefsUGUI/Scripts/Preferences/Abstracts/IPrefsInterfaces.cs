using System;

namespace PrefsUGUI
{
    public interface IReadOnlyPrefs<ValType> : IDisposable
    {
        #region "PrefsBase"
        event Action OnValueChanged;
        event Action OnEditedInGui;
        event Action OnDisposed;

        Guid PrefsId { get; }
        string SaveKey { get; }
        string Key { get; }
        string GuiLabel { get; }
        bool Unsave { get; }
        Hierarchy GuiHierarchy { get; }
        int GuiSortOrder { get; }

        void ResetDefaultValue();
        void Reload();
        #endregion

        #region "PrefsValueBase"
        ValType Value { get; }
        ValType DefaultValue { get; }

        ValType Get();
        ValType GetDefaultValue();
        #endregion

        #region "PrefsGuiBase"
        string GuiLabelWithoutAffix { get; }
        bool IsCreatedGui { get; }
        float BottomMargin { get; }
        float TopMargin { get; }
        bool VisibleGUI { get; }
        string GuiLabelPrefix { get; }
        string GuiLabelSufix { get; }
        #endregion

    }
    public interface IPrefsCommon : IDisposable
    {
        #region "PrefsBase"
        event Action OnValueChanged;
        event Action OnEditedInGui;
        event Action OnDisposed;

        Guid PrefsId { get; }
        string SaveKey { get; }
        string Key { get; }
        string GuiLabel { get; }
        bool Unsave { get; set; }
        Hierarchy GuiHierarchy { get; }
        int GuiSortOrder { get; }

        void ResetDefaultValue();
        void Reload();
        #endregion

        #region "PrefsGuiBase"
        string GuiLabelWithoutAffix { get; }
        bool IsCreatedGui { get; }
        float BottomMargin { get; set; }
        float TopMargin { get; set; }
        bool VisibleGUI { get; set; }
        string GuiLabelPrefix { get; set; }
        string GuiLabelSufix { get; set; }
        #endregion
    }
}
