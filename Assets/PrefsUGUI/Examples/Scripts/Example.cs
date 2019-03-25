using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PrefsUGUI.Examples
{
    using static Structs;

    using Random = UnityEngine.Random;

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
            public PrefsButton prefsButton1 = new PrefsButton("Click1", null, HierarchyTest1);
            public PrefsButton prefsButton2 = new PrefsButton("SwitchClick1", null, HierarchyTest1);


            public Test1()
            {
                this.prefsButton1.OnClicked = this.Click1;
                this.prefsButton1.OnValueChanged += () => Debug.Log(nameof(this.prefsButton1) + " : OnValueChanged");

                var counter = 0;
                this.prefsButton2.OnClicked += () => Debug.Log(nameof(this.prefsButton2) + " : Clicked");
                this.prefsButton2.OnClicked += ()
                    => this.prefsButton1.OnClicked = ++counter % 2 == 0 ? (UnityAction)this.Click1 : this.Click2;
            }

            private void Click1()
                => Debug.Log(nameof(this.prefsButton1) + " : Click1");

            private void Click2()
                => Debug.Log(nameof(this.prefsButton1) + " : Click2");
        }
        [Serializable]
        private class Test2
        {
            public PrefsString prefsString = new PrefsString("PrefsString", "Example", HierarchyTest2);
            public PrefsVector2 prefsVector2 = new PrefsVector2("PrefsVector2", Vector2.one, HierarchyTest2);
            public PrefsVector2Int prefsVector2Int = new PrefsVector2Int("PrefsVector2Int", Vector2Int.right, HierarchyTest2);
            private PrefsButton prefsButton3
                = new PrefsButton("PrefsButton3", () => Debug.Log(nameof(prefsButton3) + " : Clicked"), HierarchyTest2);

            [SerializeField]
            private PrefsVector3 prefsVector3 = new PrefsVector3("PrefsVector3", Vector3.down, HierarchyTest2);
            [SerializeField]
            private PrefsVector3Int prefsVector3Int = new PrefsVector3Int("PrefsVector3Int", Vector3Int.left, HierarchyTest2);
            [SerializeField]
            private PrefsVector4 prefsVector4 = new PrefsVector4("PrefsVector4", new Vector4(0.1f, 0.2f, 0.3f, 0.4f), HierarchyTest2);


            public Test2()
            {
                this.prefsButton3.OnClicked += () => Debug.Log(nameof(this.prefsButton3) + " : Clicked2");
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
