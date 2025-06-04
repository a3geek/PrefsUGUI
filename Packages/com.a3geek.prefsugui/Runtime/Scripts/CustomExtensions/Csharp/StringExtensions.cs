using System;
using System.Text;

namespace PrefsUGUI.CustomExtensions.CSharp
{
    public static class StringExtensions
    {
        public static int ToInt(this string str, int defaultValue = 0)
            => int.TryParse(str, out var v) ? v : defaultValue;

        public static float ToFloat(this string str, float defaultValue = 0f)
            => float.TryParse(str, out var v) ? v : defaultValue;

        public static string ToLabelable(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            var span = str.AsSpan();
            var builder = new StringBuilder(str.Length);

            builder.Append(span[0]);
            for (var i = 0; i < span.Length; i++)
            {
                var previous = 0 <= i - 1 ? span[i - 1] : '\0';
                var next = i + 1 < span.Length ? span[i + 1] : '\0';
                var isNeedSpace = char.IsUpper(span[i]) && (char.IsLower(previous) || char.IsLower(next));

                if (isNeedSpace)
                {
                    builder.Append(" ");
                }
                builder.Append(span[i]);
            }

            return builder.ToString();
        }
    }
}
