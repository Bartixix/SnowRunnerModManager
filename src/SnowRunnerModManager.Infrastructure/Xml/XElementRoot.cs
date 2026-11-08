using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XElementRoot(string name, XDocument document) : XElementBase(name, document)
    {
        public override XElementBase? Parent { get; } = null;
    }
}
