using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Infrastructure.Xml;
using System;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace SnowRunnerModManager.Core.GameParsers
{
    public class ParserPath
    {
        public enum TerminationType
        {
            Tag,
            Attribute,
            Operation
        }

        public ImmutableArray<string> Steps { get; }
        public TerminationType Termination { get; }
        public string? TerminationString { get; } = null;
        public string? TerminationArg { get; } = null;
        public string Name { get; }
        public bool NoTags { get; } = false;

        public ParserPath(string path, string name)
        {
            Name = name;

            int operandCount = path.Count(c => c == '#' || c == '%');
            if (operandCount > 1)
                throw new InvalidDataException();

            if (path.StartsWith('%') || path.StartsWith('#'))
                NoTags = true;

            path = path.Replace("$", name);
            string[] queries = path.Split('>');

            if (operandCount == 0)
            {
                Termination = TerminationType.Tag;
                Steps = [.. queries];
                return;
            }

            string termninationString = queries[^1];

            if (!termninationString.Any(c => c == '#' || c == '%'))
                throw new InvalidDataException();

            char splitChar = termninationString.Contains('#') ? '#' : '%';

            queries = [.. queries.SelectMany(s => s.Split(splitChar))];

            if (splitChar == '#')
                Termination = TerminationType.Attribute;

            if (splitChar == '%')
                Termination = TerminationType.Operation;

            if (!NoTags)
                Steps = [.. queries.SkipLast(1)];
            else
                Steps = [];

            string[] terminationArgs = queries[^1].Split('@');
            TerminationString = terminationArgs[0];

            if (terminationArgs.Length > 1)
                TerminationArg = terminationArgs[1];
        }

        public bool TryGetSingleValue<TParent, TChild>(XBase<TParent, TChild> document, string origin, [NotNullWhen(true)] out string? value)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            value = null;

            if (Termination == TerminationType.Tag)
                return false;

            //if (!TryResolveQuery(document, out XElement? element))
            //    return false;
            List<TChild> elements = document.QuerySelector([.. Steps]);
            if (elements.Count == 0)
                return false;

            XBase<TParent, TChild> element = elements[0];

            if (Termination == TerminationType.Attribute)
            {
                XAttribute? attribute = element.GetAttribute(TerminationString!);
                if (attribute == null)
                    return false;

                value = attribute.Value;
                return true;
            }

            if (Termination == TerminationType.Operation)
            {
                if (!Parser.TryResolveSpecial(TerminationString!, TerminationArg, element, origin, out value))
                    return false;
                return true;
            }

            return false;
        }

        public bool TryGetClassValue<TParent, TChild>(XBase<TParent, TChild> document, GameContainer gc, Type objectType, string origin, [NotNullWhen(true)] out object? value)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            value = null;

            if (Termination != TerminationType.Tag)
                return false;

            //if (!TryResolveQuery(document, out XElement? newRoot))
            //    return false;

            XBase<TParent, TChild> newRoot = document.QuerySelector([.. Steps]).FirstOrDefault() ?? throw new Exception();

            return LowLevelParse(newRoot, gc, objectType, origin, out value);
        }

        public bool TryGetClassValueList<TParent, TChild>(XBase<TParent, TChild> container, GameContainer gc, Type objectType, string origin, [NotNullWhen(true)] out IList? value)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            value = null;
            IList list = (IList)Activator.CreateInstance(objectType)!;

            //if (!TryResolveQueryAll(container, out XBase<TParent, TChild>[] elements))
            //    return false;

            List<TChild> elements = container.QuerySelector([.. Steps]);


            foreach (TChild element in elements)
            {
                if (!LowLevelParse(element, gc, objectType.GetGenericArguments()[0], origin, out object? obj))
                    return false;

                list.Add(obj);
            }

            value = list;
            return true;
        }

        private static bool LowLevelParse<TParent, TChild>(XBase<TParent, TChild> root, GameContainer gc, Type objectType, string origin, [NotNullWhen(true)] out object? value)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            Parser parser = Parser.GetParser(objectType);
            value = parser.Parse(root, gc, origin);

            if (value == null)
                return false;

            return true;
        }

        //public bool TryResolveQuery<TParent, TChild>(XBase<TParent, TChild> container, [NotNullWhen(true)] out XBase<TParent, TChild>? element)
        //    where TParent : XBase<TParent, TChild>
        //    where TChild : XBase<TParent, TChild>
        //{
        //    element = null;

        //    if (Steps.Length == 0)
        //    {
        //        element = container;
        //        if (element == null)
        //            return false;

        //        return true;
        //    }

        //    XBase<TParent, TChild>? res = container.Element(Steps[0]);
        //    if (res == null) return false;

        //    for (int i = 1; i < Steps.Length; i++)
        //    {
        //        res = res.Element(Steps[i]);
        //        if (res == null) return false;
        //    }

        //    element = res;
        //    return true;
        //}

        //public XElement[] ResolveQueryAll(XContainerBase container)
        //{
        //    if (Steps.Length == 0)
        //        throw new Exception();

        //    XElement? res = container.Element(Steps[0]) ?? throw new Exception();
        //    for (int i = 1; i < Steps.Length - 1; i++)
        //        res = res.Element(Steps[i]) ?? throw new Exception();

        //    return [.. res.Elements(Steps[^1])];
        //}

        //public bool TryResolveQueryAll<TParent, TChild>(XBase<TParent, TChild> container, [NotNullWhen(true)] out XBase<TParent, TChild>[] elements)
        //    where TParent : XBase<TParent, TChild>
        //    where TChild : XBase<TParent, TChild>
        //{
        //    elements = [];
        //    XBase<TParent, TChild>? element;

        //    if (Steps.Length == 0)
        //    {
        //        element = container as XBase<TParent, TChild>;
        //        if (element == null)
        //            return false;

        //        return true;
        //    }

        //    XBase<TParent, TChild>? res = container.Element(Steps[0]);
        //    if (res == null) return false;

        //    for (int i = 1; i < Steps.Length - 1; i++)
        //    {
        //        res = res.Element(Steps[i]);
        //        if (res == null) return false;
        //    }

        //    elements = [.. res.Elements(Steps[^1])];
        //    return true;
        //}
    }

}
