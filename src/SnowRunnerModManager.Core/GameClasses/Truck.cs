using SnowRunnerModManager.Utils.Metadata;

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


        [EditorDropdown("First axle AWD")]
        public TorqueMode FrontAWD { get; set; }
        [EditorDropdown("Rear AWD")]
        public TorqueMode ReadAWD { get; set; }
        [EditorDropdown("Second axle AWD")]
        public TorqueMode SecondAxleAWD { get; set; }

        [PropertyIgnoreIf(false, nameof(SecondAxleAWD))]
        public bool HasSecondAxle { get; }

        [EditorDropdown("Diff lock type")]
        public DiffLock DiffLockType { get; set; }
    }
}
