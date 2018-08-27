using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace XmlStorage.Components
{
    using Utilities;

    public static class Consts
    {
        public static readonly char Separator = Path.DirectorySeparatorChar;
        public static readonly char DoubleBackSlash = '\\';
        public static readonly char Slash = '/';
        public static readonly string Period = @".";
        public static readonly string BackDirectory = @"..";

        /// <summary>ファイルに保存する時のエンコード情報</summary>
        public static readonly UTF8Encoding Encode = new UTF8Encoding(false);

        /// <summary>デフォルトの集団名</summary>
        public static readonly string DefaultAggregationName = "Default";

        public static readonly string ExtensionSearchPattern = "*" + DefaultExtension;

        public static readonly string DefaultExtension = FileUtils.AdjustAsExtension(Period + "xml");
        public static readonly string DefaultSubDirectoryName
            = FileUtils.AdjustAsDirectoryPath("Saves" + Separator);
        public static readonly string DefaultSaveDirectory
            = FileUtils.AdjustAsDirectoryPath(
                Application.dataPath + Separator + BackDirectory + Separator + DefaultSubDirectoryName
            );

        public static readonly string DefaultFilePathStoragesSaveFolder
            = FileUtils.AdjustAsDirectoryPath(Application.persistentDataPath + Separator);
        public static readonly string DefaultFilePathStoragesFileName
            = FileUtils.AdjustAsFileName("FilePaths.txt", ".txt");
    }
}
