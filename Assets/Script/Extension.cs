using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

public static class Extension
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute != null ? attribute.Description : value.ToString();
    }
    [Serializable]
    public class Wrapper<T>
    {
        public List<T> Items;
    }
}
