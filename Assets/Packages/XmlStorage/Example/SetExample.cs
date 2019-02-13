using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace XmlStorage.Examples
{
    /// <summary>
    /// 値のセットと保存のテスト
    /// </summary>
    [AddComponentMenu("")]
    public class SetExample : MonoBehaviour
    {
        /// <summary>インスタンスの保存テスト</summary>
        [SerializeField]
        private ExampleController.Test1 test1 = new ExampleController.Test1();
        /// <summary>インスタンスの保存テスト</summary>
        private ExampleController.Test2 test2 = new ExampleController.Test2();
        /// <summary><see cref="Vector2"/>の保存テスト</summary>
        private Vector2 vec2 = new Vector2(0.1f, 0.2f);
        /// <summary><see cref="Vector3"/>の保存テスト</summary>
        private Vector3 vec3 = new Vector3(1f, 2f, 3f);
        /// <summary><see cref="Quaternion"/>の保存テスト</summary>
        private Quaternion quaternion = Quaternion.Euler(new Vector3(10f, 20f, 30f));


        /// <summary>
        /// 値をセットして保存する
        /// </summary>
        public void Execute()
        {
            this.test1.ff = 10f;
            this.test2.vec4 *= 10f;

            Storage.Set(typeof(SetExample), "SetExample", 10);
            Storage.SetInt("integer1", 10);
            Storage.SetInt("integer2", 15);
            Storage.SetInt("integer3", 20);
            Storage.SetFloat("float", 1.12345f);

            Storage.ChangeAggregation("Test1");
            Storage.CurrentAggregation.FileName = "Test1";
            Storage.Set("test1", this.test1);
            Storage.Set("test2", this.test2);
            Storage.Set("vec2", this.vec2);
            Storage.Set("vec3", this.vec3);
            Storage.Set("qua", this.quaternion);

            Storage.Save();

            // Save to oher folder.
            Storage.DirectoryPath = ExampleController.SecondaryFolder;   
            Storage.Load();

            // meaninglessness.
            Storage.ChangeAggregation(Storage.DefaultAggregationName);

            Storage.SetInt("integer", 100);
            Storage.SetFloat("float", 10.12345f);

            Storage.SetInt("del_test1", 2);
            Storage.SetString("del_test1", "del_test1");
            Storage.DeleteKey("del_test1");

            Storage.SetInt("del_test2", 5);
            Storage.SetString("del_test2", "del_test2");
            Storage.DeleteKey("del_test2", typeof(int));

            Storage.Save();
            Debug.Log("Finish");
        }
    }
}
