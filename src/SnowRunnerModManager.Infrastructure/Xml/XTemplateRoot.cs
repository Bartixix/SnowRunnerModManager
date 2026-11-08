using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XTemplateRoot(string name, XDocument document) : XTemplateBase(name, document)
    {
        public override XTemplateBase? Parent { get; } = null;

        public string? Include { get; private set; }

        public bool SetInclude(string include)
        {
            if (Include != null)
                return false;

            Include = include;
            return true;
        }
    }
}
