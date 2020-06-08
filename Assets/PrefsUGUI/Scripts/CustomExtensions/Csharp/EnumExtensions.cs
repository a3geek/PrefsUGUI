using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PrefsUGUI.CustomExtensions.Csharp
{
    public static class EnumExtensions
    {
        public static AttributeType GetAttribute<AttributeType>(this Enum enumKey) where AttributeType : Attribute
        {
            return EnumAttributesCache<AttributeType>.GetOrAdd(enumKey, e => enumKey.GetAttributes<AttributeType>()?.First());
        }

        private static IEnumerable<AttributeType> GetAttributes<AttributeType>(this Enum enumKey) where AttributeType : Attribute
        {
            var fieldInfo = enumKey.GetType().GetField(enumKey.ToString());
            var attributes
                = fieldInfo.GetCustomAttributes(typeof(AttributeType), false)
                .Cast<AttributeType>();

            return (attributes?.Count() ?? 0) <= 0 ? null : attributes;
        }

        private static class EnumAttributesCache<AttributeType> where AttributeType : Attribute
        {
            private static ConcurrentDictionary<Enum, AttributeType> Dictionary { get; }
                = new ConcurrentDictionary<Enum, AttributeType>();


            internal static AttributeType GetOrAdd(Enum enumKey, Func<Enum, AttributeType> valueFactory)
                => Dictionary.GetOrAdd(enumKey, valueFactory);
        }
    }
}
