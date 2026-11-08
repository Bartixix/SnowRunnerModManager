using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    [EditorSimpleClass]
    public class CompatibleWheels
    {
        [EditorField("Z offset", 0)]
        [XMLElement("#$", DefaultValue = "0")]
        public required double OffsetZ { get; set; }
        [EditorField("Rear Z offset", 1)]
        [XMLElement("#$", DefaultValue = "0")]
        public required double RearOffsetZ { get; set; }
        [EditorBinding("Wheels", 2, nameof(ActiveProject.Wheels))]
        [XMLElement("#Type")]
        public required string Wheel { get; set; }
    }
}
