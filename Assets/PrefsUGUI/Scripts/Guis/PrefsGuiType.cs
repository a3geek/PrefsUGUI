using System;
using System.Collections.Generic;

namespace PrefsUGUI.Guis
{
    using CustomExtensions.Attributes;
    using CustomExtensions.Csharp;
    using Guis.Preferences;
    using System.Linq;

    public enum PrefsGuiType
    {
        [PrefsGuiComponentType(null)]
        None = 0,

        [PrefsGuiComponentType(typeof(PrefsGuiBool))]
        PrefsGuiBool = 1,

        [PrefsGuiComponentType(typeof(PrefsGuiColor))]
        PrefsGuiColor,

        [PrefsGuiComponentType(typeof(PrefsGuiColorSlider))]
        PrefsGuiColorSlider,

        [PrefsGuiComponentType(typeof(PrefsGuiEnum))]
        PrefsGuiEnum,

        [PrefsGuiComponentType(typeof(PrefsGuiNumericDecimal))]
        PrefsGuiNumericDecimal,

        [PrefsGuiComponentType(typeof(PrefsGuiNumericInteger))]
        PrefsGuiNumericInteger,

        [PrefsGuiComponentType(typeof(PrefsGuiNumericSliderDecimal))]
        PrefsGuiNumericSliderDecimal,

        [PrefsGuiComponentType(typeof(PrefsGuiNumericSliderInteger))]
        PrefsGuiNumericSliderInteger,

        [PrefsGuiComponentType(typeof(PrefsGuiString))]
        PrefsGuiString,

        [PrefsGuiComponentType(typeof(PrefsGuiVector2))]
        PrefsGuiVector2,

        [PrefsGuiComponentType(typeof(PrefsGuiVector2Int))]
        PrefsGuiVector2Int,

        [PrefsGuiComponentType(typeof(PrefsGuiVector3))]
        PrefsGuiVector3,

        [PrefsGuiComponentType(typeof(PrefsGuiVector3Int))]
        PrefsGuiVector3Int,

        [PrefsGuiComponentType(typeof(PrefsGuiVector4))]
        PrefsGuiVector4,

        [PrefsGuiComponentType(typeof(PrefsGuiRect))]
        PrefsGuiRect,

        [PrefsGuiComponentType(typeof(PrefsGuiButton))]
        PrefsGuiButton,

        [PrefsGuiComponentType(typeof(PrefsGuiLabel))]
        PrefsGuiLabel,

        [PrefsGuiComponentType(typeof(PrefsGuiImageLabel))]
        PrefsGuiImageLabel,
    }

    public static class PrefsGuiTypeExtentions
    {
        private static IReadOnlyDictionary<Type, PrefsGuiType> PrefsGuiTypesDictionary { get; }
            = Enum.GetValues(typeof(PrefsGuiType))
            .Cast<PrefsGuiType>().ToDictionary(guiType => guiType.GetPrefsGuiComponentType());


        public static Type GetPrefsGuiComponentType(this PrefsGuiType type)
            => type.GetAttribute<PrefsGuiComponentTypeAttribute>()?.ComponentType;

        public static PrefsGuiType GetPrefsGuiTypeByComponentType(Type type)
            => PrefsGuiTypesDictionary.TryGetValue(type, out var guiType) == true ? guiType : PrefsGuiType.None;
    }
}
