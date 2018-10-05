using UnityEngine;
using System.Linq;
using System;

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
            Debug.Log(Storage.HasAggregation("Default"));
            Debug.Log(Storage.HasAggregation("Test1"));
            Debug.Log(Storage.HasAggregation("Test2"));
            Debug.Log("");

            // 10
            Debug.Log(Storage.Get(typeof(SetExample), "SetExample", 0));
            Debug.Log("");

            /*
             * 1
             * 1.111
             * XmlStorage.Examples.ExampleController+Test
             * "TestString"
             * 0.1f
             * 10.1f
             * 
             * 0
             * ""
             * 
             * 0
             * "del_tes2"
             * 
             * "lab-interactive@team-lab.com"
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             * 
             * 111
             * 2018
             * 10
             * 
             * "del_test2"
             * "address"
             */
            Debug.Log("Default Aggregatpion");
            Debug.Log("Directory Path : " + Storage.DirectoryPath);
            Debug.Log("File Name : " + Storage.FileName);
            this.GetData();

            if(Application.isEditor)
            {
                /*
                 * 11
                 * 1.111
                 * XmlStorage.Examples.ExampleController+Test
                 * "TestString"
                 * 0.1f
                 * 10.1f
                 * 
                 * 0
                 * ""
                 * 
                 * 0
                 * "del_tes2"
                 * 
                 * "lab-interactive@team-lab.com"
                 * 
                 * (0.1, 0.2)
                 * (1.0, 2.0, 3.0)
                 * (10.0, 20.0, 30.0)
                 * 
                 * 111
                 * 2018
                 * 10
                 * 
                 * "del_test2"
                 * "address"
                 */
                Debug.Log("Test1 Aggregation");
                Storage.ChangeAggregation("Test1");
                Debug.Log("Directory Path : " + Storage.DirectoryPath);
                Debug.Log("File Name : " + Storage.FileName);
                this.GetData();
            }

            /*
             * 111
             * 1.111
             * XmlStorage.Examples.ExampleController+Test
             * "TestString"
             * 0.1f
             * 10.1f
             * 
             * 0
             * ""
             * 
             * 0
             * "del_tes2"
             * 
             * "lab-interactive@team-lab.com"
             * 
             * (0.1, 0.2)
             * (1.0, 2.0, 3.0)
             * (10.0, 20.0, 30.0)
             * 
             * 111
             * 2018
             * 10
             * 
             * "del_test2"
             * "address"
             */
            Debug.Log("Test2 Aggregation");
            Storage.ChangeAggregation("Test2");
            Debug.Log("Directory Path : " + Storage.DirectoryPath);
            Debug.Log("File Name : " + Storage.FileName);
            this.GetData();
        }

        /// <summary>
        /// <see cref="Storage"/>から値を取得してログに出力する
        /// </summary>
        private void GetData()
        {
            Debug.Log(Storage.GetInt("integer", 0));
            Debug.Log(Storage.GetFloat("float", 0f));

            Debug.Log(Storage.Get(typeof(object), "obj_int", 0));
            Debug.Log(Storage.Get<object>("obj_int"));
            Debug.Log(Storage.Get(typeof(int), "obj_int", 0));
            Debug.Log(Storage.Get<int>("obj_int"));

            Debug.Log("");

            Debug.Log(Storage.Get<ExampleController.Test1>("Test1Class"));
            Debug.Log(Storage.Get<ExampleController.Test1>("Test1Class").str);
            Debug.Log(Storage.Get<ExampleController.Test1>("Test1Class").list1.First());
            Debug.Log(Storage.Get<ExampleController.Test1>("Test1Class").list1.Last());

            Debug.Log(Storage.Get<ExampleController.Test2>("Test2Class", null));
            Debug.Log(Storage.Get<ExampleController.Test2>("Test2Class", null).vec2);
            Debug.Log(Storage.Get<ExampleController.Test2>("Test2Class", null).vec3);

            Debug.Log("");

            Debug.Log(Storage.GetInt("del_tes1"));
            Debug.Log(Storage.GetString("del_tes1"));

            Debug.Log("");

            Debug.Log(Storage.GetInt("del_tes2"));
            Debug.Log(Storage.GetString("del_tes2"));

            Debug.Log("");

            Debug.Log(Storage.Get<string>("address"));

            Debug.Log("");

            Debug.Log(Storage.Get("vec2", Vector2.zero));
            Debug.Log(Storage.Get("vec3", Vector3.zero));
            Debug.Log(Storage.Get("qua", Quaternion.identity).eulerAngles);

            Debug.Log("");

            // List Example
            int[] intArr = Storage.GetInts();
            for(var i=0;i< intArr.Length; i++)
            {
                Debug.Log(intArr[i]);
            }

            Debug.Log("");

            string[] string_keys = Storage.GetKeys(typeof(string));
            for(var i=0; i< string_keys.Length; i++)
            {
                Debug.Log(string_keys[i]);
            }

            Debug.Log("");

            Type[] types = Storage.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                Debug.Log(types[i]);
            }

            Debug.Log("");
        }
    }
}
