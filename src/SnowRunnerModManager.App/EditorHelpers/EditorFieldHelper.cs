using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorUserControls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Events;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SnowRunnerModManager.App.EditorHelpers
{
    internal class EditorFieldHelper
    {
        public static Control? GetSimpleTypeControl(object o)
        {
            Type type = o.GetType();

            if (IsNumeric(type))
                return new NumericUpDown() { Value = decimal.Parse(o.ToString()!) };

            if (IsString(type))
                return new TextBox() { Text = (string)o };

            if (IsBool(type))
                return new CheckBox() { IsChecked = (bool)o };

            if (IsEnum(type))
            {
                ComboBox comboBox = new();

                foreach (string value in Enum.GetNames(type))
                    comboBox.Items.Add(value);
                comboBox.SelectedValue = Enum.GetName(type, o);

                return comboBox;
            }

            return null;
        }

        private static bool IsNumeric(Type type) => type == typeof(int) || type == typeof(double);
        private static bool IsString(Type type) => type == typeof(string);
        private static bool IsBool(Type type) => type == typeof(bool);
        private static bool IsEnum(Type type) => type.IsEnum;



        public static Dictionary<PropertyInfo, T> GetEditorProperties<T>(Type t, HashSet<string> ignoredProperties) where T : EditorMetadata
        {
            List<PropertyInfo> properties = [.. t.GetProperties(BindingFlags.Public | BindingFlags.Instance)];

            List<PropertyInfo> applicableProperties = [.. properties.Where(p => p.GetCustomAttribute<T>() != null && p.CanWrite && p.CanRead && !ignoredProperties.Contains(p.Name))];

            Dictionary<PropertyInfo, T> propertyDictionary = applicableProperties
                .Select(p => new KeyValuePair<PropertyInfo, T>(p, p.GetCustomAttribute<T>()!))
                .ToDictionary(p => p.Key, p => p.Value);

            return propertyDictionary;
        }

        public static List<EditorUserControl> GetFieldControls(Dictionary<PropertyInfo, EditorField> parameters, object target, int depth)
        {
            List<EditorUserControl> result = [];

            foreach (KeyValuePair<PropertyInfo, EditorField> parameter in parameters)
            {
                if (parameter.Key.GetCustomAttribute<EditorSimpleClass>() != null)
                {
                    result.Add(new EditorSimpleClassControl
                    {
                        FieldName = parameter.Value.Name,
                        Order = parameter.Value.Order,
                        Parameter = parameter.Key,

                        Target = parameter.Key.GetValue(target)!,
                        Depth = depth + 1,
                    });
                    continue;
                }

                result.Add(new EditorFieldControl
                {
                    FieldName = parameter.Value.Name,
                    Order = parameter.Value.Order,
                    Parameter = parameter.Key,

                    Target = parameter.Key.GetValue(target)!,
                    MarginLeft = depth * Editor.MarginPerDepth
                });
            }

            return result;
        }

        public static List<EditorUserControl> GetListControls(Dictionary<PropertyInfo, EditorList> parameters, object target)
        {
            List<EditorUserControl> result = [];

            foreach (KeyValuePair<PropertyInfo, EditorList> parameter in parameters)
            {
                result.Add(new EditorListControl
                {
                    FieldName = parameter.Value.Name,
                    Order = parameter.Value.Order,
                    Parameter = parameter.Key,

                    AllowAdd = parameter.Value.AllowAdd,
                    AllowReorder = parameter.Value.AllowReorder,
                    AllowRemove = parameter.Value.AllowRemove,

                    Target = parameter.Key.GetValue(target)!,
                });
            }

            return result;
        }

        public static List<EditorUserControl> GetBindingControls(Dictionary<PropertyInfo, EditorBinding> parameters, object target)
        {
            List<EditorUserControl> result = [];

            RegistryHelper.GetAllRegistries<ActiveProject>(out var registries, out var groupRegistries);

            foreach (KeyValuePair<PropertyInfo, EditorBinding> parameter in parameters)
            {
                if (!RegistryHelper.GetBoundRegistry(registries, groupRegistries, parameter.Value, out IRegistry? registry))
                    throw new Exception($"Failed to fetch the required registry: {parameter.Value.SourceName}");



                result.Add(new EditorBindingControl
                {
                    FieldName = parameter.Value.Name,
                    Order = parameter.Value.Order,
                    Parameter = parameter.Key,

                    BoundRegistry = registry,

                    Target = parameter.Key.GetValue(target)!,
                });
            }

            return result;
        }

        public static List<Control> GetControls(object target, int depth)
        {
            Type type = target.GetType();

            HashSet<string> ignoredProperties = PropertyIgnoreIf.FindPropertiesToIgnore(target);

            Dictionary<PropertyInfo, EditorField> editorFields = GetEditorProperties<EditorField>(type, ignoredProperties);
            Dictionary<PropertyInfo, EditorList> editorLists = GetEditorProperties<EditorList>(type, ignoredProperties);
            Dictionary<PropertyInfo, EditorBinding> editorBindings = GetEditorProperties<EditorBinding>(type, ignoredProperties);

            List<EditorUserControl> controls = [];
            controls.AddRange(GetFieldControls(editorFields, target, depth));
            controls.AddRange(GetListControls(editorLists, target));
            controls.AddRange(GetBindingControls(editorBindings, target));

            return [.. controls.OrderBy(uc => uc.Order)];
        }
    }
}
