using System.IO;
using System.Text;

namespace XmlStorage.Systems
{
    using Utilities;

    public static class XmlStorageConsts
    {
        /// <summary>パスの区切り文字</summary>
        public static readonly char Separator = Path.DirectorySeparatorChar;
        /// <summary>Macのパスの区切り文字</summary>
        /// <remarks><see cref="Separator"/>に統一する</remarks>
        public static readonly char DoubleBackSlash = '\\';
        /// <summary>Windowsのパスの区切り文字</summary>
        /// <remarks><see cref="Separator"/>に統一する</remarks>
        public static readonly char Slash = '/';
        /// <summary>ピリオド</summary>
        public static readonly string Period = @".";

        /// <summary>ファイルに保存する時のエンコード情報</summary>
        public static readonly UTF8Encoding Encode = new UTF8Encoding(false);
        /// <summary>ファイルの拡張子</summary>
        public static readonly string Extension = FileUtils.AdjustAsExtension(Period + "xml");

        /// <summary>デフォルトの集団名</summary>
        public static readonly string DefaultAggregationName = "Default";
        /// <summary><see cref="Extension"/>拡張子のファイルを全検索するための検索パターン</summary>
        public static readonly string ExtensionSearchPattern = "*" + Extension;

        /// <summary>デフォルトの保存ディレクトリのディレクトリ名</summary>
        public static readonly string DefaultSubDirectoryName
            = FileUtils.AdjustAsDirectoryPath("Saves" + Separator, creatable: false);
        /// <summary>デフォルトの保存ディレクトリのフルパス</summary>
        public static readonly string DefaultSaveDirectory
            = FileUtils.AdjustAsDirectoryPath(
#if UNITY_EDITOR
                Directory.GetCurrentDirectory()
#else
                System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
#endif

                + Separator + DefaultSubDirectoryName
            );
    }
}
