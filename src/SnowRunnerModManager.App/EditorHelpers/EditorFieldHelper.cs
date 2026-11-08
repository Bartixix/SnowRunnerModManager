using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorUserControls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

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

        public static bool GetSimpleControlValue(Control control, Type type, [NotNullWhen(true)] out object? value)
        {
            value = null;

            if (control is NumericUpDown numericUpDown)
            {
                decimal userValue = numericUpDown.Value ?? 0;

                if (type == typeof(double))
                {
                    value = (double)userValue;
                    return true;
                }

                if (type == typeof(int))
                {
                    value = decimal.ToInt32(userValue);
                    return true;
                }

                return false;
            }

            if (control is TextBox textBox)
            {
                value = textBox.Text ?? string.Empty;
                return true;
            }

            if (control is CheckBox checkBox)
            {
                value = checkBox.IsChecked ?? false;
                return true;
            }

            if (control is ComboBox comboBox)
            {
                if (Enum.TryParse(type, (string)comboBox.SelectedValue!, true, out value))
                    return true;
                return false;
            }


            return false;
        }

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

                        Target = target,
                        Depth = depth + 1,
                    });
                    continue;
                }

                result.Add(new EditorFieldControl
                {
                    FieldName = parameter.Value.Name,
                    Order = parameter.Value.Order,
                    Parameter = parameter.Key,

                    Target = target,
                    Depth = depth
                });
            }

            return result;
        }

        public static List<EditorUserControl> GetListControls(Dictionary<PropertyInfo, EditorList> parameters, object target, int depth)
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

                    Target = target,
                    Depth = depth
                });
            }

            return result;
        }

        public static List<EditorUserControl> GetBindingControls(Dictionary<PropertyInfo, EditorBinding> parameters, object target, int depth)
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
                    MultiChoice = parameter.Value.MultiChoice,

                    Target = target,
                    Depth = depth
                });
            }

            return result;
        }

        public static List<Control> GetControls(object target, int depth)
        {
            Type type = target.GetType();

            HashSet<string> ignoredProperties = PropertyIgnoreIf.FindPropertiesToIgnore(type, target);

            Dictionary<PropertyInfo, EditorField> editorFields = GetEditorProperties<EditorField>(type, ignoredProperties);
            Dictionary<PropertyInfo, EditorList> editorLists = GetEditorProperties<EditorList>(type, ignoredProperties);
            Dictionary<PropertyInfo, EditorBinding> editorBindings = GetEditorProperties<EditorBinding>(type, ignoredProperties);

            List<EditorUserControl> controls = [];
            controls.AddRange(GetFieldControls(editorFields, target, depth));
            controls.AddRange(GetListControls(editorLists, target, depth));
            controls.AddRange(GetBindingControls(editorBindings, target, depth));

            return [.. controls.OrderBy(uc => uc.Order)];
        }
    }
}
