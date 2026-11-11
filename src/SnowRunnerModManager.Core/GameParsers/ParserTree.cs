using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Infrastructure.Xml;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace SnowRunnerModManager.Core.GameParsers
{
    public class ParserTree
    {
        public XMLPath XMLPath { get; }
        public Type XMLPathType { get; }
        public PropertyInfo Parameter { get; }
        public ImmutableArray<ParserPath> Paths { get; }

        public ParserTree(XMLPath xmlPath, Type xmlPathType, PropertyInfo parameter)
        {
            XMLPath = xmlPath;
            XMLPathType = xmlPathType;
            Parameter = parameter;

            string[] strPaths = xmlPath.Path.Split("||");

            ParserPath[] paths = new ParserPath[strPaths.Length];

            for (int i = 0; i < strPaths.Length; i++)
                paths[i] = new(strPaths[i], parameter.Name);

            Paths = [.. paths];
        }

        public bool TryParse<TParent, TChild>(XBase<TParent, TChild> container, GameContainer? gc, Type type, string origin, out object? obj)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            obj = null;

            for (int i = 0; i < Paths.Length; i++)
                if (TryEvalPath(container, gc, Paths[i], type, origin, out obj))
                    return true;


            return false;
        }

        public bool TryEvalPath<TParent, TChild>(XBase<TParent, TChild> container, GameContainer? gc, ParserPath path, Type type, string origin, out object? obj)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            obj = null;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Templatable<,>))
            {
                path.TryGetSingleValue(container, origin, out string? value);

                if (!TryResolveTemplatable(gc, type, value, out obj))
                    return false;

                return true;
            }

            else if (type.IsAssignableTo(typeof(Template)))
            {
                //if (!TryResolveTemplate(gc, type, out Template? temp))
                //    return false;

                _ = TryResolveTemplate(gc, type, out Template? temp);

                obj = temp;
                return true;
            }


            else if (type == typeof(List<string>) && XMLPath.ListSeparator != null)
            {
                if (!path.TryGetSingleValue(container, origin, out string? value))
                    return false;

                List<string> list = [.. value.Split(XMLPath.ListSeparator)];
                obj = list;

                return true;
            }


            else if (type.IsClass && type != typeof(string))
            {
                if (XMLPath.Special == SpecialResolver.MultipleValues && path.TryGetClassValueList(container, gc, type, origin, out IList? list))
                {
                    obj = list;
                    return true;
                }

                if (XMLPath.Special != SpecialResolver.MultipleValues && path.TryGetClassValue(container, gc, type, origin, out obj))
                    return true;
            }


            else if (!type.IsClass || type == typeof(string))
            {
                if (!path.TryGetSingleValue(container, origin, out string? value))
                    return false;

                obj = value;
                return true;
            }

            return false;
        }

        public bool TryResolveTemplate(GameContainer? gc, Type type, [NotNullWhen(true)] out Template? template)
        {
            template = null;

            if (gc != null && TryGetLocalTemplate(gc, type, out template))
                return true;

            if (TryGetGlobalTemplate(type, out template))
                return true;

            return false;
        }

        public bool TryGetLocalTemplate(GameContainer gc, Type type, [NotNullWhen(true)] out Template? template)
        {
            template = null;

            if (!gc.Templates.TryGetValue(type, out Dictionary<string, Template>? templates))
                return false;

            foreach (ParserPath path in Paths)
            {
                if (path.Steps.Length < 2)
                    continue;

                string name = path.Steps[1];
                if (!templates.TryGetValue(name, out template))
                    continue;

                if (template.GetType() != type)
                    continue;

                return true;
            }

            return false;
        }

        public bool TryGetGlobalTemplate(Type type, [NotNullWhen(true)] out Template? template)
        {
            template = null;
            Type dbType = typeof(ActiveProject);

            PropertyInfo[] properties = dbType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            IRegistry? registry = null;

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead)
                    continue;

                if (!property.PropertyType.IsGenericType)
                    continue;

                Type pType = property.PropertyType.GetGenericArguments()[0];

                if (pType != type)
                    continue;

                if (property.GetValue(null) is IRegistry reg)
                {
                    registry = reg;
                    break;
                }

                return false;
            }

            if (registry == null)
                return false;

            foreach (ParserPath path in Paths)
            {
                if (!registry.TryGet(path.Steps[0], out object? temp))
                    continue;

                if (temp is not Template)
                    return false;

                template = (Template)temp;
                return true;
            }

            return false;
        }

        public bool TryResolveTemplatable(GameContainer? gc, Type type, string? singleValue, [NotNullWhen(true)] out object? obj)
        {
            obj = null;

            Type tValue = type.GetGenericArguments()[0];
            Type tTemplate = type.GetGenericArguments()[1];

            _ = TryResolveTemplate(gc, tTemplate, out Template? template);

            if (singleValue == null)
            {
                if (template == null)
                    return false;

                obj = Activator.CreateInstance(type, Parameter.Name, template);

                if (obj == null)
                    return false;

                return true;
            }

            object tempObj = ParserHelper.Parse(tValue, singleValue);

            if (template == null)
                obj = Activator.CreateInstance(type, Parameter.Name, tempObj);
            else
                obj = Activator.CreateInstance(type, Parameter.Name, tempObj, template);

            if (obj == null)
                return false;

            return true;
        }
    }
}
