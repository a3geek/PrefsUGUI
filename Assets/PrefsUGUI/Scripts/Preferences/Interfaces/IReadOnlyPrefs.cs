using System;

namespace PrefsUGUI
{
    public interface IReadOnlyPrefs<ValType> : IDisposable, IPrefsCommon
    {
        #region "PrefsValueBase"
        ValType Value { get; }
        ValType DefaultValue { get; }

        ValType Get();
        ValType GetDefaultValue();
        #endregion
    }
}
