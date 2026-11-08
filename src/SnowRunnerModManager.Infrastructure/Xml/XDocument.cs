using SnowRunnerModManager.Infrastructure.Xml.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XDocument
    {
        public XTemplateRoot? TemplateRoot { get; private set; }
        public XElementRoot? ElementRoot { get; private set; }

        public string Path { get; }

        private XDocument(string path)
        {
            Path = path;
        }

        public static XDocument Load(string path)
        {
            using FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using XmlReader xr = XmlReader.Create(fs, new() { ConformanceLevel = ConformanceLevel.Fragment });

            XDocument doc = new(path);

            XReader.Read(doc, xr);

            return doc;
        }

        internal bool SetElementRoot(XElementRoot root)
        {
            if (ElementRoot != null)
                return false;

            ElementRoot = root;
            return true;
        }

        internal bool SetTemplateRoot(XTemplateRoot root)
        {
            if (TemplateRoot != null)
                return false;

            TemplateRoot = root;
            return true;
        }
    }
}
