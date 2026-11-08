using SnowRunnerModManager.Core.GameClasses.Objects;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Infrastructure.IO;
using SnowRunnerModManager.Utils.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

using SnowRunnerModManager.Infrastructure.Xml;

namespace SnowRunnerModManager.Core.Helpers
{
    public static class ParserHelper
    {
        public static T Parse<T>(this string str) where T : IParsable<T>
        {
            if (!T.TryParse(str, CultureInfo.InvariantCulture, out T? res))
            {
                AppEvents.RaiseMessage(null, new("Parsing text to number failed", MessageEventArgs.MessageSeverity.Error));
                throw new FormatException($"Failed to parse \"{str}\".");
            }

            return res;
        }

        public static object Parse(Type objectType, object obj)
        {
            if (objectType == typeof(int))
                return ((string)obj).Parse<int>();

            if (objectType == typeof(double))
                return ((string)obj).Parse<double>();

            if (objectType.IsEnum)
                return Enum.Parse(objectType, (string)obj, true);

            if (objectType == typeof(bool))
                return bool.Parse((string)obj);

            return obj;
        }

        public static bool ParseBool(this string str)
        {
            if (!bool.TryParse(str, out bool res))
            {
                AppEvents.RaiseMessage(null, new("Parsing text to true/false failed", MessageEventArgs.MessageSeverity.Error));
                throw new FormatException($"Failed to parse \"{str}\" to bool.");
            }

            return res;
        }

        public static string? GetFileName(string origin)
        {
            if (!Path.GetDirectoryName(origin)!.Contains(Constants.Trucks))
                return null;
            return Path.GetFileNameWithoutExtension(origin);
        }
        public static string GetRelativePath(string origin)
        {
            string initialUnpacked = Project.CurrentProject.InitialPath;
            return Path.GetRelativePath(initialUnpacked, origin);
        }

        public static string? GetAxleTorque<TParent, TChild>(XBase<TParent, TChild> container, int id)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            if (id == 0)
            {
                XBase<TParent, TChild> element = container.Element("FrontWheel") ?? container.Element("FirstAxle") ?? throw new Exception();
                return GetTorqueValue(element);
            }
            if (id == 1)
            {
                XBase<TParent, TChild> element = container.Element("SecondAxle") ?? throw new Exception();
                return GetTorqueValue(element);
            }
            if (id == 2)
            {
                XBase<TParent, TChild> element = container.Element("MiddleAxle") ?? throw new Exception();
                return GetTorqueValue(element);
            }
            if (id == 3)
            {
                XBase<TParent, TChild> element = container.Element("RearWheel") ?? container.Element("RearAxle") ?? throw new Exception();
                return GetTorqueValue(element);
            }

            return null;
        }

        private static string? GetTorqueValue<TParent, TChild>(XBase<TParent, TChild> element)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            string? value = element.GetAttribute("Torque")?.Value;
            if (value != null)
                return value;

            string template = element.GetAttribute("_template")?.Value ?? throw new Exception();
            return element?.Parent?.Element(template)?.GetAttribute("Torque")?.Value ?? throw new Exception();
        }

        public static string? GetTemplateName(XBase container) => container.Name;

        public static string? ResolveTemplate<TParent, TChild>(XBase<TParent, TChild> container, string templateType)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            string? template = container.Element("WheelFriction")?.GetAttribute("_template")?.Value;
            if (template == null)
            {
                string type = container.GetAttribute("_template")?.Value ?? throw new Exception();
                template = container.Document.TemplateRoot?.Element("TruckTire")?.Element(type)?.Element("WheelFriction")?.GetAttribute("_template")?.Value;
            }

            if (template == null)
                throw new Exception();

            Wheel wheelTemplate = ActiveProject.WheelTemplates.Get(template);

            string? res = templateType switch
            {
                "bfa" => wheelTemplate.BodyFrictionAsphalt.ToString(),
                "bf" => wheelTemplate.BodyFriction.ToString(),
                "sf" => wheelTemplate.SubstanceFriction.ToString(),
                _ => null
            };

            return res;
        }

        //public static string? IsWheelTemplate<TParent, TChild>(XBase<TParent, TChild> container)
        //    where TParent : XBase<TParent, TChild>
        //    where TChild : XBase<TParent, TChild>
        //{
        //    XBase<TParent, TChild> element = container;
        //    if (element.Name != "TruckTire")
        //        return bool.TrueString;
        //    return bool.FalseString;
        //}

        //public static async Task<XContainer> LoadXML(string path)
        //{
        //    string file = await File.ReadAllTextAsync(path);
        //    string newRoot = $"<Root>{file}</Root>";
        //    newRoot = newRoot.Replace("region:", "SRMMregion");
        //    return XDocument.Parse(newRoot).Root!;
        //}

        public static string? HasAxle<TParent, TChild>(XBase<TParent, TChild> element, int axle)
            where TParent : XBase<TParent, TChild>
            where TChild : XBase<TParent, TChild>
        {
            if (element.Children.Count == 5)
                throw new InvalidDataException();

            if (axle == 1)
                return (element.Element("FrontWheel") ?? element.Element("FirstAxle")) != null ? bool.TrueString : bool.FalseString;

            if (axle == 2)
                return element.Element("SecondAxle") != null ? bool.TrueString : bool.FalseString;

            if (axle == 3)
                return element.Element("MiddleAxle") != null ? bool.TrueString : bool.FalseString;

            if (axle == 4)
                return (element.Element("RearWheel") ?? element.Element("RearAxle")) != null ? bool.TrueString : bool.FalseString;

            //if (element.Descendants().Count() == 3)
            //    return bool.TrueString;
            //if (element.Descendants().Count() == 2)
            //    return bool.FalseString;


            return null;
        }
    }
}
