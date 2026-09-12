using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorList(string name) : Attribute
    {
        public string Name { get; } = name;

        public bool AllowAdd { get; init; } = true;
        public bool AllowRemove { get; init; } = true;
        public bool AllowReorder { get; init; } = true;
    }
}
