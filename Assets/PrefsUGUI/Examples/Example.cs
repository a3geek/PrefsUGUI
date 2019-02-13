using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    [AddComponentMenu("")]
    public class Example : MonoBehaviour
    {
        public static readonly GuiHierarchy HierarchyHoge = new GuiHierarchy("hoge/", 1);
        public static readonly GuiHierarchy HierarchyHogeFuga = new GuiHierarchy("fuga/", 1, HierarchyHoge);
        public static readonly GuiHierarchy HierarchyTest = new GuiHierarchy("Test", 0);

        [Serializable]
        public enum Test1
        {
            One = 1, Two, Three
        }

        [Serializable]
        private enum Test2
        {
            A = -1, B = 3, C = 4
        }

#pragma warning disable 414
        #region "Hoge/Fuga"
        [Serializable]
        private class HogeFuga
        {
            public PrefsVector2 v1 = new PrefsVector2("HogeFuga - v1", Vector2.one, HierarchyHogeFuga);
            public PrefsVector2 v2 = new PrefsVector2("HogeFuga - v2", Vector2.one * 2.5f, HierarchyHogeFuga);
        }
        [SerializeField]
        private HogeFuga hogeFuga = new HogeFuga();
        #endregion

        #region "Hoge/Fuga/Pi/"
        [Serializable]
        private class HogeFugaPi
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("pi/", 1, HierarchyHogeFuga);

            public PrefsVector2 v1 = new PrefsVector2("HogeFugaPi - v1", Vector2.right, Hierarchy);
            public PrefsVector2 v2 = new PrefsVector2("HogeFugaPi - v2", Vector2.left, Hierarchy);
        }
        [SerializeField]
        private HogeFugaPi hogeFugaPi = new HogeFugaPi();
        #endregion

        #region "Hoge/Fuga/Piyo/"
        private class HogeFugaPiyo
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("piyo/", 2, HierarchyHogeFuga);

            public PrefsVector2 v1 = new PrefsVector2("HogeFugaPiyo - v1", new Vector2(10f, 20f), Hierarchy);
            public PrefsVector3 v2 = new PrefsVector3("HogeFugaPiyo - v2", new Vector3(1f, 2f, 3f), Hierarchy);
        }
        private HogeFugaPiyo hogeFugaPiyo = new HogeFugaPiyo();
        #endregion

        #region "Hoge/Fuga/Piyo2/"
        private class HogeFugaPiyo2
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("piyo2/", -3, HierarchyHogeFuga);

            public PrefsVector3 v1 = new PrefsVector3("HogeFugaPiyo2 - v1", new Vector3(-1f, -1.5f, -2f), Hierarchy);
            public PrefsVector3Int v2 = new PrefsVector3Int("HogeFugaPiyo2 - v2", new Vector3Int(1, 2, 3), Hierarchy);
        }
        private HogeFugaPiyo2 hogeFugaPiyo2 = new HogeFugaPiyo2();
        #endregion

        #region "Hoge/Ppp/Aaa/"
        private class HogePppAaa
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("ppp/aaa/", new int[] { 2, -2 }, HierarchyHoge);

            public PrefsVector2Int v1 = new PrefsVector2Int("HogePppAaa - v1", Vector2Int.one, Hierarchy);
            public PrefsVector4 v2 = new PrefsVector4("HogePppAaa - v2", new Vector4(-1f, 0f, 1f, 2f), Hierarchy);
        }
        private HogePppAaa hogePppAaa = new HogePppAaa();
        #endregion

        #region "Hoge/Ppp/Bbb/"
        private class HogePppBbb
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("ppp/bbb/", new int[] { -2, 2 }, HierarchyHoge);

            public PrefsVector2Int v1 = new PrefsVector2Int("HogePppBbb - v1", Vector2Int.one, Hierarchy);
            public PrefsVector4 v2 = new PrefsVector4("HogePppBbb - v2", new Vector4(-1f, 0f, 1f, 2f), Hierarchy);
        }
        private HogePppBbb hogePppBbb = new HogePppBbb();
        #endregion

        #region "Hoge/Ppp/Ccc/"
        private class HogePppCcc
        {
            public static readonly GuiHierarchy Hierarchy = new GuiHierarchy("ppp/ccc/", new int[] { 0, -5 }, HierarchyHoge);

            public PrefsVector2Int v1 = new PrefsVector2Int("HogePppCcc - v1", Vector2Int.one, Hierarchy);
            public PrefsVector4 v2 = new PrefsVector4("HogePppCcc - v2", new Vector4(-1f, 0f, 1f, 2f), Hierarchy);
        }
        private HogePppCcc hogePppCcc = new HogePppCcc();
        #endregion
        
        [SerializeField]
        private PrefsVector2 v1 = new PrefsVector2("v1", Vector2.zero);
        [SerializeField]
        private PrefsVector2 v2 = new PrefsVector2("v2", Vector2.one);
        [SerializeField]
        private PrefsVector2 v3 = new PrefsVector2("v3", new Vector2(2f, 3f));
        private PrefsVector2 v4 = new PrefsVector2("v4", Vector2.zero);
        private PrefsVector2Int v5 = new PrefsVector2Int("v5");

        #region "Test/"
        [Serializable]
        private class Test
        {
            public PrefsBool b = new PrefsBool("PrefsBool", true, HierarchyTest, "Prefs " + RichTextColors.Red("Bool"));
            public PrefsColor c = new PrefsColor("PrefsColor", Color.red, HierarchyTest);
            public PrefsColorSlider cs = new PrefsColorSlider("ColorSlider", Color.blue, HierarchyTest);
            public PrefsFloat f = new PrefsFloat("PrefsFloat", 1f, HierarchyTest);
            public PrefsFloatSlider fs = new PrefsFloatSlider("PrefsFloatSlider", 0f, 10f, 5f, HierarchyTest);
            public PrefsInt i = new PrefsInt("PrefsInt", 5, HierarchyTest);
            public PrefsIntSlider isl = new PrefsIntSlider("PrefsIntSlider", -10, 10, 5, HierarchyTest);
            public PrefsVector3 v6 = new PrefsVector3("PrefsVector3", new Vector3(2f, 4f, 6f), HierarchyTest);
            public PrefsEnum<Test1> e1 = new PrefsEnum<Test1>("PrefsEnum1", Test1.Two, HierarchyTest);

            private PrefsString s = new PrefsString("PrefsString", "localhost", HierarchyTest);

            [SerializeField]
            private PrefsVector3Int v7 = new PrefsVector3Int("PrefsVector3Int", new Vector3Int(-10, -5, 0), HierarchyTest);
            [SerializeField]
            private PrefsVector4 v8 = new PrefsVector4("PrefsVector4", new Vector4(-2f, 0f, 2f, 100f), HierarchyTest);
            [SerializeField]
            private PrefsEnum<Test2> e2 = new PrefsEnum<Test2>("PrefsEnum2", Test2.A, HierarchyTest);
        }
        [SerializeField]
        private Test test = new Test();
        #endregion
#pragma warning restore 414


        private void Awake()
        {
            this.test.e1.BottomMargin = 50f;
            this.test.e1.GuiLabelPrefix = RichTextColors.Blue(" ~ ");
            this.test.e1.GuiLabelSufix = RichTextColors.Red(" ~ ");
        }

        void Update()
        {
            transform.position = this.test.v6;

            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                this.test.e1.VisibleGUI = !this.test.e1.VisibleGUI;
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                this.test.v6.Set(this.test.v6.Get() + Vector3.one);
            }
        }
    }
}
