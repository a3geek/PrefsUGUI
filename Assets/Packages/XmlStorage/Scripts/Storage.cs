using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace XmlStorage
{
    using Components;
    using Components.Aggregations;
    using Components.Utilities;
    using SerializeType = List<Components.Data.DataSet>;

    /// <summary>
    /// セットしたデータ群をXML形式で保存する
    /// </summary>
    public static partial class Storage
    {
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル名</summary>
        public static string FileName
        {
            set { CurrentAggregation.FileName = value; }
            get { return CurrentAggregation.FileName; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル拡張子</summary>
        public static string Extension
        {
            set { CurrentAggregation.Extension = value; }
            get { return CurrentAggregation.Extension; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存するファイルを置くフォルダ</summary>
        public static string DirectoryPath
        {
            set { CurrentAggregation.DirectoryPath = value; }
            get { return CurrentAggregation.DirectoryPath; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル名(拡張子なし)</summary>
        public static string FileNameWithoutExtension
        {
            get { return CurrentAggregation.FileNameWithoutExtension; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のフルパス</summary>
        public static string FullPath
        {
            get { return CurrentAggregation.FullPath; }
        }
        /// <summary>現在選択されている集団名</summary>
        public static string CurrentAggregationName
        {
            get; private set;
        }
        /// <summary>データを保存している各ファイルの保存先を保存しているファイルのフルパス</summary>
        public static string FilePathStoragesFullPath
        {
            get { return FilePathStorage.FullPath; }
        }
        

        /// <summary>現在選択されている集団</summary>
        private static Aggregation CurrentAggregation
        {
            get { return Aggregations[CurrentAggregationName]; }
        }
        /// <summary>集団群</summary>
        private static Dictionary<string, Aggregation> Aggregations = new Dictionary<string, Aggregation>();
        /// <summary>全保存ファイルの管理</summary>
        private static FilePathStorage FilePathStorage = new FilePathStorage();


        /// <summary>静的コンストラクタ</summary>
        static Storage()
        {
            Aggregations = Load();
            CurrentAggregationName = Consts.DefaultAggregationName;
        }
        
        /// <summary>
        /// セットしたデータ群をXML形式でファイルに保存する
        /// </summary>
        /// <remarks>全ての集団のデータ群を保存する</remarks>
        public static void Save()
        {
            var dic = Converter.AggregationsToDictionary(Aggregations, Consts.Encode);
            FilePathStorage.ClearFilePaths();

            foreach(var pair in dic)
            {
                using(var sw = new StreamWriter(pair.Key, false, Consts.Encode))
                {
                    var serializer = new XmlSerializer(typeof(SerializeType));
                    serializer.Serialize(sw, pair.Value);
                }

                FilePathStorage.AddFilePath(pair.Key);
            }

            FilePathStorage.Save();
        }

        /// <summary>
        /// 保存したファイルから情報を読み込む
        /// </summary>
        /// <returns>読み込んだ情報</returns>
        private static Dictionary<string, Aggregation> Load()
        {
            var aggs = new Dictionary<string, Aggregation>()
            {
                { Consts.DefaultAggregationName, new Aggregation(null, Consts.DefaultAggregationName) }
            };

            var flag = false;
            foreach(var path in FilePathStorage.Load())
            {
                flag |= Converter.Deserialize(path, Consts.Encode, ref aggs);
            }

            if(flag == false)
            {
                foreach(var path in SearchFiles())
                {
                    Converter.Deserialize(path, Consts.Encode, ref aggs);
                }
            }
            
            return aggs;
        }
        
        /// <summary>
        /// デフォルトの保存先フォルダに指定の拡張子のファイルがないか検索する
        /// </summary>
        /// <returns>検索結果</returns>
        private static string[] SearchFiles()
        {
            return Directory.GetFiles(
                Consts.DefaultSaveDirectory, Consts.ExtensionSearchPattern, SearchOption.AllDirectories
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
