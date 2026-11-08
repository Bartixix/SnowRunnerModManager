using Microsoft.Win32;
using SnowRunnerModManager.Utils.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using SnowRunnerModManager.Infrastructure.IO;

namespace SnowRunnerModManager.Infrastructure.Helpers
{

    public class SteamHelper : IGameLibHelper
    {
        private const string SteamUserSubKey = "Software\\Valve\\Steam";
        private const string SteamUserName = "SteamPath";
        private const string SteamGlobalSubKey = "SOFTWARE\\WOW6432Node\\Valve\\Steam";
        private const string SteamGlobalName = "InstallPath";
        private const string SteamLibraryDataRelative = "steamapps\\libraryfolders.vdf";
        private const int SnowRunnerSteamId = 1465360;
        private const string SnowRunnerRelativePath = "steamapps\\common\\SnowRunner";

        public static bool TryGetSnowRunnerPath([NotNullWhen(true)] out string? path)
        {
            path = null;

            if (!TryFindSteamPath(out string? steamPath))
            {
                AppEvents.RaiseMessage(null, new("Failed to fetch steam path", MessageEventArgs.MessageSeverity.Warning, true));
                return false;
            }

            string[] libraries = GetSteamLibraries(steamPath);

            if (!GetRootGamePath(libraries, SnowRunnerSteamId, out path))
            {
                AppEvents.RaiseMessage(null, new("Failed to fetch steam game path", MessageEventArgs.MessageSeverity.Warning, true));
                return false;
            }

            return true;
        }

        private static bool TryFindSteamPath([NotNullWhen(true)] out string? steamPath)
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(SteamUserSubKey))
            {

                string? path = key?.GetValue(SteamUserName) as string;

                if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                {
                    steamPath = path;
                    return true;
                }
            }

            using (RegistryKey? key = Registry.LocalMachine.OpenSubKey(SteamGlobalSubKey))
            {
                string? path = key?.GetValue(SteamGlobalName) as string;

                if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                {
                    steamPath = path;
                    return true;
                }
            }

            steamPath = null;
            return false;
        }

        private static string[] GetSteamLibraries(string steamPath)
        {
            string libConfigsPath = Path.Combine(steamPath, SteamLibraryDataRelative);
            List<string> configPaths = [.. File.ReadAllLines(libConfigsPath)];

            List<string> libPathsRaw = configPaths.FindAll(s => s.Trim().StartsWith("\"path\""));

            List<string> libPaths = [.. libPathsRaw.Select(s => s.Split('"').SkipLast(1).Last().Replace("\\\\", "\\"))];

            return [.. libPaths];
        }

        private static string GetAppManifestFile(int appId) => $"steamapps\\appmanifest_{appId}.acf";

        private static bool GetRootGamePath(string[] libraries, int appId, [NotNullWhen(true)] out string? path)
        {
            string manifest = GetAppManifestFile(appId);

            foreach (string library in libraries)
            {
                string manifestPath = Path.Combine(library, manifest);
                if (!File.Exists(manifestPath))
                    continue;

                path = Path.Combine(library, SnowRunnerRelativePath, Constants.EXERelative);
                return true;
            }

            path = null;
            return false;
        }
    }
}
