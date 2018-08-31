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
        public PrefsVector2 v1 = new PrefsVector2("v1", Vector2.zero, "hoge/fuga/piyo");
        public PrefsVector2 v2 = new PrefsVector2("v2", Vector2.one, "aaa/bbb");
        public PrefsVector2 v3 = new PrefsVector2("v3", new Vector2(2f, 3f), "hoge/ppp/aaa");
        public PrefsVector2 v4 = new PrefsVector2("v4", Vector2.zero, "hoge/fuga");
        public PrefsVector2Int v5 = new PrefsVector2Int("v5");

        public PrefsBool b = new PrefsBool("PrefsBool", true, "Test");
        public PrefsColor c = new PrefsColor("PrefsColor", Color.red, "Test");
        public PrefsColorSlider cs = new PrefsColorSlider("ColorSlider", Color.blue, "Test");
        public PrefsFloat f = new PrefsFloat("PrefsFloat", 1f, "Test");
        public PrefsFloatSlider fs = new PrefsFloatSlider("PrefsFloatSlider", 0f, 10f, 5f, "Test");
        public PrefsInt i = new PrefsInt("PrefsInt", 5, "Test");
        public PrefsIntSlider isl = new PrefsIntSlider("PrefsIntSlider", -10, 10, 5, "Test");

        public PrefsString s = new PrefsString("PrefsString", "localhost", "Test");
        public PrefsVector3 v6 = new PrefsVector3("PrefsVector3", new Vector3(2f, 4f, 6f), "Test");
        public PrefsVector3Int v7 = new PrefsVector3Int("PrefsVector3Int", new Vector3Int(-10, -5, 0), "Test");
        public PrefsVector4 v8 = new PrefsVector4("PrefsVector4", new Vector4(-2f, 0f, 2f, 100f), "Test");


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }
        }
    }
}
