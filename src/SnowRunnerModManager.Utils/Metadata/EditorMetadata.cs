using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{
    public abstract class EditorMetadata(string name, int order) : Attribute
    {
        public string Name { get; } = name;
        public int Order { get; } = order;
    }
}
