using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Subclasses
{
    [EditorSimpleClass]
    public class CompatibleWheels
    {
        [EditorField("Z offset", 0)]
        public double OffsetZ { get; set; }
        [EditorField("Rear Z offset", 1)]
        public double RearOffsetZ { get; set; }
        [EditorBinding("Wheels", 2, nameof(ActiveProject.Wheels))]
        public string Wheel { get; set; }
    }
}
