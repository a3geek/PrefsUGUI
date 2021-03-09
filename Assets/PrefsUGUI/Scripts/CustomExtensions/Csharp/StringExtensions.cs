namespace PrefsUGUI.CustomExtensions.Csharp
{
    public static class StringExtensions
    {
        public static int ToInt(this string str, int defaultValue = default)
            => int.TryParse(str, out var v) == true ? v : defaultValue;

        public static float ToFloat(this string str, float defaultValue = default)
            => float.TryParse(str, out var v) == true ? v : defaultValue;

        public static string ToLabelable(this string str)
        {
            var label = str.Length > 0 ? str[0].ToString() : "";
            for(var i = 1; i < str.Length; i++)
            {
                var c = str[i];
                var next = i + 1 < str.Length ? str[i + 1] : '\0';
                var previous = i - 1 > 0 ? str[i - 1] : '\0';
                var isNeedSpace = char.IsUpper(c) && (char.IsLower(previous) || char.IsLower(next));

                label += (isNeedSpace ? " " : "") + c.ToString();
            }

            return label;
        }
    }
}
