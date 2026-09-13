using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public interface IRegistry
    {
        Type ItemType { get; }
        IReadOnlyCollection<string> ItemNames { get; }
    }
}
