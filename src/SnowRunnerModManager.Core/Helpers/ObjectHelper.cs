using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SnowRunnerModManager.Core.Helpers
{
    public class ObjectHelper
    {
        public static T Clone<T>(T obj) where T : class
        {
            string json = JsonSerializer.Serialize(obj);
            T res = JsonSerializer.Deserialize<T>(json)!;

            return res;
        }
    }
}
