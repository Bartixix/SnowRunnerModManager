//using SnowRunnerModManager.Utils.Events;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Numerics;
//using System.Text;
//using System.Xml.Linq;

//namespace SnowRunnerModManager.Infrastructure.Helpers
//{
//    public class XMLHelper(string filePath)
//    {
//        public XDocument Document { get; } = XDocument.Load(filePath);

//        public string? GetValue(string query)
//        {
//            string[] selectors = GetElementNames(query, out _);

//            XElement element = Document.Root!;

//            foreach (string selector in selectors)
//                element = element.Element(selector)!;

//            return element.Value;
//        }

//        public string? GetAttributeValue(string query)
//        {
//            string[] selectors = GetElementNames(query, out string? attribute);

//            if (attribute == null)
//                throw new Exception("Attribute doesn't exist.");

//            XElement element = Document.Root!;

//            foreach (string selector in selectors)
//                element = element.Element(selector)!;

//            return element.Attribute(attribute)!.Value;
//        }

//        public static string[] GetElementNames(string query, out string? attribute)
//        {
//            attribute = null;

//            string[] parts = query.Split('#');

//            if (parts.Length > 2)
//                throw new Exception("Invalid query");

//            if (parts.Length == 2)
//                attribute = parts[1];

//            string[] selectors = parts[0].Split('>');
//            return selectors;
//        }


//    }
//}
