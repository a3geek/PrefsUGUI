using System;

namespace XmlStorage.Components.Aggregations.Accessors
{
    public abstract class Setters
    {
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(string key, T value)
            => this.SetValue(key, value, typeof(T));

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="type">セットするデータの型情報</param>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void Set<T>(Type type, string key, T value)
            => this.SetValue(key, value, type);

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetFloat(string key, float value)
            => this.SetValue(key, value, typeof(float));

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetInt(string key, int value)
            => this.SetValue(key, value, typeof(int));

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetString(string key, string value)
            => this.SetValue(key, value, typeof(string));

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public void SetBool(string key, bool value)
            => this.SetValue(key, value, typeof(bool));

        /// <summary>
        /// データとキーを内部的にセットする
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="key">データのキー</param>
        /// <param name="value">データ</param>
        /// <param name="type">データの型情報</param>
        protected abstract void SetValue<T>(string key, T value, Type type = null);
    }
}
