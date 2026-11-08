using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using SnowRunnerModManager.Infrastructure.Xml;
using SnowRunnerModManager.Core.Classes;

namespace SnowRunnerModManager.Core.GameParsers
{
    public class Parser
    {
        //public string InitialDir { get; }
        //public string FolderName { get; }
        //public List<string> JSONParsed { get; }

        //public Parser(string initialDir, string folderName, List<string> jsonParsed)
        //{
        //    InitialDir = initialDir;
        //    FolderName = folderName;
        //    JSONParsed = jsonParsed;
        //}

        private static Dictionary<Type, Parser> Parsers { get; } = [];

        public ImmutableDictionary<string, ParserTree> PropertyIgnoreIfs { get; }
        public ImmutableDictionary<string, ParserTree> XMLElements { get; }
        public ImmutableDictionary<string, ParserTree> XMLTemplates { get; }
        public ImmutableDictionary<string, ParserTree> XMLRoots { get; }
        public ImmutableDictionary<string, ParserTree> XMLSpecials { get; }
        public Type ObjectType { get; }

        protected Parser(Type type)
        {
            ObjectType = type;

            List<PropertyInfo> properties = [.. type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<XMLPath>(true) != null && p.CanWrite)];

            //ImmutableDictionary<string, ParserTree> primarySteps = properties.Where(p => p.GetCustomAttributes<PropertyIgnoreIf>().Any()).Select(p =>
            //{
            //    XMLElement path = p.GetCustomAttribute<XMLElement>()!;
            //    return new KeyValuePair<string, ParserTree>(p.Name, new(path, p));
            //}).ToImmutableDictionary(p => p.Key, p => p.Value);

            //ImmutableDictionary<string, ParserTree> secondarySteps = properties.Where(p => !p.GetCustomAttributes<PropertyIgnoreIf>().Any()).Select(p =>
            //{
            //    XMLElement path = p.GetCustomAttribute<XMLElement>()!;
            //    return new KeyValuePair<string, ParserTree>(p.Name, new(path, p));
            //}).ToImmutableDictionary(p => p.Key, p => p.Value);

            //PrimarySteps = primarySteps;
            //SecondarySteps = secondarySteps;

            PropertyIgnoreIfs = GetParserTree<XMLPath>(properties, true);
            XMLElements = GetParserTree<XMLElement>(properties, false);
            XMLTemplates = GetParserTree<XMLTemplate>(properties, false);
            XMLRoots = GetParserTree<XMLRoot>(properties, false);
            XMLSpecials = GetParserTree<XMLSpecial>(properties, false);
        }

        private static ImmutableDictionary<string, ParserTree> GetParserTree<T>(List<PropertyInfo> properties, bool hasPropertyIgnoreIf) where T : XMLPath
        {
            Dictionary<string, ParserTree> res = [];

            foreach (PropertyInfo property in properties)
            {
                if (hasPropertyIgnoreIf != (property.GetCustomAttribute<PropertyIgnoreIf>() != null))
                    continue;

                T? path = property.GetCustomAttribute<T>(true);
                if (path == null)
                    continue;

                res.Add(property.Name, new(path, property.PropertyType, property));
            }

            return [.. res];
        }

        public static Parser GetParser<T>() where T : class =>
            GetParser(typeof(T));

        public static Parser GetParser(Type type)
        {
            if (!Parsers.TryGetValue(type, out Parser? parser))
            {
                parser = new Parser(type);
                Parsers.Add(type, parser);
            }

            return parser;
        }

        public T Parse<T, TParent, TChild>(XBase<TParent, TChild> element, GameContainer gc, string origin) where T : class
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            if (typeof(T) != ObjectType)
                throw new InvalidCastException();

            object? obj = Parse(element, gc, origin) ?? throw new InvalidCastException();
            return (T)obj;
        }

        public object? Parse<TParent, TChild>(XBase<TParent, TChild> container, GameContainer? gc, string origin)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            object obj = Activator.CreateInstance(ObjectType)!;

            //if (!LowLevelParse(container, obj, origin, true))
            //    return null;

            //if (!LowLevelParse(container, obj, origin, false))
            //    return null;

            if (!LowLevelParse(PropertyIgnoreIfs, gc, container, obj, origin, false))
                return null;

            if (!LowLevelParse(XMLSpecials, gc, container, obj, origin))
                return null;

            if (!LowLevelParse(XMLElements, gc, container, obj, origin))
                return null;

            if (!ResolveTemplates(XMLTemplates, gc, obj))
                return null;

            return obj;
        }

        private bool LowLevelParse<TParent, TChild>(ImmutableDictionary<string, ParserTree> steps, GameContainer? gc, XBase<TParent, TChild> container, object obj, string origin, bool accountForIgnores = true)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            HashSet<string> toIgnore = GetIgnores(obj, accountForIgnores);

            foreach (KeyValuePair<string, ParserTree> tree in steps)
            {
                if (toIgnore.Contains(tree.Value.Parameter.Name))
                    continue;

                if (!tree.Value.TryParse(container, gc, tree.Value.Parameter.PropertyType, origin, out object? tempObj))
                {
                    if (tree.Value.XMLPath.DefaultValue == null)
                        return false;

                    tempObj = tree.Value.XMLPath.DefaultValue;
                }

                tempObj = ParserHelper.Parse(tree.Value.Parameter.PropertyType, tempObj);

                tree.Value.Parameter.SetValue(obj, tempObj);
            }

            return true;
        }

        private bool ResolveTemplates(ImmutableDictionary<string, ParserTree> steps, GameContainer? gc, object obj, bool accountForIgnores = true)
        {
            HashSet<string> toIgnore = GetIgnores(obj, accountForIgnores);

            foreach (KeyValuePair<string, ParserTree> tree in steps)
            {
                if (toIgnore.Contains(tree.Value.Parameter.Name))
                    continue;

                Type templateType = tree.Value.Parameter.PropertyType;

                if (templateType.GetGenericTypeDefinition() != typeof(Templatable<>))
                    continue;

                if (!tree.Value.TryResolveTemplate(gc, templateType.GetGenericArguments()[0], out Template? template))
                    continue;

                object? templatable = Activator.CreateInstance(templateType, template);

                if (templatable == null)
                    continue;

                PropertyInfo? isOverridable = templateType.GetProperty("IsOverridable");
                if (isOverridable == null)
                    continue;

                isOverridable.SetValue(templatable, false);

                tree.Value.Parameter.SetValue(obj, templatable);

                return true;
            }

            return false;
        }

        private HashSet<string> GetIgnores(object obj, bool accountForIgnores) => accountForIgnores ? PropertyIgnoreIf.FindPropertiesToIgnore(ObjectType, obj) : [];

        public static bool TryResolveSpecial<TParent, TChild>(string special, string? specialArg, XBase<TParent, TChild> container, string origin, [NotNullWhen(true)] out string? result)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            try
            {
                result = special switch
                {
                    "OriginalState" => Enum.GetName(ModificationState.Original),
                    "FileName" => ParserHelper.GetFileName(origin),
                    "FileRelative" => ParserHelper.GetRelativePath(origin),
                    "axle_torque" => ParserHelper.GetAxleTorque(container, int.Parse(specialArg!)),
                    "TagName" => ParserHelper.GetTemplateName(container),
                    "template" => ParserHelper.ResolveTemplate(container, specialArg!),
                    //"is_wheel_template" => ParserHelper.IsWheelTemplate(container),
                    "has_axle" => ParserHelper.HasAxle(container, int.Parse(specialArg!)),
                    _ => null
                };
                if (result == null)
                    return false;

                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
