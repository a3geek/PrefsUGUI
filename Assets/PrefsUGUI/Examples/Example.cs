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
        [Serializable]
        public enum Test1
        {
            One = 1, Two, Three
        }

        [Serializable]
        private enum Test2
        {
            A = 0, B, C
        }

#pragma warning disable 414
        [SerializeField]
        private PrefsVector2 v1 = new PrefsVector2("v1", Vector2.zero, "hoge/fuga/piyo");
        [SerializeField]
        private PrefsVector2 v2 = new PrefsVector2("v2", Vector2.one, "aaa/bbb");
        [SerializeField]
        private PrefsVector2 v3 = new PrefsVector2("v3", new Vector2(2f, 3f), "hoge/ppp/aaa");
        [SerializeField]
        private PrefsVector2 v4 = new PrefsVector2("v4", Vector2.zero, "hoge/fuga");
        [SerializeField]
        private PrefsVector2Int v5 = new PrefsVector2Int("v5");

        [SerializeField]
        private PrefsBool b = new PrefsBool("PrefsBool", true, "Test");
        [SerializeField]
        private PrefsColor c = new PrefsColor("PrefsColor", Color.red, "Test");
        [SerializeField]
        private PrefsColorSlider cs = new PrefsColorSlider("ColorSlider", Color.blue, "Test");
        [SerializeField]
        private PrefsFloat f = new PrefsFloat("PrefsFloat", 1f, "Test");
        [SerializeField]
        private PrefsFloatSlider fs = new PrefsFloatSlider("PrefsFloatSlider", 0f, 10f, 5f, "Test");
        [SerializeField]
        private PrefsInt i = new PrefsInt("PrefsInt", 5, "Test");
        [SerializeField]
        private PrefsIntSlider isl = new PrefsIntSlider("PrefsIntSlider", -10, 10, 5, "Test");
        [SerializeField]
        public PrefsEnum<Test1> e1 = new PrefsEnum<Test1>("PrefsEnum1", Test1.Two, "Test");
        
        private PrefsEnum<Test2> e2 = new PrefsEnum<Test2>("PrefsEnum2");
        private PrefsString s = new PrefsString("PrefsString", "localhost", "Test");
        private PrefsVector3 v6 = new PrefsVector3("PrefsVector3", new Vector3(2f, 4f, 6f), "Test");
        private PrefsVector3Int v7 = new PrefsVector3Int("PrefsVector3Int", new Vector3Int(-10, -5, 0), "Test");
        private PrefsVector4 v8 = new PrefsVector4("PrefsVector4", new Vector4(-2f, 0f, 2f, 100f), "Test");
#pragma warning restore 414

        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }
        }
    }
}
