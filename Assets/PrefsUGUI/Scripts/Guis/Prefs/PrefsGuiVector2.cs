using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector2 : PrefsGuiVectorCombi<Vector2, Vector2Int>
    {
        protected override int ElementCount
        {
            get { return 2; }
        }


        protected override string GetElement(int index)
        {
            return this.IsDecimalNumber == true ? this.value[index].ToString() : this.valueInt[index].ToString();
        }

        protected override Vector2 Vector4ToVec(Vector4 vec4)
        {
            return new Vector2(vec4.x, vec4.y);
        }

        protected override Vector2Int Vector3IntToVecInt(Vector3Int vec3)
        {
            return new Vector2Int(vec3.x, vec3.y);
        }

        protected override Vector4 VecToVector4()
        {
            return new Vector4(this.value.x, this.value.y, 0f, 0f);
        }

        protected override Vector3Int VecIntToVecotr3Int()
        {
            return new Vector3Int(this.valueInt.x, this.valueInt.y, 0);
        }
    }
}
