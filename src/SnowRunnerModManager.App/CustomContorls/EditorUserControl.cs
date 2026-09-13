using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SnowRunnerModManager.App.CustomContorls
{
    public class EditorUserControl : UserControl
    {
        public required string FieldName { get; init; }
        public int Order { get; init; } = -1;
        public required PropertyInfo Parameter { get; init; }

        public required object Target { protected get; init; }
    }
}
