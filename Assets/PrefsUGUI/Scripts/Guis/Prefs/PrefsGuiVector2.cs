using UnityEngine;

namespace PrefsUGUI.Guis.Prefs
{
    [AddComponentMenu("")]
    public class PrefsGuiVector2 : PrefsGuiVectorCombination<Vector2, Vector2Int>
    {
        protected override int ElementCount => 2;


        protected override string GetElement(int index)
            => this.IsDecimalNumber == true ? this.value[index].ToString() : this.valueInt[index].ToString();

        protected override Vector2 Vector4ToVec(Vector4 vec4) => new Vector2(vec4.x, vec4.y);

        protected override Vector2Int Vector3IntToVecInt(Vector3Int vec3) => new Vector2Int(vec3.x, vec3.y);

        protected override Vector4 VecToVector4() => new Vector4(this.value.x, this.value.y, 0f, 0f);

        protected override Vector3Int VecIntToVecotr3Int() => new Vector3Int(this.valueInt.x, this.valueInt.y, 0);
    }
}
