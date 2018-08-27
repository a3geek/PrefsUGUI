using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector3 : PrefsGuiVectorCombi<Vector3, Vector3Int>
    {
        protected override int ElementCount
        {
            get { return 3; }
        }


        protected override string GetElement(int index)
        {
            return this.IsDecimalNumber == true ? this.value[index].ToString() : this.valueInt[index].ToString();
        }

        protected override Vector3 Vector4ToVec(Vector4 vec4)
        {
            return new Vector3(vec4.x, vec4.y, vec4.z);
        }

        protected override Vector3Int Vector3IntToVecInt(Vector3Int vec3)
        {
            return vec3;
        }

        protected override Vector4 VecToVector4()
        {
            return new Vector4(this.value.x, this.value.y, this.value.z, 0f);
        }

        protected override Vector3Int VecIntToVecotr3Int()
        {
            return this.valueInt;
        }
    }
}
