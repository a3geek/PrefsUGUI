using System;

namespace PrefsUGUI.CustomExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class PrefsGuiComponentTypeAttribute : Attribute
    {
        public Type ComponentType { get; } = null;


        public PrefsGuiComponentTypeAttribute(Type type)
        {
            this.ComponentType = type;
        }
    }
}
