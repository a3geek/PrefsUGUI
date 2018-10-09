using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PrefsUGUI
{
    public static class RichTextColors
    {
        public const string ColorTop = "<color=";
        public const string TopLast = ">";
        public const string ColorBottom = "</color>";
        

        public static string Aqua(string text)
        {
            return Get("aqua", text);
        }

        public static string Black(string text)
        {
            return Get("black", text);
        }

        public static string Blue(string text)
        {
            return Get("blue", text);
        }

        public static string Brown(string text)
        {
            return Get("brown", text);
        }

        public static string Cyan(string text)
        {
            return Get("cyan", text);
        }

        public static string Darkblue(string text)
        {
            return Get("darkblue", text);
        }

        public static string Fuchsia(string text)
        {
            return Get("fuchsia", text);
        }

        public static string Green(string text)
        {
            return Get("green", text);
        }

        public static string Grey(string text)
        {
            return Get("grey", text);
        }

        public static string Lightblue(string text)
        {
            return Get("lightblue", text);
        }

        public static string Lime(string text)
        {
            return Get("lime", text);
        }

        public static string Magenta(string text)
        {
            return Get("magenta", text);
        }

        public static string Maroon(string text)
        {
            return Get("maroon", text);
        }

        public static string Navy(string text)
        {
            return Get("navy", text);
        }

        public static string Olive(string text)
        {
            return Get("olive", text);
        }

        public static string Orange(string text)
        {
            return Get("orange", text);
        }

        public static string Purple(string text)
        {
            return Get("purple", text);
        }

        public static string Red(string text)
        {
            return Get("red", text);
        }

        public static string Silver(string text)
        {
            return Get("silver", text);
        }

        public static string Teal(string text)
        {
            return Get("teal", text);
        }

        public static string White(string text)
        {
            return Get("white", text);
        }

        public static string Yellow(string text)
        {
            return Get("yellow", text);
        }

        private static string Get(string color, string text)
        {
            return ColorTop + color + TopLast + text + ColorBottom;
        }
    }
}
