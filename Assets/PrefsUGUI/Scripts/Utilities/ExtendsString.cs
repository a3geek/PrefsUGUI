using UnityEngine;
using UnityEngine.EventSystems;

namespace PrefsUGUI.Utilities
{
    public static class ExtendsString
    {
        public static int ToInt(this string str, int defaultValue = default(int))
        {
            var value = 0;
            return string.IsNullOrEmpty(str) == true ? 0 : (
                int.TryParse(str, out value) == true ? value : defaultValue
            );
        }

        public static float ToFloat(this string str, float defaultValue = default(float))
        {
            var value = 0f;
            return string.IsNullOrEmpty(str) == true ? 0 : (
                float.TryParse(str, out value) == true ? value : defaultValue
            );
        }

        public static string ToLabelable(this string str)
        {
            var label = "";
            for(var i = 0; i < str.Length; i++)
            {
                var c = str[i];
                label += (i > 0 && char.IsUpper(c) == true ? " " : "") + c.ToString();
            }

            return label;
        }
    }
}
