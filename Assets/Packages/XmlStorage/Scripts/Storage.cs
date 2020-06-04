using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage
{
    using Systems;
    using Systems.Aggregations;
    using Systems.Utilities;

    using SerializeType = List<Systems.Data.DataSet>;

    /// <summary>
    /// セットしたデータ群をXML形式で保存する
    /// </summary>
    public static partial class Storage
    {
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存するファイルを置くフォルダ</summary>
        public static string DirectoryPath
        {
            get => Folder;
            set => Folder = FileUtils.AdjustAsDirectoryPath(value, Folder, false);
        }
        /// <summary>現在選択されている集団名</summary>
        public static string CurrentAggregationName
        {
            get; private set;
        }
        /// <summary>デフォルトの集団名</summary>
        public static string DefaultAggregationName => XmlStorageConsts.DefaultAggregationName;
        /// <summary>デフォルトの保存ディレクトリのフルパス</summary>
        public static string DefaultSaveDirectory => XmlStorageConsts.DefaultSaveDirectory;
        /// <summary>現在選択されている集団</summary>
        public static Aggregation CurrentAggregation => Aggregations[CurrentAggregationName];

        /// <summary>集団群</summary>
        private static Dictionary<string, Aggregation> Aggregations
        {
            set => Aggs = value;
            get
            {
                if(Aggs == null)
                {
                    Load();
                }

                return Aggs;
            }
        }

        /// <summary>集団群</summary>
        private static Dictionary<string, Aggregation> Aggs = null;
        /// <summary>保存するファイルを格納するフォルダ</summary>
        private static string Folder = DefaultSaveDirectory;


        /// <summary>静的コンストラクタ</summary>
        static Storage()
        {
            CurrentAggregationName = DefaultAggregationName;
        }

        /// <summary>
        /// セットしたデータ群をXML形式でファイルに保存する
        /// </summary>
        /// <remarks>全ての集団のデータ群を保存する</remarks>
        public static void Save()
        {
            var dic = Converter.AggregationsToDictionary(Aggregations, XmlStorageConsts.Encode);

            foreach(var pair in dic)
            {
                using(var sw = new StreamWriter(pair.Key, false, XmlStorageConsts.Encode))
                {
                    var serializer = new XmlSerializer(typeof(SerializeType));
                    serializer.Serialize(sw, pair.Value);
                }
            }
        }

        /// <summary>
        /// 保存したファイルから情報を読み込む
        /// </summary>
        /// <returns>読み込んだ情報</returns>
        public static void Load()
        {
            var aggs = new Dictionary<string, Aggregation>()
            {
                [DefaultAggregationName] = new Aggregation(null, DefaultAggregationName)
            };

            foreach(var path in SearchFiles())
            {
                Converter.Deserialize(path, XmlStorageConsts.Encode, ref aggs);
            }

            Aggregations = aggs;
            CurrentAggregationName = DefaultAggregationName;
        }

        /// <summary>
        /// <see cref="DirectoryPath"/>に指定の拡張子のファイルがないか検索する
        /// </summary>
        /// <returns>検索結果</returns>
        private static string[] SearchFiles()
        {
            if(Directory.Exists(DirectoryPath) == false)
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            return Directory.GetFiles(
                DirectoryPath, XmlStorageConsts.ExtensionSearchPattern, SearchOption.AllDirectories
            );
        }

        /// <summary>
        /// <paramref name="aggregationName"/>で指定した集団に対して処理を行う
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <param name="action">処理内容</param>
        private static void Action(string aggregationName, Action<Aggregation> action)
        {
            Func(aggregationName, agg =>
            {
                action(agg);
                return true;
            });
        }

        /// <summary>
        /// <paramref name="aggregationName"/>で指定した集団に対して処理を行い値を返す
        /// </summary>
        /// <typeparam name="T">返す値の型</typeparam>
        /// <param name="aggregationName">集団名</param>
        /// <param name="func">処理内容</param>
        /// <returns>返却値</returns>
        private static T Func<T>(string aggregationName, Func<Aggregation, T> func)
        {
            return HasAggregation(aggregationName) == true ?
                func(Aggregations[aggregationName]) :
                func(CurrentAggregation);
        }
    }
}
