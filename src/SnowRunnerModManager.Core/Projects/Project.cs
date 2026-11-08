using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses.Objects;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Infrastructure.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SnowRunnerModManager.Core.Projects
{
    public class Project
    {
        [MemberNotNullWhen(true, nameof(_currentProject))]
        public static bool Initiated { get; private set; }
        private static Project? _currentProject;
        public static Project CurrentProject
        {
            get
            {
                if (!Initiated)
                    throw new InvalidOperationException();
                return _currentProject;
            }
            private set
            {
                Initiated = value != null;
                _currentProject = value;
            }
        }

        public string Name { get; }
        public string RootFolder { get; }
        public string GameFolder { get; }

        public GroupRegistry<Engine> Engines { get; } = new();
        public GroupRegistry<Gearbox> Gearboxes { get; } = new();
        public GroupRegistry<Wheel> Wheels { get; } = new();
        public Registry<Truck> Trucks { get; } = new();
        public Registry<Wheel> WheelTemplates { get; } = new();

        public string InitialPath { get; }


        private Project(string name, string rootFolder, string gameFolder)
        {
            Name = name;
            RootFolder = rootFolder;
            GameFolder = gameFolder;

            InitialPath = Path.Combine(RootFolder, Constants.InitialUnpacked);
        }


        public static async Task<Project> Load(string path)
        {
            return null;
        }

        public static async Task<Project?> Create(string name, string rootFolder, string exePath)
        {
            if (!ProjectIO.VerifyGameDir(exePath, out string? gameFolder))
                return null;

            CurrentProject = new(name, rootFolder, gameFolder);

            await ProjectIO.UnpackInitial(rootFolder, gameFolder);
            await RegistryHelper.LoadAllRegistries();

            return CurrentProject;
        }

        public static Project CreateDummy()
        {
            CurrentProject = new Project("", "", "");
            Initiated = true;
            return CurrentProject;
        }
    }
}
