using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    public static class Structs
    {
        public static readonly GuiHierarchy HierarchyTest1 = new GuiHierarchy("Test1", 1);
        public static readonly GuiHierarchy HierarchyTest2 = new GuiHierarchy("Test2", 0);
        public static readonly GuiHierarchy HierarchyTest2Ex1 = new GuiHierarchy("Ex1", 0, HierarchyTest2);
        public static readonly GuiHierarchy HierarchyTest2Ex2 = new GuiHierarchy("Ex2", 0, HierarchyTest2);
    }

    [Serializable]
    public enum TestEnum1
    {
        One = 0, Two, Three
    }

    public partial class Example
    {
        [Serializable]
        private enum TestEnum2
        {
            A = -1, B = 0, C = 4
        }
    }
}
