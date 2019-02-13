using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

namespace XmlStorage.Components.Aggregations
{
    using Accessors;
    using Utilities;

    using Elements = List<Data.DataElement>;
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;

    /// <summary>
    /// データ群を集団としてまとめて管理する
    /// </summary>
    [Serializable]
    public sealed partial class Aggregation : Accessor
    {
        /// <summary>集団名</summary>
        public string AggregationName
        {
            get; private set;
        }
        /// <summary>全ての型をシリアライズして保存するかどうか</summary>
        /// <remarks>falseの時は<see cref="Type.IsSerializable"/>によってシリアライズするかどうかを判断する</remarks>
        public bool IsAllTypesSerialize
        {
            get; private set;
        }
        /// <summary>データ群を保存する時のファイル名</summary>
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = FileUtils.AdjustAsFileName(value, Consts.Extension, this.fileName); }
        }
        /// <summary>データ群を保存する時のファイル名(拡張子なし)</summary>
        public string FileNameWithoutExtension => this.FileName.TrimEnd(Consts.Extension.ToCharArray());
        /// <summary>データ群を保存する時のフルパス</summary>
        public string FullPath => Storage.DirectoryPath + this.FileName;

        /// <summary>データ群</summary>
        protected override ExDictionary dictionary => this.dic;

        /// <summary>保存する時のファイル名</summary>
        private string fileName = SceneManager.GetActiveScene().name + Consts.Extension;
        /// <summary>データ群</summary>
        private ExDictionary dic = new ExDictionary();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="elements">初期データ群</param>
        /// <param name="aggregationName">集団名</param>
        /// <param name="isAllTypesSerialize">全ての型をシリアライズして保存するかどうか</param>
        public Aggregation(Elements elements, string aggregationName, bool isAllTypesSerialize = false)
            : this(elements, aggregationName, "", isAllTypesSerialize) {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="elements">初期データ群</param>
        /// <param name="aggregationName">集団名</param>
        /// <param name="fileName">保存ファイルのフルパス</param>
        /// <param name="isAllTypesSerialize">全ての型をシリアライズして保存するかどうか</param>
        public Aggregation(Elements elements, string aggregationName, string fileName, bool isAllTypesSerialize = false)
        {
            this.IsAllTypesSerialize = isAllTypesSerialize;

            if(elements != null)
            {
                this.SetData(elements);
            }
            this.AggregationName = (string.IsNullOrEmpty(aggregationName) ? Guid.NewGuid().ToString() : aggregationName);

            if(string.IsNullOrEmpty(fileName) == false)
            {
                this.FileName = Path.GetFileName(fileName);
            }
        }

        /// <summary>
        /// 現在所属する全てのデータを消去して新規データ群を所属させる
        /// </summary>
        /// <param name="list">データ群</param>
        public void SetData(Elements list)
        {
            this.DeleteAll();
            this.AddData(list);
        }

        /// <summary>
        /// 新しいデータ群を所属させる
        /// </summary>
        /// <param name="list">データ群</param>
        public void AddData(Elements list)
        {
            for(var i = 0; i < list.Count; i++)
            {
                var e = list[i];
                var type = e.ValueType;
                var saveType = e.SaveType;

                if(e.ValueType == null || saveType == null)
                {
                    continue;
                }
                if(this.dictionary.ContainsKey(saveType) == false)
                {
                    this.dictionary[saveType] = new Dictionary<string, object>();
                }

                this.dictionary[saveType][e.Key] = Converter.Deserialize(type, e.Value, this.IsAllTypesSerialize);
            }
        }

        /// <summary>
        /// データ群を<see cref="DataElement"/>型の情報群に変換する
        /// </summary>
        /// <remarks><paramref name="encode"/>がnullの時は<see cref="Encoding.UTF8"/>を使う</remarks>
        /// <param name="encode">シリアライズ時のエンコード</param>
        /// <returns>情報群</returns>
        public Elements GetDataAsDataElements(Encoding encode = null)
            => Converter.ExDicToDataElements(this.dictionary, encode, this.IsAllTypesSerialize);
    }
}
