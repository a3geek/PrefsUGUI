using System;

namespace XmlStorage
{
    public static partial class Storage
    {
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void Set<T>(string key, T value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.Set(key, value));
        }

        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="type">セットするデータの型情報</param>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void Set<T>(Type type, string key, T value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.Set(type, key, value));
        }

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetFloat(string key, float value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.SetFloat(key, value));
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetInt(string key, int value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.SetInt(key, value));
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetString(string key, string value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.SetString(key, value));
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void SetBool(string key, bool value, string aggregationName = null)
        {
            Action(aggregationName, agg => agg.SetBool(key, value));
        }
    }
}
