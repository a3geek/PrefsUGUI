using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsEnum<T> : Prefs.PrefsGuiBase<int, PrefsGuiEnum> where T : Enum
    {
        public Type EnumType { get; protected set; }


        public PrefsEnum(
            string key, T defaultValue = default, GuiHierarchy hierarchy = null,
            string guiLabel = null, Action<Prefs.PrefsGuiBase<int, PrefsGuiEnum>> onCreatedGui = null
        )
            : base(key, 0, hierarchy, guiLabel, onCreatedGui)
        {
            this.EnumType = typeof(T);
            this.defaultValue = Convert.ToInt32(defaultValue);
        }

        protected override void OnCreatedGuiInternal(PrefsGuiEnum gui)
            => gui.Initialize<T>(
                this.GuiLabel, this.EnumType, this.Get(), v => Convert.ToInt32(v), this.GetDefaultValue
            );
    }
}
