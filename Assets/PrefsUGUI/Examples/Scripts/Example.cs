using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    using static Structs;

    [AddComponentMenu("")]
    public partial class Example : MonoBehaviour
    {
#pragma warning disable 0414
        [Serializable]
        private class Test1
        {
            public PrefsBool prefsBool = new PrefsBool("PrefsBool", true, HierarchyTest1, "Prefs " + RichTextColors.Red("Bool"));
            public PrefsColor prefsColor = new PrefsColor("PrefsColor", Color.red, HierarchyTest1);
            public PrefsColorSlider prefsColorSlider = new PrefsColorSlider("PrefsColorSlider", Color.blue, HierarchyTest1);
            public PrefsFloat prefsFloat = new PrefsFloat("PrefsFloat", 0.12345f, HierarchyTest1);
            public PrefsFloatSlider prefsFloatSlider = new PrefsFloatSlider("PrefsFloatSlider", 0f, 10f, 5f, HierarchyTest1);
            public PrefsInt prefsInt = new PrefsInt("PrefsInt", 3, HierarchyTest1);
            public PrefsIntSlider prefsIntSlider = new PrefsIntSlider("PrefsIntSlider", 0, 10, 2, HierarchyTest1);
            //public PrefsButton b1 = new PrefsButton("Click1", null, HierarchyTest);


            public Test1()
            {
                //this.b1.OnClicked = this.Click1;
                //this.b1.OnValueChanged += () => Debug.Log("B1 : OnValueChanged");
            }

            //private void Click1()
            //{
            //    Debug.Log("B1 : OnClicked");
            //}
        }
        [Serializable]
        private class Test2
        {
            public PrefsString prefsString = new PrefsString("PrefsString", "Example", HierarchyTest2);
            public PrefsVector2 prefsVector2 = new PrefsVector2("PrefsVector2", Vector2.one, HierarchyTest2);
            public PrefsVector2Int prefsVector2Int = new PrefsVector2Int("PrefsVector2Int", Vector2Int.right, HierarchyTest2);
            //private PrefsButton b3 = new PrefsButton("Click3", () => Debug.Log("B3 : DefaultAction"), HierarchyTest);

            [SerializeField]
            private PrefsVector3 prefsVector3 = new PrefsVector3("PrefsVector3", Vector3.down, HierarchyTest2);
            [SerializeField]
            private PrefsVector3Int prefsVector3Int = new PrefsVector3Int("PrefsVector3Int", Vector3Int.left, HierarchyTest2);
            [SerializeField]
            private PrefsVector4 prefsVector4 = new PrefsVector4("PrefsVector4", new Vector4(0.1f, 0.2f, 0.3f, 0.4f), HierarchyTest2);


            public Test2()
            {
                //this.b3.OnClicked = () => Debug.Log("B3 : OnClicked");
            }
        }
        [Serializable]
        public class Test2Ex1
        {
            public PrefsFloatSlider prefsFloatSlider = new PrefsFloatSlider("PrefsFloatSlider2", 10f, HierarchyTest2Ex1);
            public PrefsIntSlider prefsIntSlider = new PrefsIntSlider("PrefsIntSlider2", 20, HierarchyTest2Ex1);
        }

        public Test2Ex1 test2Ex1 = new Test2Ex1();

        [SerializeField]
        private Test1 test1 = new Test1();
        [SerializeField]
        private Test2 test2 = new Test2();

        [SerializeField]
        private PrefsEnum<TestEnum1> prefsEnum1 = new PrefsEnum<TestEnum1>("PrefsEnum1", TestEnum1.Three, HierarchyTest2Ex2);
        [SerializeField]
        private PrefsEnum<TestEnum2> prefsEnum2 = new PrefsEnum<TestEnum2>("PrefsEnum2", TestEnum2.A, HierarchyTest2Ex2);
#pragma warning restore 0414


        private void Awake()
        {
            this.test1.prefsIntSlider.BottomMargin = 50f;
            this.test1.prefsIntSlider.GuiLabelSufix = RichTextColors.Blue(" ~ ");
            this.test1.prefsIntSlider.GuiLabelPrefix = RichTextColors.Red(" ~ ");
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                this.test1.prefsInt.VisibleGUI = !this.test1.prefsInt.VisibleGUI;
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                this.test2.prefsVector2.Set(this.test2.prefsVector2 + Vector2.one);
            }
        }
    }
}
