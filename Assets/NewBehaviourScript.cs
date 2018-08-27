using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public PrefsUGUI.PrefsVector2 v1 = new PrefsUGUI.PrefsVector2("hoge", Vector2.zero, "hoge/fuga/piyo/test", "Hoge");
    public PrefsUGUI.PrefsVector2 v2 = new PrefsUGUI.PrefsVector2("fuga", Vector2.zero, "hoge/fuga");
    public PrefsUGUI.PrefsVector2 v3 = new PrefsUGUI.PrefsVector2("piyo", Vector2.zero, "", "Piyo");
    public PrefsUGUI.PrefsVector2Int v1int = new PrefsUGUI.PrefsVector2Int("iii");

    public PrefsUGUI.PrefsVector2 v5 = new PrefsUGUI.PrefsVector2("pppp", new Vector2(10f, 29f), "piyo/nnnn/");
    
    void Update () {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PrefsUGUI.Prefs.ShowGUI();
        }
	}
}
