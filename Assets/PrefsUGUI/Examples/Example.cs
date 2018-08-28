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


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Prefs.ShowGUI();
            }
        }
    }
}
