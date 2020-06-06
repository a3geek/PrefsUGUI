namespace PrefsUGUI
{
    /// <summary>
    /// Simplified wrapper for Unitys rich text.
    /// </summary>
    /// <remarks>Reference: https://docs.unity3d.com/Manual/StyledText.html </remarks>
    public static class RichTextColors
    {
        /// <summary>Top of color tag</summary>
        public const string ColorTop = @"<color=";
        /// <summary>Tops last of color tag</summary>
        public const string TopLast = @">";
        /// <summary>Bottom of color tag</summary>
        public const string ColorBottom = @"</color>";


        public static string Aqua(string text)
            => Get("aqua", text);

        public static string Black(string text)
            => Get("black", text);

        public static string Blue(string text)
            => Get("blue", text);

        public static string Brown(string text)
            => Get("brown", text);

        public static string Cyan(string text)
            => Get("cyan", text);

        public static string Darkblue(string text)
            => Get("darkblue", text);

        public static string Fuchsia(string text)
            => Get("fuchsia", text);

        public static string Green(string text)
            => Get("green", text);

        public static string Grey(string text)
            => Get("grey", text);

        public static string Lightblue(string text)
            => Get("lightblue", text);

        public static string Lime(string text)
            => Get("lime", text);

        public static string Magenta(string text)
            => Get("magenta", text);

        public static string Maroon(string text)
            => Get("maroon", text);

        public static string Navy(string text)
            => Get("navy", text);

        public static string Olive(string text)
            => Get("olive", text);

        public static string Orange(string text)
            => Get("orange", text);

        public static string Purple(string text)
            => Get("purple", text);

        public static string Red(string text)
            => Get("red", text);

        public static string Silver(string text)
            => Get("silver", text);

        public static string Teal(string text)
            => Get("teal", text);

        public static string White(string text)
            => Get("white", text);

        public static string Yellow(string text)
            => Get("yellow", text);

        /// <summary>Get the specific color snippets string.</summary>
        private static string Get(string color, string text)
            => ColorTop + color + TopLast + text + ColorBottom;
    }
}
