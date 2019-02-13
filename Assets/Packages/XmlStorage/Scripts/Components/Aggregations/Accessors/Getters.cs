using System;
using System.Collections.Generic;

namespace XmlStorage.Components.Aggregations.Accessors
{
    public abstract class Getters : Setters
    {
        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public T Get<T>(string key, T defaultValue = default(T))
            => this.GetValue(key, defaultValue, typeof(T), obj => (T)obj);

        /// <summary>
        /// キーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="type">データの型情報</param>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public T Get<T>(Type type, string key, T defaultValue = default(T))
            => this.GetValue(key, defaultValue, type ?? typeof(T), obj => (T)obj);

        /// <summary>
        /// キーと対応する任意のList型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public List<T> Gets<T>(string key, List<T> defaultValue = default(List<T>))
            => this.GetValue(key, defaultValue, typeof(List<T>), obj => (List<T>)obj);

        /// <summary>
        /// キーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public float GetFloat(string key, float defaultValue = default(float))
            => this.GetValue(key, defaultValue, typeof(float), null);

        /// <summary>
        /// キーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public int GetInt(string key, int defaultValue = default(int))
            => this.GetValue(key, defaultValue, typeof(int), null);

        /// <summary>
        /// キーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public string GetString(string key, string defaultValue = "")
            => this.GetValue(key, defaultValue, typeof(string), null);

        /// <summary>
        /// キーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public bool GetBool(string key, bool defaultValue = default(bool))
            => this.GetValue(key, defaultValue, typeof(bool), null);

        /// <summary>
        /// float型と対応するデータを取得する
        /// </summary>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>型に対応するデータ</returns>
        public float[] GetFloats(float[] defaultValue = default(float[]))
            => this.GetValues(defaultValue, typeof(float));

        /// <summary>
        /// int型と対応するデータを取得する
        /// </summary>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>型に対応するデータ</returns>
        public int[] GetInts(int[] defaultValue = default(int[]))
            => this.GetValues(defaultValue, typeof(int));

        /// <summary>
        /// string型と対応するデータを取得する
        /// </summary>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>型に対応するデータ</returns>
        public string[] GetStrings(string[] defaultValue = default(string[]))
            => this.GetValues(defaultValue, typeof(string));

        /// <summary>
        /// bool型と対応するデータを取得する
        /// </summary>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>型に対応するデータ</returns>
        public bool[] GetBools(bool[] defaultValue = default(bool[]))
            => this.GetValues(defaultValue, typeof(bool));

        /// <summary>
        /// キーと対応するデータを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <param name="converter">型変換処理</param>
        /// <returns>データ</returns>
        protected abstract T GetValue<T>(string key, T defaultValue, Type type = null, Func<object, T> converter = null);

        /// <summary>
        /// 型と対応するデータ Listを取得する
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="type">データの型情報</param>
        /// <param name="defaultValue">データが存在しなかった時の返り値</param>
        /// <returns>データ</returns>
        protected abstract T[] GetValues<T>(T[] defaultValue, Type type);

        /// <summary>
        /// データの型と対応するキーを取得する
        /// </summary>
        /// <param name="type">データの型情報</param>
        /// <returns>データの型と対応するキー</returns>
        public abstract string[] GetKeys(Type type);

        /// <summary>
        /// データの型情報を取得する
        /// </summary>
        /// <returns>データの型情報</returns>
        public abstract Type[] GetTypes();

    }
}
