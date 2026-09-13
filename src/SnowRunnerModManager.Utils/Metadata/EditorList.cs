using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorList(string name, int order) : EditorMetadata(name, order)
    {
        public bool AllowAdd { get; init; } = true;
        public bool AllowRemove { get; init; } = true;
        public bool AllowReorder { get; init; } = true;
    }
}
