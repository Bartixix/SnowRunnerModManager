using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XTemplate(string name, XTemplateBase parent, XDocument document) : XTemplateBase(name, document)
    {
        public override XTemplateBase? Parent { get; } = parent;
    }
}
