using System;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    using static GuiHierarchies;

    public partial class Example : MonoBehaviour
    {
#pragma warning disable 0414
        [Serializable]
        public class Test0
        {
            public PrefsBool PrefsBool = new PrefsBool("PrefsBool", true, Test0Gui, "Prefs" + RichTextColors.Red("Bool"),
                sortOrder: 1
            );
            public PrefsEnum<TestEnum> PrefsEnum = new PrefsEnum<TestEnum>("PrefsEnum", TestEnum.Four, Test0Gui,
                sortOrder: 1
            );
            public PrefsImageLabel PrefsImageLabel = new PrefsImageLabel("PrefsImageLabel", "TEXT", Test0Gui,
                sortOrder: 1
            );
            public PrefsLabel PrefsLabel = new PrefsLabel("PrefsLabel", "TEXT", Test0Gui,
                sortOrder: 1
            );

            public PrefsInt PrefsInt = new PrefsInt("PrefsInt", 10, Test0Gui,
                sortOrder: 0
            );
            public PrefsIntSlider PrefsIntSlider = new PrefsIntSlider("PrefsIntSlider", -20, 20, 10, Test0Gui,
                sortOrder: 0
            );
            public PrefsFloat PrefsFloat = new PrefsFloat("PrefsFloat", 0.01f, Test0Gui,
                sortOrder: 0
            );
            public PrefsFloatSlider PrefsFloatSlider = new PrefsFloatSlider("PrefsFloatSlider", -1f, 1f, 0.01f, Test0Gui,
                sortOrder: 0, onCreatedGui: prefs => prefs.BottomMargin = 25f
            );

            [SerializeField]
            private PrefsString prefsString = new PrefsString("PrefsString", "String", Test0Gui,
                sortOrder: 2, onCreatedGui: prefs => prefs.TopMargin = 25f
            );
        }

        private class Test1
        {
            public PrefsRect PrefsRect = new PrefsRect("PrefsRect", new Rect(1f, 2f, 3f, 4f), Test1Gui,
                sortOrder: 1, onCreatedGui: prefs => prefs.TopMargin = 25f
            );

            public PrefsColor PrefsColor = new PrefsColor("PrefsColor", Color.red, Test1Gui,
                sortOrder: 0, onCreatedGui: prefs => prefs.TopMargin = 25f
            );
            public PrefsColorSlider PrefsColorSlider = new PrefsColorSlider("PrefsColorSlider", Color.blue, Test1Gui,
                sortOrder: 0
            );

            public PrefsVector2 PrefsVector2 = new PrefsVector2("PrefsVector2", new Vector2(1f, 2f), Test1Gui,
                sortOrder: 2
            );
            public PrefsVector2Int PrefsVector2Int = new PrefsVector2Int("PrefsVector2Int", new Vector2Int(10, 20), Test1Gui,
                sortOrder: 2
            );
        }

        private class Test1Ex1
        {
            public PrefsVector3 PrefsVector3 = new PrefsVector3("PrefsVector3", new Vector3(0.1f, 0.2f, 0.3f), Test1Ex1Gui);
            public PrefsVector3Int PrefsVector3Int = new PrefsVector3Int("PrefsVector3Int", new Vector3Int(10, 20, 30), Test1Ex1Gui);
            public PrefsVector4 PrefsVector4 = new PrefsVector4("PrefsVector4", new Vector4(-0.1f, -0.2f, 0.1f, 0.2f), Test1Ex1Gui);
        }

        private class Test1Ex2
        {
            private static Test1Ex2 Instance = null;

            public PrefsRect PrefsRect = new PrefsRect("PrefsRect", new Rect(-1f, -2f, -3f, -4f), Test1Ex2Gui);
            public PrefsButton PrefsButton1 = new PrefsButton("PrefsButton1", OnButton1Clicked1, Test1Ex2Gui);
            public PrefsButton PrefsButton2 = new PrefsButton("PrefsButton2", null, Test1Ex2Gui);
            public PrefsButton PrefsButton3 = new PrefsButton("PrefsButton3", null, Test1Ex2Gui);

            public PrefsLabel PrefsLabel = new PrefsLabel("ButtonAction", "", Test1Ex2Gui,
                onCreatedGui: prefs => prefs.TopMargin = 25f
            );

            private bool toggle = true;


            public Test1Ex2()
            {
                Instance = this;

                this.PrefsButton2.OnClicked += () => this.PrefsLabel.Set(nameof(this.PrefsButton2) + " Clicked");
                this.PrefsButton1.OnValueChanged += () => Debug.Log("OnValueChanged : " + nameof(this.PrefsButton1));
                this.PrefsButton3.OnClicked += () =>
                {
                    this.toggle = !this.toggle;
                    if(toggle == true)
                    {
                        this.PrefsButton1.Set(OnButton1Clicked1);
                    }
                    else
                    {
                        this.PrefsButton1.Set(OnButton1Clicked2);
                    }
                };
                this.PrefsButton3.OnEditedInGui += () => Debug.Log("OnEditedInGui : " + nameof(this.PrefsButton3));
            }

            private static void OnButton1Clicked1()
                => Instance.PrefsLabel.Set(nameof(Instance.PrefsButton1) + " Clicked 1");

            private static void OnButton1Clicked2()
                => Instance.PrefsLabel.Set(nameof(Instance.PrefsButton1) + " Clicked 2");
        }

        private class Test2
        {

        }
#pragma warning restore 0414
    }
}
