using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;
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

        public bool TryParse<TParent, TChild>(XBase<TParent, TChild> container, GameContainer? gc, Type type, string origin, [NotNullWhen(true)] out object? obj)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            obj = null;

            for (int i = 0; i < Paths.Length; i++)
            {
                //if (type == typeof(List<string>) && XMLPath.ListSeparator != null && Paths[i].TryGetSingleValue(container, origin, out string? temp))
                //{
                //    obj = temp.Split(XMLPath.ListSeparator).ToList();
                //    return true;
                //}

                //if (type.IsClass && type != typeof(string))
                //{
                //    if (XMLPath.Special != SpecialResolver.MultipleValues && Paths[i].TryGetClassValue(container, type, origin, out obj))
                //        return true;

                //    if (XMLPath.Special == SpecialResolver.MultipleValues && Paths[i].TryGetClassValueList(container, type, origin, out IList? list))
                //    {
                //        obj = list;
                //        return true;
                //    }
                //}

                //if ((!type.IsClass || type == typeof(string)) && Paths[i].TryGetSingleValue(container, origin, out temp))
                //{
                //    obj = temp;
                //    return true;
                //}

                if (TryEvalPath(container, gc, Paths[i], type, origin, out obj))
                    return true;
            }

            return false;
        }

        public bool TryEvalPath<TParent, TChild>(XBase<TParent, TChild> container, GameContainer? gc, ParserPath path, Type type, string origin, [NotNullWhen(true)] out object? obj)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            //obj = null;

            //if (type == typeof(List<string>) && XMLPath.ListSeparator != null && path.TryGetSingleValue(container, origin, out string? temp))
            //{
            //    obj = temp.Split(XMLPath.ListSeparator).ToList();
            //    return true;
            //}

            //if (type.IsClass && type != typeof(string))
            //{
            //    if (XMLPath.Special != SpecialResolver.MultipleValues && path.TryGetClassValue(container, gc, type, origin, out obj))
            //        return true;

            //    if (XMLPath.Special == SpecialResolver.MultipleValues && path.TryGetClassValueList(container, gc, type, origin, out IList? list))
            //    {
            //        obj = list;
            //        return true;
            //    }
            //}

            //if ((!type.IsClass || type == typeof(string)) && path.TryGetSingleValue(container, origin, out temp))
            //{
            //    obj = temp;
            //    return true;
            //}

            //return false;

            obj = null;


            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Templatable<>))
                ;

            if (type == typeof(Template))
                ;

            if (type == typeof(List<string>) && XMLPath.ListSeparator != null)
                ;

            if (type.IsClass && type != typeof(string))
                ;

            if (!type.IsClass || type == typeof(string))
                ;

            return false;
        }

        // implement global templates
        public bool TryResolveTemplate(GameContainer? gc, Type type, [NotNullWhen(true)] out Template? template)
        {
            template = null;

            if (!Enum.TryParse(Paths.First().Steps.First(), out TemplateType tType))
                return false;

            if (!gc.Templates.TryGetValue(tType, out Dictionary<string, Template>? templates))
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
    }
}
