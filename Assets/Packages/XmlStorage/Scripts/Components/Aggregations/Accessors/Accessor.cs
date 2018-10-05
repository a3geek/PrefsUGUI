using System;
using System.Collections.Generic;
using System.Linq;

namespace XmlStorage.Components.Aggregations.Accessors
{
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;

    public abstract class Accessor : Getters
    {
        protected abstract ExDictionary dictionary { get; }


        /// <summary>
        /// データとキーを内部的にセットする
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="value">データ</param>
        /// <param name="type">データの型情報</param>
        protected override void SetValue<T>(string key, T value, Type type = null)
        {
            type = type ?? typeof(T);

            if(this.HasType(type) == false)
            {
                this.dictionary[type] = new Dictionary<string, object>();
            }
            
            this.dictionary[type][key] = value;
        }

        /// <summary>
        /// キーと対応するデータを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <param name="converter">型変換処理</param>
        /// <returns>データ</returns>
        protected override T GetValue<T>(string key, T defaultValue, Type type = null, Func<object, T> converter = null)
        {
            type = type ?? typeof(T);

            return this.HasKey(key, type) ?
                (converter == null ? (T)this.dictionary[type][key] : converter(this.dictionary[type][key])) :
                defaultValue;
        }

        /// <summary>
        /// 型と対応するデータ Listを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <returns>データ</returns>
        protected override T[] GetValues<T>(T[] defaultValue, Type type)
        {
            return this.HasType(type) == true ? dictionary[type].Values.Cast<T>().ToArray() : new T[0];
        }

        /// <summary>
        /// データの型と対応するキーを取得する
        /// </summary>
        /// <param name="type">データの型情報</param>
        /// <returns>データの型と対応するキー</returns>
        public override string[] GetKeys(Type type)
        {
            return this.HasType(type) == true ? this.dictionary[type].Keys.ToArray() : new string[0];
        }


        /// <summary>
        /// データの型情報を取得する
        /// </summary>
        /// <returns>データの型情報</returns>
        public override Type[] GetTypes()
        {
            return dictionary.Keys.ToArray();
        }
        
        /// <summary>
        /// セットした全てのデータを消去する
        /// </summary>
        public void DeleteAll()
        {
            this.dictionary.Clear();
        }

        /// <summary>
        /// キーと一致するデータを全て消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <returns>消去に成功したかどうか</returns>
        public bool DeleteKey(string key)
        {
            var flag = false;

            foreach(var pair in this.dictionary)
            {
                flag |= pair.Value.Remove(key);
            }

            return flag;
        }

        /// <summary>
        /// キーと型に一致するデータを消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        /// <returns>消去に成功したかどうか</returns>
        public bool DeleteKey(string key, Type type)
        {
            return this.HasType(type) == false ? false : this.dictionary[type].Remove(key);
        }

        /// <summary>
        /// キーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <returns>存在するかどうか</returns>
        public bool HasKey(string key)
        {
            foreach(var pair in this.dictionary)
            {
                if(this.HasKey(key, pair.Key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// キーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <returns>存在するかどうか</returns>
        public bool HasKey(string key, Type type)
        {
            return this.HasType(type) && this.dictionary[type].ContainsKey(key);
        }

        /// <summary>
        /// 型がデータとして存在するかどうかを検索する
        /// </summary>
        /// <param name="type">検索する型</param>
        /// <returns>存在するかどうか</returns>
        public bool HasType(Type type)
        {
            return this.dictionary.ContainsKey(type);
        }
    }
}
