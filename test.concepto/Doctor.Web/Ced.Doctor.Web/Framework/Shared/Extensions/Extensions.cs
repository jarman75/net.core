using System;
using System.ComponentModel;
using System.Reflection;

namespace Framework.Shared.Extensions
{
    public static class ClassExtensions
    {
        public static string GetDescription(this Object value)
        {
            var type = value.GetType();

            var descriptions = (DescriptionAttribute[])
            type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
            {
                return value.ToString();
            }
            return descriptions[0].Description;
        }
    }



    public class PropertyExtensions<T> where T : class
    {
        private readonly object t;

        public PropertyExtensions(object T)
        {
            t = T;
        }

        public object this[string propertyName]
        {
            get
            {
                Type type = typeof(T);
                PropertyInfo propInfo = type.GetProperty(propertyName);
                return propInfo.GetValue(t, null);
            }
            set
            {
                Type type = typeof(T);
                PropertyInfo propInfo = type.GetProperty(propertyName);
                propInfo.SetValue(t, value, null);

            }
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum value)
        {
            Type type = value.GetType();

            MemberInfo[] memInfo = type.GetMember(value.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return value.ToString();
        }
    }

}
