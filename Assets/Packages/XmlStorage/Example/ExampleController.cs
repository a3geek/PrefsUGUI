using UnityEngine;
using System.Collections.Generic;
using System;

namespace XmlStorage.Examples
{
    /// <summary>
    /// サンプル用コントローラー
    /// </summary>
    [AddComponentMenu("")]
    public class ExampleController : MonoBehaviour
    {
        /// <summary>
        /// クラスのインスタンス保存テスト用
        /// </summary>
        [Serializable]
        public class Test1
        {
            /// <summary><see cref="int"/>型の保存テスト</summary>
            public int integer = 1;
            /// <summary>文字列保存テスト</summary>
            public string str = "TestString";
            /// <summary>リスト保存テスト</summary>
            public List<float> list1 = new List<float>() {
                0.1f, 1.1f, 10.1f
            };
        }

        /// <summary>
        /// クラスのインスタンス保存テスト用
        /// </summary>
        public class Test2
        {
            /// <summary><see cref="Vector2"/>型の保存テスト</summary>
            public Vector2 vec2 = new Vector2(1f, 2f);
            /// <summary><see cref="Vector3"/>型の保存テスト</summary>
            public Vector3 vec3 = new Vector3(1f, 2f, 3f);
        }

        /// <summary>
        /// 動作設定用enum
        /// </summary>
        public enum ExampleMode
        {
            Set = 1, Get
        };

        /// <summary>
        /// 値をセットするかゲットするかの動作を選択
        /// </summary>
        public ExampleMode mode = ExampleMode.Set;
        /// <summary>データセット用コンポーネント</summary>
        public SetExample set = null;
        /// <summary>データゲット用コンポーネント</summary>
        public GetExample get = null;


        void Awake()
        {
            if(this.set == null)
            {
                this.set = GetComponentInChildren<SetExample>();
            }
            if(this.get == null)
            {
                this.get = GetComponentInChildren<GetExample>();
            }
        }

        void Start()
        {
            if(this.mode == ExampleMode.Set)
            {
                this.set.Execute();
            }
            else
            {
                this.get.Execute();
            }
        }
    }
}
