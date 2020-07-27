using System;

namespace PrefsUGUI.Examples
{
    public static class GuiHierarchies
    {
        public static readonly GuiHierarchy Test1Gui = new GuiHierarchy("Test1", 1);
        public static readonly GuiHierarchy Test1Ex2Gui = new GuiHierarchy("Ex2", 0, Test1Gui);
        public static readonly GuiHierarchy Test1Ex1Gui = new GuiHierarchy("Ex1", 0, Test1Gui);

        public static readonly LinkedGuiHierarchy LinkedTest1Gui = new LinkedGuiHierarchy("Linked Test1", 3, null, Test1Gui,
            onCreatedGui: hierarchy => hierarchy.VisibleGUI = true
        );

        public static readonly GuiHierarchy Test0Gui = new GuiHierarchy("Test0", 0);
        public static readonly GuiHierarchy Test2Gui = new GuiHierarchy("Test2", 2);
    }

    [Serializable]
    public enum TestEnum
    {
        MinusOne = -1, Zero = 0, Four = 4
    }
}
