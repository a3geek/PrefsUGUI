using System;

namespace PrefsUGUI
{
    public interface IPrefsCommon : IDisposable
    {
        #region "PrefsBase"
        event Action OnValueChanged;
        event Action OnDisposed;

        Guid PrefsId { get; }
        string SaveKey { get; }
        string Key { get; }
        string GuiLabel { get; }
        bool Unsave { get; set; }
        GuiHierarchy GuiHierarchy { get; }
        int GuiSortOrder { get; }

        void ResetDefaultValue();
        void Reload();
        void Reload(bool withEvent);
        #endregion

        #region "PrefsGuiBase"
        string GuiLabelWithoutAffix { get; }
        float BottomMargin { get; set; }
        float TopMargin { get; set; }
        bool VisibleGUI { get; set; }
        string GuiLabelPrefix { get; set; }
        string GuiLabelSufix { get; set; }
        #endregion
    }
}
