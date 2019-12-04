using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PrefsUGUI.Examples
{
    using static Structs;

    public partial class Example : MonoBehaviour
    {
#pragma warning disable 0414
        [Serializable]
        private class Test1
        {
            public PrefsBool PrefsBool = new PrefsBool("PrefsBool", true, HierarchyTest1, "Prefs " + RichTextColors.Red("Bool"));
            public PrefsColor PrefsColor = new PrefsColor("PrefsColor", Color.red, HierarchyTest1);
            public PrefsColorSlider PrefsColorSlider = new PrefsColorSlider("PrefsColorSlider", Color.blue, HierarchyTest1);
            public PrefsFloat PrefsFloat = new PrefsFloat("PrefsFloat", 0.12345f, HierarchyTest1);
            public PrefsFloatSlider PrefsFloatSlider = new PrefsFloatSlider("PrefsFloatSlider", 0f, 10f, 5f, HierarchyTest1);
            public PrefsInt PrefsInt = new PrefsInt("PrefsInt", 3, HierarchyTest1);
            public PrefsIntSlider PrefsIntSlider = new PrefsIntSlider("PrefsIntSlider", 0, 10, 2, HierarchyTest1);
            public PrefsButton PrefsButton1 = new PrefsButton("Click1", null, HierarchyTest1);

            [SerializeField]
            private PrefsButton PrefsButton2 = new PrefsButton("SwitchClick1", null, HierarchyTest1);


            public Test1()
            {
                this.PrefsButton1.OnClicked = this.Click1;
                this.PrefsButton1.OnValueChanged += () => Debug.Log(nameof(this.PrefsButton1) + " : OnValueChanged");

                var counter = 0;
                this.PrefsButton2.OnClicked += () => Debug.Log(nameof(this.PrefsButton2) + " : Clicked");
                this.PrefsButton2.OnClicked += ()
                    => this.PrefsButton1.OnClicked = ++counter % 2 == 0 ? (UnityAction)this.Click1 : this.Click2;
            }

            private void Click1()
                => Debug.Log(nameof(this.PrefsButton1) + " : Click1");

            private void Click2()
                => Debug.Log(nameof(this.PrefsButton1) + " : Click2");
        }

        [Serializable]
        private class Test2
        {
            public IReadOnlyPrefs<string> ReadOnlyPrefsString => this.PrefsString;

            public PrefsString PrefsString = new PrefsString("PrefsString", "Example", HierarchyTest2);
            public PrefsVector2 PrefsVector2 = new PrefsVector2("PrefsVector2", Vector2.one, HierarchyTest2);
            public PrefsVector2Int PrefsVector2Int = new PrefsVector2Int("PrefsVector2Int", Vector2Int.right, HierarchyTest2);
            public PrefsButton PrefsButton3
                = new PrefsButton("PrefsButton3", () => Debug.Log(nameof(PrefsButton3) + " : Clicked"), HierarchyTest2);

            [SerializeField]
            private PrefsVector3 prefsVector3 = new PrefsVector3("PrefsVector3", Vector3.down, HierarchyTest2);
            [SerializeField]
            private PrefsVector3Int prefsVector3Int = new PrefsVector3Int("PrefsVector3Int", Vector3Int.left, HierarchyTest2);
            [SerializeField]
            private PrefsVector4 prefsVector4 = new PrefsVector4("PrefsVector4", new Vector4(0.1f, 0.2f, 0.3f, 0.4f), HierarchyTest2);


            public Test2()
            {
                this.PrefsButton3.OnClicked += () => Debug.Log(nameof(this.PrefsButton3) + " : Clicked2");
            }
        }

        [Serializable]
        public class Test2Ex1
        {
            public PrefsFloatSlider PrefsFloatSlider = new PrefsFloatSlider("PrefsFloatSlider2", 10f, HierarchyTest2Ex1);
            public PrefsIntSlider PrefsIntSlider = new PrefsIntSlider("PrefsIntSlider2", 20, HierarchyTest2Ex1);

            private List<GuiHierarchy> Hierarchies = new List<GuiHierarchy>();
            private List<PrefsInt> prefsInts = new List<PrefsInt>();


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
        }

        [Serializable]
        private class Test3
        {
            public PrefsIntSlider PrefsIntSliderUnsave = new PrefsIntSlider("PrefsIntSliderUnsave", 0, 10, 5, HierarchyTest3);
            public PrefsLabel PrefsLabel1 = new PrefsLabel("PrefsLabel1", "Test", HierarchyTest3);
            public PrefsButton PrefsButton = new PrefsButton("ChangePrefsLabel1", null, HierarchyTest3);

            [SerializeField]
            private PrefsLabel prefsLabel = new PrefsLabel("PrefsLabel2", "TimeExample", HierarchyTest3);


            public Test3()
            {
                this.PrefsIntSliderUnsave.Unsave = true;
                this.PrefsButton.OnClicked += () => this.prefsLabel.Set(DateTime.Now.ToString());
            }
        }
#pragma warning restore 0414
    }
}
