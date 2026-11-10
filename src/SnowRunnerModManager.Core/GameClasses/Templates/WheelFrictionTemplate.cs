using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class WheelFrictionTemplate : Template<WheelFrictionTemplate>
    {
        [XMLElement("#$")]
        public required double BodyFrictionAsphalt { get; init; }

        [XMLElement("#$")]
        public required double BodyFriction { get; set; }

        [XMLElement("#$")]
        public required double SubstanceFriction { get; set; }

        [XMLElement("#$", DefaultValue = "false")]
        public required bool IsIgnoreIce { get; set; }
    }
}
