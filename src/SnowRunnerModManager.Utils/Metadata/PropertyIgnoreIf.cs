using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyIgnoreIf(bool ignoreIf, params string[] paramNames) : Attribute
    {
        public bool IgnoreIf { get; } = ignoreIf;
        public string[] ParamNames { get; } = paramNames;

        public static HashSet<string> FindPropertiesToIgnore(Type type, object obj)
        {
            List<PropertyInfo> properties = [.. type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttributes<PropertyIgnoreIf>().Any() && p.CanRead)];

            HashSet<string> result = [];

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType != typeof(bool))
                    throw new Exception("PropertyIgnoreIf applied to a different type than bool");

                PropertyIgnoreIf[] propertyIgnoreIfs = [.. property.GetCustomAttributes<PropertyIgnoreIf>()];

                bool value = (bool)property.GetValue(obj)!;

                foreach (PropertyIgnoreIf propertyIgnoreIf in propertyIgnoreIfs)
                {
                    if (value != propertyIgnoreIf.IgnoreIf)
                        continue;

                    foreach (string s in propertyIgnoreIf.ParamNames)
                        result.Add(s);
                }
            }

            return result;
        }
    }
}
