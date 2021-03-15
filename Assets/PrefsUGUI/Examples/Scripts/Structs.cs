using System;

namespace PrefsUGUI.Examples
{
    public static class GuiHierarchies
    {
        public static readonly Hierarchy Test1Gui = new Hierarchy("Test1", 1);
        public static readonly Hierarchy Test1Ex2Gui = new Hierarchy("Ex2", 0, Test1Gui,
            onCreatedGui: prefs => prefs.BottomMargin = 25f
        );
        public static readonly Hierarchy Test1Ex1Gui = new Hierarchy("Ex1", 0, Test1Gui);

        public static readonly LinkedHierarchy LinkedTest1Gui = new LinkedHierarchy("Linked Test1", Test1Gui, 3, null,
            onCreatedGui: hierarchy => hierarchy.VisibleGUI = true
        );

        public static readonly Hierarchy Test0Gui = new Hierarchy("Test0", 0);
        public static readonly Hierarchy Test2Gui = new Hierarchy("Test2", 2);
    }

    [Serializable]
    public enum TestEnum
    {
        MinusOne = -1, Zero = 0, Four = 4
    }
}
