using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    public static class Structs
    {
        public static readonly GuiHierarchy HierarchyHoge = new GuiHierarchy("hoge/", 1);
        public static readonly GuiHierarchy HierarchyHogeFuga = new GuiHierarchy("fuga/", 1, HierarchyHoge);
        public static readonly GuiHierarchy HierarchyTest = new GuiHierarchy("Test", 0);
    }

    [Serializable]
    public enum Test1
    {
        One = 1, Two, Three
    }
}
