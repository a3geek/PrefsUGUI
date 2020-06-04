using System;

namespace XmlStorage
{
    using Systems.Aggregations;

    public static partial class Storage
    {
        /// <summary>
        /// 別の集団を選択する
        /// <paramref name="aggregationName"/>集団が存在しない場合は新しく生成する
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        public static void ChangeAggregation(string aggregationName)
        {
            if(HasAggregation(aggregationName) == false)
            {
                Aggregations.Add(aggregationName, new Aggregation(null, aggregationName));
            }

            CurrentAggregationName = aggregationName;
        }

        /// <summary>
        /// 集団を消去する
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteAggregation(string aggregationName)
            => aggregationName != DefaultAggregationName && Aggregations.Remove(aggregationName);

        /// <summary>
        /// 集団が存在するかどうか
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasAggregation(string aggregationName)
            => string.IsNullOrEmpty(aggregationName) != true && Aggregations.ContainsKey(aggregationName);

        /// <summary>
        /// セットした全てのデータを消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void DeleteAll(string aggregationName = null)
            => Action(aggregationName, agg => agg.DeleteAll());

        /// <summary>
        /// キーと一致するデータを全て消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteKey(string key, string aggregationName = null)
            => Func(aggregationName, agg => agg.DeleteKey(key));

        /// <summary>
        /// キーと型に一致するデータを消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteKey(string key, Type type, string aggregationName = null)
            => Func(aggregationName, agg => agg.DeleteKey(key, type));

        /// <summary>
        /// キーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasKey(string key, string aggregationName = null)
            => Func(aggregationName, agg => agg.HasKey(key));

        /// <summary>
        /// キーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasKey(string key, Type type, string aggregationName = null)
            => Func(aggregationName, agg => agg.HasKey(key, type));
    }
}
