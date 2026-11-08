using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Xml
{
    public sealed class XTemplateBinding
    {
        public string? GlobalTemplatePath { get; }
        public XTemplate? LocalTemplate { get; }
        public bool IsGlobalTemplate { get; }

        internal XTemplateBinding(string globalTemplatePath)
        {
            IsGlobalTemplate = true;
            GlobalTemplatePath = globalTemplatePath;
        }

        internal XTemplateBinding(XTemplate localTemplate)
        {
            IsGlobalTemplate = false;
            LocalTemplate = localTemplate;
        }
    }
}
