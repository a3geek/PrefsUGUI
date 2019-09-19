using System.Collections.Generic;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    using static Structs;

    [AddComponentMenu("")]
    public partial class Example : MonoBehaviour
    {
#pragma warning disable 0414
        public Test2Ex1 Test2Ex = new Test2Ex1();

        [SerializeField]
        private Test1 test1 = new Test1();
        [SerializeField]
        private Test2 test2 = new Test2();
        [SerializeField]
        private Test3 test3 = new Test3();

        [SerializeField]
        private PrefsEnum<TestEnum1> prefsEnum1 = new PrefsEnum<TestEnum1>("PrefsEnum1", TestEnum1.Three, HierarchyTest2Ex2);
        [SerializeField]
        private PrefsEnum<TestEnum2> prefsEnum2 = new PrefsEnum<TestEnum2>("PrefsEnum2", TestEnum2.A, HierarchyTest2Ex2);

        private PrefsRect prefsRect = new PrefsRect("PrefsRect", new Rect(0.25f, 0.5f, 1f, 2f));
#pragma warning restore 0414

        private List<GuiHierarchy> Hierarchies = null;
        private List<PrefsInt> prefsInts = null;


        public void AddHierarchy()
        {
            var hierarchy = new GuiHierarchy(
                "Hierarchy" + this.Hierarchies.Count, this.Hierarchies.Count, HierarchyTest2Ex1
            );
            var prefsInt = new PrefsInt(
                "PrefsInt" + this.prefsInts.Count, this.prefsInts.Count, hierarchy
            );

            this.Hierarchies.Add(hierarchy);
            this.prefsInts.Add(prefsInt);
        }

        public void RemoveHierarchy()
        {
            var idx = this.Hierarchies.Count - 1;
            if(idx < 0)
            {
                return;
            }

            this.prefsInts[idx].Dispose();
            this.prefsInts.RemoveAt(idx);

            this.Hierarchies[idx].Dispose();
            this.Hierarchies.RemoveAt(idx);
        }


        private void Awake()
        {
            this.test2.PrefsString.TopMargin = 50f;
            this.test2.PrefsString.BottomMargin = 50f;

            this.test2.PrefsButton3.TopMargin = 30f;
            this.test2.PrefsButton3.BottomMargin = 25f;

            this.test1.PrefsIntSlider.BottomMargin = 50f;
            this.test1.PrefsIntSlider.GuiLabelSufix = RichTextColors.Blue(" ~ ");
            this.test1.PrefsIntSlider.GuiLabelPrefix = RichTextColors.Red(" ~ ");

            this.test3.PrefsLabel1.TopMargin = 20f;
            this.test3.PrefsLabel1.BottomMargin = 20f;

            this.prefsRect.TopMargin = 15f;

            this.Hierarchies = new List<GuiHierarchy>();
            this.prefsInts = new List<PrefsInt>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                this.test1.PrefsInt.VisibleGUI = !this.test1.PrefsInt.VisibleGUI;
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                this.test2.PrefsVector2.Set(this.test2.PrefsVector2 + Vector2.one);
            }

            if(Input.GetKeyDown(KeyCode.Y))
            {
                this.AddHierarchy();
            }
            if(Input.GetKeyDown(KeyCode.U))
            {
                this.RemoveHierarchy();
            }
        }
    }
}
