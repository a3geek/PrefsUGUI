using UnityEngine;

namespace PrefsUGUI.Guis.Prefs
{
    [DisallowMultipleComponent]
    public abstract class GuiBase : MonoBehaviour
    {
        public abstract void SetLabel(string label);
        public abstract void SetValue(object value);

        public abstract string GetLabel();
        public abstract object GetValueObject();
    }
}
