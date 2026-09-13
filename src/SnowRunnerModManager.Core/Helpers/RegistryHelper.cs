using SnowRunnerModManager.Core.Classes;

using System.Reflection;


using SnowRunnerModManager.Utils.Metadata;
using System.Diagnostics.CodeAnalysis;


namespace SnowRunnerModManager.Core.Helpers
{
    public class RegistryHelper
    {
        public static void GetAllRegistries<T>(out Dictionary<string, IRegistry> registries, out Dictionary<string, IRegistry> groupRegistries) where T : class
        {
            registries = [];
            groupRegistries = [];

            Type type = typeof(T);
            List<PropertyInfo> properties = [.. type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.GetCustomAttribute<EditorSource>() != null && p.CanRead)];

            foreach (PropertyInfo p in properties)
            {
                Type propertyType = p.PropertyType;

                if (!propertyType.IsGenericType)
                    continue;

                Type genericDefinition = propertyType.GetGenericTypeDefinition();

                if (genericDefinition == typeof(Registry<>))
                {
                    registries.Add(p.Name, (IRegistry)p.GetValue(null)!);
                    continue;
                }

                if (genericDefinition == typeof(GroupRegistry<>))
                {
                    groupRegistries.Add(p.Name, (IRegistry)p.GetValue(null)!);
                    continue;
                }
            }
        }

        public static bool GetBoundRegistry(Dictionary<string, IRegistry> registries, Dictionary<string, IRegistry> groupRegistries, EditorBinding binding, [NotNullWhen(true)] out IRegistry? registry)
        {
            if (registries.TryGetValue(binding.SourceName, out registry))
                return true;

            if (groupRegistries.TryGetValue(binding.SourceName, out registry))
                return true;

            registry = null;
            return false;
        }
    }
}
