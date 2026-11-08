//using SnowRunnerModManager.Utils.Events;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Globalization;
//using System.Text;
//using System.Xml.Linq;

//namespace SnowRunnerModManager.Infrastructure.Helpers
//{
//    public static class XDocumentHelper
//    {
//        public static List<XElement> QuerySelector(this XElement element, string selector)
//        {
//            IEnumerable<XElement> elements = [element];
//            string[] selectors = selector.Split('>');

//            foreach (string s in selectors)
//                elements = elements.SelectMany(e => e.Elements(s));

//            return [.. elements];
//        }

//        public static bool GetAttributeValue(this XElement element, string attributeName, [NotNullWhen(true)] out string? value)
//        {
//            value = null;
//            XAttribute? attribute = element.Attribute(attributeName);

//            if (attribute == null)
//                return false;

//            value = attribute.Value;
//            return true;
//        }


//        public static T Parse<T>(this string str) where T : IParsable<T>
//        {
//            if (!T.TryParse(str, CultureInfo.InvariantCulture, out T? res))
//            {
//                AppEvents.RaiseMessage(null, new("Parsing text to number failed", MessageEventArgs.MessageSeverity.Error);
//                throw new FormatException($"Failed to parse \"{str}\".");
//            }

//            return res;
//        }

//        public static bool ParseBool(this string str)
//        {
//            if (!bool.TryParse(str, out bool res))
//            {
//                AppEvents.RaiseMessage(null, new("Parsing text to true/false failed", MessageEventArgs.MessageSeverity.Error);
//                throw new FormatException($"Failed to parse \"{str}\" to bool.");
//            }

//            return res;
//        }
//    }
//}
