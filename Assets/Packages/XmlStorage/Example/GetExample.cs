using System;
using System.Linq;
using UnityEngine;

namespace XmlStorage.Examples
{
    /// <summary>
    /// 値のゲットのテスト
    /// </summary>
    [AddComponentMenu("")]
    public class GetExample : MonoBehaviour
    {
        /// <summary>
        /// 値をゲットしてログに出力する
        /// </summary>
        public void Execute()
        {
            /*
             * true
             * true
             * false
             */
            Debug.Log(Storage.HasAggregation(Storage.DefaultAggregationName));
            Debug.Log(Storage.HasAggregation("Test1"));
            Debug.Log(Storage.HasAggregation("Test2"));
            Debug.Log("");

            /*
             * 10
             * 10
             * 15
             * 20
             * 1.12345
             */
            Debug.Log(Storage.Get(typeof(SetExample), "SetExample", 0));
            Debug.Log(Storage.GetInt("integer1"));
            Debug.Log(Storage.GetInt("integer2"));
            Debug.Log(Storage.GetInt("integer3"));
            Debug.Log(Storage.GetFloat("float"));
            Debug.Log("");
            Debug.Log("");

            /*
             * c:/......
             * Test2.xml
             */
            Storage.ChangeAggregation("Test1");
            Debug.Log("Directory Path : " + Storage.DirectoryPath);
            Debug.Log("File Name : " + Storage.CurrentAggregation.FileName);

            /*
             * XmlStorage.Examples.ExampleController+Test1
             * 1 _ TestString _ 0.1 _ 1.1 _ 10.1 _ 1.0
             * XmlStorage.Examples.ExampleController+Test2
             * (1.0, 2.0)
             * (1.0, 2.0, 3.0)
             * (1.0, 2.0, 3.0, 4.0)
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             */
            var test1 = Storage.Get<ExampleController.Test1>("test1");
            var test2 = Storage.Get<ExampleController.Test2>("test2");
            Debug.Log(test1);
            test1?.Log();

            Debug.Log(test2);
            test2?.Log();

            Debug.Log("");
            Debug.Log(Storage.Get<Vector2>("vec2"));
            Debug.Log(Storage.Get<Vector3>("vec3"));
            Debug.Log(Storage.Get<Quaternion>("qua").eulerAngles);


            Debug.Log("");
            Debug.Log("");
            Storage.DirectoryPath = ExampleController.SecondaryFolder;
            Storage.Load();

            /*
             * c:/......
             * XmlStorage.xml
             */
            Debug.Log("Directory Path : " + Storage.DirectoryPath);
            Debug.Log("File Name : " + Storage.CurrentAggregation.FileName);

            /*
             * true
             * false
             */
            Debug.Log(Storage.HasAggregation(Storage.DefaultAggregationName));
            Debug.Log(Storage.HasAggregation("Test1"));
            Debug.Log("");

            /*
             * 100
             * 10.12345
             * 0
             * ""
             * 0
             * "del_test2"
             */
            Debug.Log(Storage.GetInt("integer"));
            Debug.Log(Storage.GetFloat("float"));
            Debug.Log(Storage.GetInt("del_test1"));
            Debug.Log(Storage.GetString("del_test1"));
            Debug.Log(Storage.GetInt("del_test2"));
            Debug.Log(Storage.GetString("del_test2"));


            Debug.Log("");
            Debug.Log("");
            Storage.DirectoryPath = Storage.DefaultSaveDirectory;
            Storage.Load();

            var ints = Storage.GetInts();
            for(var i = 0; i < ints.Length; i++)
            {
                Debug.Log(ints[i]);
            }
            Debug.Log("");

            var keys = Storage.GetKeys(typeof(int));
            for(var i = 0; i < keys.Length; i++)
            {
                Debug.Log(keys[i]);
            }

            Debug.Log("");

            var types = Storage.GetTypes();
            for(var i = 0; i < types.Length; i++)
            {
                Debug.Log(types[i]);
            }
        }
    }
}
