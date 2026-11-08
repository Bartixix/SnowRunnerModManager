using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XElement(string name, XElementBase parent, XDocument document) : XElementBase(name, document)
    {
        public override XElementBase Parent { get; } = parent;
    }
}
