using System;
using System.ComponentModel;

namespace Fba.Api.Helper
{
	public static class EnumHelper
	{
        public static string GetEnumDescription(this System.Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }
    }
}

