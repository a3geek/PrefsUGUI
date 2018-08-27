using System;
using System.Reflection;

namespace XmlStorage.Components.Data
{
    /// <summary>
    /// データをファイルに保存する時のデータ形式
    /// </summary>
    [Serializable]
    public sealed class DataElement
    {
        /// <summary>データを取り出す時に使うキー</summary>
        public string Key { get; private set; }
        /// <summary>保存するデータ</summary>
        public object Value { get; private set; }
        /// <summary>データの型のフルネーム</summary>
        public string TypeName { get; private set; }
        /// <summary>データの型(RO)</summary>
        public Type ValueType { get { return this.GetType(this.TypeName); } }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>シリアライズするのにデフォルトコンストラクタが必要</remarks>
        public DataElement() : this(Guid.NewGuid().ToString(), new object(), typeof(object).FullName) {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型</param>
        public DataElement(string key, object value, Type type) : this(key, value, type.FullName) {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型のフルネーム</param>
        public DataElement(string key, object value, string type) { this.Set(key, value, type); }

        /// <summary>
        /// メンバ変数の値を更新する
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型</param>
        public void Set(string key, object value, Type type)
        {
            this.Set(key, value, type.FullName);
        }

        /// <summary>
        /// メンバ変数の値を更新する
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型のフルネーム</param>
        public void Set(string key, object value, string type)
        {
            if(key == null)
            {
                throw new ArgumentNullException("key", "Key cannot be null.");
            }
            else if(key == "")
            {
                throw new ArgumentException("key", "Key cannot be empty.");
            }

            if(value == null)
            {
                throw new ArgumentNullException("value", "Value cannot be null.");
            }

            if(type == null)
            {
                throw new ArgumentNullException("type", "Type cannnot be null.");
            }
            else if(type == "")
            {
                throw new ArgumentException("type", "Type cannot be empty.");
            }

            this.Key = key;
            this.Value = value;
            this.TypeName = type;
        }

        // https://answers.unity.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        /// <summary>
        /// 型名からType型に変換する
        /// </summary>
        /// <param name="typeName">型名</param>
        /// <returns>変換した型</returns>
        private Type GetType(string typeName)
        {
            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(typeName);

            // If it worked, then we're done here
            if(type != null)
            {
                return type;
            }

            // If the TypeName is a full name, then we can try loading the defining assembly directly
            if(typeName.Contains("."))
            {
                // Get the name of the assembly (Assumption is that we are using 
                // fully-qualified type names)
                var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

                // Attempt to load the indicated Assembly
                var assembly = Assembly.Load(assemblyName);
                if(assembly == null)
                {
                    return null;
                }

                // Ask that assembly to return the proper Type
                type = assembly.GetType(typeName);
                if(type != null)
                {
                    return type;
                }
            }

            // If we still haven't found the proper type, we can enumerate all of the 
            // loaded assemblies and see if any of them define the type
            var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            foreach(var assemblyName in referencedAssemblies)
            {
                // Load the referenced assembly
                var assembly = Assembly.Load(assemblyName);

                if(assembly != null)
                {
                    // See if that assembly defines the named type
                    type = assembly.GetType(typeName);

                    if(type != null)
                    {
                        return type;
                    }
                }
            }

            // The type just couldn't be found...
            return null;
        }
    }
}
