using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Subclasses
{
    public class CompatibleWheels
    {
        [EditorField("Z offset")]
        public double OffsetZ { get; set; }
        [EditorField("Rear Z offset")]
        public double RearOffsetZ { get; set; }
    }
}
