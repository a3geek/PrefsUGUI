using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlStorage.Components.Utilities
{
    using Data;
    using Aggregations;

    using Elements = List<Data.DataElement>;
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;
    using SerializeType = List<Data.DataSet>;

    public static class Converter
    {
        /// <summary>データをXMLにシリアライズするためのシリアライザーインスタンス</summary>
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(SerializeType));

        /// <summary>
        /// 集団群をファイル保存用の情報群に変換する
        /// </summary>
        /// <param name="aggregations">集団群</param>
        /// <returns>保存用に変換した情報群</returns>
        public static Dictionary<string, SerializeType> AggregationsToDictionary(Dictionary<string, Aggregation> aggregations, Encoding encode)
        {
            var dic = new Dictionary<string, SerializeType>();

            foreach(var agg in aggregations.Values)
            {
                var dataset = new DataSet(
                    agg.AggregationName, agg.FileName, agg.GetDataAsDataElements(encode)
                );

                if(dic.ContainsKey(agg.FullPath) == false)
                {
                    dic.Add(agg.FullPath, new SerializeType());
                }

                dic[agg.FullPath].Add(dataset);
            }

            return dic;
        }

        /// <summary>
        /// 保存用の情報群から集団群に変換する
        /// </summary>
        /// <param name="sets">情報群</param>
        /// <returns>集団群</returns>
        public static Dictionary<string, Aggregation> DataSetsToAggregations(SerializeType sets)
        {
            var dic = new Dictionary<string, Aggregation>();

            foreach(var set in sets)
            {
                dic[set.AggregationName] = new Aggregation(set.Elements, set.AggregationName, set.FileName);
            }

            return dic;
        }

        /// <summary>
        /// データ群を<see cref="DataElement"/>型の情報群に変換する
        /// </summary>
        /// <remarks><paramref name="encode"/>がnullの時は<see cref="Encoding.UTF8"/>を使う</remarks>
        /// <param name="encode">シリアライズ時のエンコード</param>
        /// <returns>情報群</returns>
        public static Elements ExDicToDataElements(ExDictionary dic, Encoding encode = null, bool forceSerialize = false)
        {
            var list = new Elements();
            encode = encode ?? Encoding.UTF8;

            foreach(var pair in dic)
            {
                foreach(var e in pair.Value)
                {
                    list.Add(Object2DataElement(e.Value, e.Key, pair.Key, encode, forceSerialize));
                }
            }

            return list;
        }

        /// <summary>
        /// オブジェクトを<see cref="DataElement"/>型に変換する
        /// </summary>
        /// <param name="value">オブジェクト</param>
        /// <param name="key">データのキー</param>
        /// <param name="saveType">データの型</param>
        /// <param name="serialize">シリアライズするかどうか</param>
        /// <param name="encode">シリアライズ時のエンコード</param>
        /// <returns>保存時用の情報</returns>
        public static DataElement Object2DataElement(object value, string key, Type saveType, Encoding encode = null, bool forceSerialize = false)
        {
            var type = value.GetType();
            if(IsNeedSerialization(type) == false && forceSerialize == false)
            {
                return new DataElement(key, value, type, saveType);
            }

            using(var sw = new FileUtils.EncodedStringWriter(encode))
            {
                var serializer = new XmlSerializer(type);
                serializer.Serialize(sw, value);

                return new DataElement(key, sw.ToString(), type, saveType);
            }
        }

        /// <summary>
        /// ファイルを読み込んで<see cref="Aggregation"/>型にデシリアライズする
        /// </summary>
        /// <param name="path">ファイルのフルパス</param>
        /// <param name="encode">読み込み時のエンコード</param>
        /// <param name="aggs">デシリアライズしたデータの保存先</param>
        /// <returns>一つでもデータをデシリアライズしたかどうか</returns>
        public static bool Deserialize(string path, Encoding encode, ref Dictionary<string, Aggregation> aggs)
        {
            if(File.Exists(path) == false)
            {
                return false;
            }

            var flag = false;
            using(var sr = new StreamReader(path, encode))
            {
                foreach(var pair in DataSetsToAggregations((SerializeType)Serializer.Deserialize(sr)))
                {
                    flag = true;
                    aggs[pair.Key] = pair.Value;
                }
            }

            return flag;
        }

        /// <summary>
        /// オブジェクトをデシリアライズする
        /// </summary>
        /// <param name="value">デシリアライズするオブジェクト</param>
        /// <param name="type">オブジェクトの本来の型</param>
        /// <returns>デシリアライズした値</returns>
        public static object Deserialize(Type type, object value, bool forceSerialize = false)
        {
            using(var sr = new StringReader(value.ToString()))
            {
                if(IsNeedSerialization(type) == false && forceSerialize == false)
                {
                    return value;
                }

                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// <paramref name="type"/>についてシリアライズが必要かどうかを判断
        /// </summary>
        /// <remarks>Unityのビルトイン構造体とかはIsSerializable == falseになる</remarks>
        /// <param name="type">型情報</param>
        /// <returns>シリアライズするかどうか</returns>
        public static bool IsNeedSerialization(Type type)
        {
            return (type.IsClass == true || type.IsSerializable == false);
        }
    }
}
