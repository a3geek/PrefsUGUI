using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
            this.SetData(1);

            if(Application.isEditor)
            {
                Storage.ChangeAggregation("Test1");

                Storage.DirectoryPath =
                    SceneManager.GetActiveScene().name == "XmlStorage" ?
                    Path.GetDirectoryName(SceneManager.GetActiveScene().path) + Path.DirectorySeparatorChar + "SaveFiles" :
                    Application.persistentDataPath;

                Storage.FileName = "XmlStorageExample";
                this.SetData(11);
            }

            Storage.ChangeAggregation("Test2");
            Storage.FileName = "Test2";
            this.SetData(111);
            
            Storage.Save();
            Debug.Log("Finish");
        }

        /// <summary>
        /// <see cref="Storage"/>に値をセットする
        /// </summary>
        /// <param name="value"><see cref="int"/>型の保存テスト</param>
        private void SetData(int value)
        {
            Storage.SetInt("integer", value);
            Storage.SetFloat("float", 1.111f);

            Storage.Set(typeof(object), "obj_int", 10);

            Storage.Set("Test1Class", this.test1);
            Storage.Set("Test2Class", this.test2);

            Storage.SetInt("del_tes1", 2);
            Storage.SetString("del_tes1", "del_tes1");
            Storage.DeleteKey("del_tes1");

            Storage.SetInt("del_tes2", 5);
            Storage.SetString("del_tes2", "del_tes2");
            Storage.DeleteKey("del_tes2", typeof(int));

            var address = "lab-interactive@team-lab.com";
            Storage.SetString("address", address);

            Storage.Set("vec2", this.vec2);
            Storage.Set("vec3", this.vec3);
            Storage.Set("qua", this.quaternion);
        }
    }
}
