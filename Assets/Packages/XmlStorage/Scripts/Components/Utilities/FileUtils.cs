using System.IO;
using System.Text;

namespace XmlStorage.Components.Utilities
{
    public static class FileUtils
    {
        /// <summary>
        /// <see cref="StringWriter"/>を指定したエンコードで保存する
        /// </summary>
        public class EncodedStringWriter : StringWriter
        {
            /// <summary>
            /// 保存する時のエンコード
            /// </summary>
            public override Encoding Encoding => this.encode;

            /// <summary>
            /// 保存する時のエンコード
            /// </summary>
            protected Encoding encode = Encoding.UTF8;


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public EncodedStringWriter() : base() {; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="encode">保存する時のエンコード</param>
            public EncodedStringWriter(Encoding encode) : this()
            {
                this.encode = encode ?? this.encode;
            }
        }

        /// <summary>
        /// ファイル名として調整する
        /// </summary>
        /// <param name="fileName">調整するファイル名</param>
        /// <param name="defaultValue">ファイル名として問題がある時に返すデフォルト値</param>
        /// <returns>ファイル名</returns>
        public static string AdjustAsFileName(string fileName, string extension, string defaultValue = null)
            => string.IsNullOrEmpty(fileName) == true ?
                defaultValue :
                (fileName.EndsWith(extension) ? fileName : fileName + extension);

        /// <summary>
        /// 拡張子として調整する
        /// </summary>
        /// <param name="extension">調節する拡張子</param>
        /// <param name="defaultValue">拡張子として問題がある時に返すデフォルト値</param>
        /// <returns>拡張子</returns>
        public static string AdjustAsExtension(string extension, string defaultValue = null)
            => string.IsNullOrEmpty(extension) == true ?
                defaultValue :
                (extension.StartsWith(Consts.Period) ? extension : Consts.Period + extension);

        /// <summary>
        /// フォルダパスとして調整する
        /// </summary>
        /// <param name="directoryPath">調節するフォルダパス</param>
        /// <param name="defaultValue">フォルダパスとして問題がある時に返すデフォルト値</param>
        /// <returns>フォルダパス</returns>
        public static string AdjustAsDirectoryPath(string directoryPath, string defaultValue = null, bool creatable = true)
        {
            if(string.IsNullOrEmpty(directoryPath))
            {
                return defaultValue;
            }

            directoryPath = ChangeSeparatorChar(directoryPath);

            if(Directory.Exists(directoryPath) == false && creatable == true)
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath.EndsWith(Consts.Separator.ToString()) ? directoryPath : directoryPath + Consts.Separator;
        }

        /// <summary>
        /// パスの区切り文字を環境に合わせて調整する
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns>調節したパス</returns>
        public static string ChangeSeparatorChar(string path)
        {
            if(string.IsNullOrEmpty(path) == true)
            {
                return null;
            }

            path = path.Replace(Consts.DoubleBackSlash, Consts.Slash);
            return path.Replace(Consts.Slash, Consts.Separator);
        }
    }
}
