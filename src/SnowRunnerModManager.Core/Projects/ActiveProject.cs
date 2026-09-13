using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;

using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.Projects
{
    public class ActiveProject
    {
        [EditorSource]
        public static GroupRegistry<Engine> Engines { get { return Project.CurrentProject.Engines; } }
        [EditorSource]
        public static GroupRegistry<Gearbox> Gearboxes { get { return Project.CurrentProject.Gearboxes; } }
        [EditorSource]
        public static GroupRegistry<Wheel> Wheels { get { return Project.CurrentProject.Wheels; } }
        public static Registry<Truck> Trucks { get { return Project.CurrentProject.Trucks; } }
        public static Registry<Wheel> WheelTemplates { get { return Project.CurrentProject.WheelTemplates; } }
    }
}
