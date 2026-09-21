using SnowRunnerModManager.Infrastructure.Helpers;
using SnowRunnerModManager.Pak;
using SnowRunnerModManager.Utils.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;

namespace SnowRunnerModManager.Infrastructure.IO
{
    public class ProjectIO
    {
        public static bool VerifyGameDir(string exePath, [NotNullWhen(true)] out string? gameFolder)
        {
            gameFolder = null;

            if (!File.Exists(exePath))
                return false;

            Uri uri = new(exePath);

            if (!uri.IsAbsoluteUri)
                return false;

            if (!exePath.EndsWith(Constants.EXERelative))
                return false;

            string rootDir = exePath[..^Constants.EXERelative.Length];

            string paks = Path.Combine(rootDir, Constants.PAKFolderRelative);
            string initial = Path.Combine(paks, Constants.InitialPAK);
            string shared = Path.Combine(paks, Constants.SharedPAK);
            string sharedSound = Path.Combine(paks, Constants.SharedSoundPAK);

            if (!File.Exists(initial) ||
                !File.Exists(shared) ||
                !File.Exists(sharedSound))
                return false;

            gameFolder = rootDir;
            return true;
        }

        public static async Task UnpackInitial(string rootDir, string gameDir)
        {
            string initialPak = Path.Combine(gameDir, Constants.PAKFolderRelative, Constants.InitialPAK);
            string unpackedRoot = Path.Combine(rootDir, Constants.InitialUnpacked);

            using FileStream fs = new(initialPak, FileMode.Open, FileAccess.Read, FileShare.Read);
            using ZipArchive zip = new(fs, ZipArchiveMode.Read);

            HashSet<string> createdDirs = [];

            for (int i = 0; i < zip.Entries.Count; i++)
            {
                ZipArchiveEntry entry = zip.Entries[i];
                string unpackedName = Path.Combine(unpackedRoot, entry.FullName);

                string dir = Path.GetDirectoryName(unpackedName)!;
                if (!createdDirs.Contains(dir))
                {
                    Directory.CreateDirectory(dir);
                    createdDirs.Add(dir);
                }

                await entry.ExtractToFileAsync(unpackedName, true);

                double progress = (double)((i + 1) * 100) / zip.Entries.Count;

                ProgressEventArgs e = new("unpack-initial-progress", "Unpacking", progress, ProgressEventArgs.ProgressState.Running);
                AppEvents.RaiseProgress(null, e);
            }

            AppEvents.RaiseProgress(null, new("unpack-initial-progress", "Unpacking", 100, ProgressEventArgs.ProgressState.Completed));
        }

        public static bool TryGetSnowRunnerPath([NotNullWhen(true)] out string? path)
        {
            path = null;

            if (SteamHelper.TryGetSnowRunnerPath(out path))
                return true;

            if (EpicHelper.TryGetSnowRunnerPath(out path))
                return true;

            return false;
        }
    }
}
