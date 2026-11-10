using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using SnowRunnerModManager.Core.GameClasses.Common;

namespace SnowRunnerModManager.Core.Helpers
{
    public class ObjectHelper
    {
        public static T Clone<T>(T obj) where T : IModificationTrackable
        {
            string json = JsonSerializer.Serialize(obj);
            T res = JsonSerializer.Deserialize<T>(json)!;

            return res;
        }
    }
}
