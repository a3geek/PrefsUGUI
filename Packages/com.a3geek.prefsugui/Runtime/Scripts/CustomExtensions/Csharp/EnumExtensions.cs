using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PrefsUGUI.CustomExtensions.CSharp
{
    public static class EnumExtensions
    {
        public static AttributeType GetAttribute<AttributeType>(this Enum enumKey) where AttributeType : Attribute
        {
            return EnumAttributeCache<AttributeType>.GetOrAdd(
                enumKey, e => e.GetAttributes<AttributeType>().FirstOrDefault()
            );
        }

        private static IEnumerable<AttributeType> GetAttributes<AttributeType>(this Enum enumKey)
            where AttributeType : Attribute
        {
            var fieldInfo = enumKey.GetType().GetField(enumKey.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(AttributeType), false).Cast<AttributeType>();

            return attributes;
        }


        private static class EnumAttributeCache<AttributeType> where AttributeType : Attribute
        {
            private static ConcurrentDictionary<Enum, AttributeType> Dictionary { get; } = new();


            public static AttributeType GetOrAdd(Enum enumKey, Func<Enum, AttributeType> factory)
            {
                return Dictionary.GetOrAdd(enumKey, factory);
            }
        }
    }
}
