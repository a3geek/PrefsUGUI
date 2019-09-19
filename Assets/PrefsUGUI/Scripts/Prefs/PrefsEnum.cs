using System;

namespace PrefsUGUI
{
    using Guis.Prefs;

    [Serializable]
    public class PrefsEnum<T> : Prefs.PrefsExtends<int, PrefsGuiEnum> where T : struct
    {
        public Type EnumType => this.enumType ?? (this.enumType = typeof(T));

        protected Type enumType = null;

        public PrefsEnum(string key, T defaultValue = default(T), GuiHierarchy hierarchy = null, string guiLabel = null)
            : base(key, 0, hierarchy, guiLabel)
        {
            var type = typeof(T);
            if(type.IsEnum == false)
            {
                throw new ArgumentException(nameof(T) + " must be an enumerated type");
            }

            this.enumType = type;
            this.defaultValue = Convert.ToInt32(defaultValue);
        }

        protected override void OnCreatedGuiInternal(PrefsGuiEnum gui)
            => gui.Initialize<T>(
                this.GuiLabel, this.EnumType, this.Get(), v => Convert.ToInt32(v), () => this.DefaultValue
            );
    }
}
