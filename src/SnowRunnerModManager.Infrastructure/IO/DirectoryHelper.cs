using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.IO
{
    public class DirectoryHelper
    {
        public static List<string> GetInitialFolders(string initialDir, string folders)
        {
            string configRoot = Path.Combine(initialDir, "[media]");

            return [.. Directory.EnumerateDirectories(configRoot, $"*{folders}", SearchOption.AllDirectories)];
        }

        public static List<string> GetFilesInFolders(List<string> folders, string searchPattern) =>
            [.. folders.SelectMany(f => Directory.EnumerateFiles(f, searchPattern))];

    }
}
