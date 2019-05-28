using System;

namespace PrefsUGUI.Examples
{
    public static class Structs
    {
        public static readonly GuiHierarchy HierarchyTest1 = new GuiHierarchy("Test1", 1);
        public static readonly GuiHierarchy HierarchyTest2 = new GuiHierarchy("Test2", 0);
        public static readonly GuiHierarchy HierarchyTest3 = new GuiHierarchy("Test3", 2);
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
