using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)fieldInfo?.GetCustomAttribute(typeof(DescriptionAttribute) , false);

        return descriptionAttribute != null ? descriptionAttribute.Description : throw new Exception("此 Value 無任何資料");
    }
}
