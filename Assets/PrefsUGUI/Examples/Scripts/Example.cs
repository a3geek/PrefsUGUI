using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrefsUGUI.Examples
{
    using static GuiHierarchies;

    [AddComponentMenu("")]
    public partial class Example : MonoBehaviour
    {
#pragma warning disable 0414
        private Test1 test1 = new Test1();
        private Test1Ex1 test1Ex1 = new Test1Ex1();
        private Test1Ex2 test1Ex2 = new Test1Ex2();
        private List<(GuiHierarchy hierarchy, PrefsInt prefsInt)> hierarchies = new List<(GuiHierarchy hierarchy, PrefsInt prefsInt)>();

        [SerializeField]
        private Texture2D prefsImage = null;
        [SerializeField]
        private Test0 test0 = new Test0();

        private PrefsRect prefsRect = new PrefsRect("PrefsRect", new Rect(0.25f, 0.5f, 1f, 2f));
#pragma warning restore 0414


        private void Start()
        {
            this.test0.PrefsImageLabel.Image = this.prefsImage;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }

            if(Input.GetKeyDown(KeyCode.V))
            {
                this.prefsRect.VisibleGUI = !this.prefsRect.VisibleGUI;
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                this.prefsRect.Set(new Rect(1f, 2f, 3f, 4f));
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                this.AddHierarchy();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                this.RemoveHierarchy();
            }
        }

        private void AddHierarchy()
        {
            var count = this.hierarchies.Count;
            var parent = this.hierarchies.ElementAtOrDefault(count - 1).hierarchy ?? Test0Gui;

            var hierarchy = new GuiHierarchy("Hierarchy" + count, count, parent);
            var prefsInt = new PrefsInt("PrefsInt", count, hierarchy);
            prefsInt.OnValueChanged += ()
                => Debug.Log("OnValueChanged : " + hierarchy.FullHierarchy + Prefs.HierarchySeparator + prefsInt.Key);

            this.hierarchies.Add((hierarchy, prefsInt));
        }

        private void RemoveHierarchy()
        {
            var (hierarchy, prefsInt) = this.hierarchies.LastOrDefault();
            prefsInt?.Dispose();
            hierarchy?.Dispose();
            this.hierarchies.RemoveAt(this.hierarchies.Count - 1);
        }
    }
}
