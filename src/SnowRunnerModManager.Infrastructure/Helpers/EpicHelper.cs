using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Helpers
{
    public class EpicHelper : IGameLibHelper
    {
        public static bool TryGetSnowRunnerPath([NotNullWhen(true)] out string? path)
        {
            path = null;
            return false;
        }
    }
}
