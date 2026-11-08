using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XAttribute
    {
        public string Name { get; }
        public string Value { get; set; } = string.Empty;

        public XAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public XAttribute(string name)
        {
            Name = name;
        }
    }
}
