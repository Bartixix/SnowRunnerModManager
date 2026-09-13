using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Metadata
{

    [AttributeUsage(AttributeTargets.Property)]
    public class EditorSource : Attribute;


    [AttributeUsage(AttributeTargets.Property)]
    public class EditorBinding(string name, int order, string sourceName, bool multiChoice = false) : EditorMetadata(name, order)
    {
        public string SourceName { get; } = sourceName;
        public bool MultiChoice { get; } = multiChoice;
    }
}
