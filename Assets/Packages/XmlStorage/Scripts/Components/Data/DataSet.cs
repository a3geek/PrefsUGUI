using System;
using System.Collections.Generic;

namespace XmlStorage.Components.Data
{
    using Utilities;

    using Elements = List<DataElement>;

    /// <summary>
    /// データ群をファイルに保存する時のデータ形式
    /// </summary>
    [Serializable]
    public sealed class DataSet
    {
        /// <summary>集団名</summary>
        public string AggregationName { get; set; }
        /// <summary>ファイル名</summary>
        public string FileName { get; set; }
        /// <summary>保存するデータ群</summary>
        public Elements Elements { get; set; }

        /// <summary>フルパス</summary>
        public string FullPath => Storage.DirectoryPath + this.FileName;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>シリアライズするのにデフォルトコンストラクタが必要</remarks>
        public DataSet() : this("", "", null) {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="extension">拡張子</param>
        /// <param name="directoryPath">フォルダ名</param>
        /// <param name="elements">保存するデータ群</param>
        public DataSet(string aggregationName, string fileName, Elements elements)
        {
            this.AggregationName = aggregationName;
            this.FileName = FileUtils.AdjustAsFileName(fileName, Consts.Extension);
            this.Elements = (elements ?? new Elements());
        }
    }
}
