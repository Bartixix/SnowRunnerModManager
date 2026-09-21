using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.Helpers
{
    public interface IGameLibHelper
    {
        public static abstract bool TryGetSnowRunnerPath([NotNullWhen(true)] out string? path);
    }
}
