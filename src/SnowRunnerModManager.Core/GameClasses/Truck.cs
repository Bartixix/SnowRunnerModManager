using SnowRunnerModManager.Core.GameClasses.Subclasses;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;
using SnowRunnerModManager.Core.Classes;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Truck : GameCommon
    {
        public enum TorqueMode
        {
            None,
            Connectable,
            Full,
            Default
        }

        public enum DiffLock
        {
            None,
            Uninstalled,
            Installed,
            Always
        }


        [EditorField("First axle AWD", 0)]
        public TorqueMode FrontAWD { get; set; }
        [EditorField("Rear AWD", 1)]
        public TorqueMode ReadAWD { get; set; }
        [EditorField("Second axle AWD", 2)]
        public TorqueMode SecondAxleAWD { get; set; }

        [PropertyIgnoreIf(false, nameof(SecondAxleAWD))]
        public bool HasSecondAxle { get; }

        [EditorField("Diff lock type", 3)]
        public DiffLock DiffLockType { get; set; }

        [EditorList("Compatible wheels", 4)]
        public List<CompatibleWheels> CompatibleWheels { get; set; }

        [EditorBinding("Gearboxes", 5, nameof(ActiveProject.Gearboxes), true)]
        public List<string> GearboxSocket { get; set; }

        [EditorBinding("Engines", 6, nameof(ActiveProject.Engines), true)]
        public List<string> EngineSocket { get; set; }
    }
}
