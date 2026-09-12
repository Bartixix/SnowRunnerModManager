using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Infrastructure.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SnowRunnerModManager.Core.Projects
{
    public class Project
    {
        public string Name { get; }
        public string RootFolder { get; }
        public string GameFolder { get; }

        public static Project? CurrentProject { get; private set; }

        public GroupRegistry<Engine> Engines { get; } = new();
        public GroupRegistry<Gearbox> Gearboxes { get; } = new();
        public GroupRegistry<Wheel> Wheels { get; } = new();
        public Registry<Truck> Trucks { get; } = new();
        public Registry<Wheel> WheelTemplates { get; } = new();


        private Project(string name, string rootFolder, string gameFolder)
        {
            Name = name;
            RootFolder = rootFolder;
            GameFolder = gameFolder;
        }


        public static async Task<Project> Load(string path)
        {
            return null;
        }

        public static async Task<Project> Create(string name, string rootFolder, string gameFolder)
        {
            CurrentProject = new(name, rootFolder, gameFolder);

            //ProjectIO.VerifyGameDir(gameFolder);


            return CurrentProject;
        }
    }
}
