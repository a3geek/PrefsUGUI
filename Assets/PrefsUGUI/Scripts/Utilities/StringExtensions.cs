namespace PrefsUGUI.Utilities
{
    public static class StringExtensions
    {
        public static int ToInt(this string str, int defaultValue = default(int))
        {
            var v = 0;
            return int.TryParse(str, out v) == true ? v : defaultValue;
        }

        public static float ToFloat(this string str, float defaultValue = default(float))
        {
            var v = 0f;
            return float.TryParse(str, out v) == true ? v : defaultValue;
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
