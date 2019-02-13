using UnityEngine;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector3 : PrefsGuiVectorCombination<Vector3, Vector3Int>
    {
        protected override int ElementCount => 3;


        protected override string GetElement(int index)
            => this.IsDecimalNumber == true ? this.value[index].ToString() : this.valueInt[index].ToString();

        protected override Vector3 Vector4ToVec(Vector4 vec4) => new Vector3(vec4.x, vec4.y, vec4.z);

        protected override Vector3Int Vector3IntToVecInt(Vector3Int vec3) => vec3;

        protected override Vector4 VecToVector4() => new Vector4(this.value.x, this.value.y, this.value.z, 0f);

        protected override Vector3Int VecIntToVecotr3Int() => this.valueInt;
    }
}
