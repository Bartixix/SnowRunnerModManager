using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyIgnoreIf(bool ignoreIf, params string[] paramNames) : Attribute
    {
        public bool IgnoreIf { get; } = ignoreIf;
        public string[] ParamNames { get; } = paramNames;
    }
}
